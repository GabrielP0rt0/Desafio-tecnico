using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransferenciaAPi.Core.ResponseBase;
using TransferenciaAPi.Constans;
using TransferenciaAPi.Data;
using TransferenciaAPi.Data.Entidades;
using TransferenciaAPi.Models;
using Microsoft.AspNetCore.Mvc;
using TransferenciaAPi.Errors;

namespace TransferenciaAPi.Services
{
    public class AutorizacaoServices : SingleResponse
    {   //injetando context diretamente de DBContext
        private DBContext _context;

        public AutorizacaoServices(DBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Este método cadastra o usuário, criando um objeto na tabela USUARIO e um na tabela CONTA, após isso o método retorna um token de acesso para que o usuário
        /// já possa começar suas operações, foi definido um valor default de 100 reais para cada conta criada.
        /// </summary>
        /// <param name="Cadastrar"></param>
        /// <returns></returns>
        public async Task<IActionResult> Register(RegisterRequestModel request)
        {
            (bool isValid, string err) = request.Validate();
            if (!isValid )
                return ErrorResponse(err);
            //verifica se já existe um email igual cadastrado
            User? user = await _context.User.Where(x => x.Email.Equals(request.Email)).FirstOrDefaultAsync(); 
 
            if (user != null)
                return ErrorResponse(TransferenciaErrors.UserExist);
            //caso não exista email semelhante, o usuário é criado
            user = new()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            //adiciona o usuário na tabela de usuários e aplica as mudanças
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            //transfere os dados criados para a tabela de Conta e adiciona um saldo de 100 reais automaticamente
            Account account = new Account
            {
                IdUser = user.Id,
                TransferKey = Guid.NewGuid(),
                Amount = 100m,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();
            //cria o token de acesso
            string token = CriarToken();
            //retorna o status do cadastro
            return SuccessResponse(new RegisterResponseModel
            {
                Token = token,
                RegisterMessage = "Usuário cadastrado com sucesso!",
                IdUser = user.Id,
                IdAccount = account.Id,
                TransferKey = account.TransferKey
            });

        }
        /// <summary>
        /// Esta função é responsável por enviar as informações de email e conta, fazer uma consulta de correspondência no banco de dados e gerar um Token de
        /// acesso e retornar as informações necessárias.
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            (bool isValid, string err) = request.Validate();
            if (!isValid)
                return ErrorResponse(err);

            User user = await _context.User.Where(x => x.Email.Equals(request.Email)).FirstOrDefaultAsync();

            if (user == null || user.Password != request.Password)
                return ErrorResponse(TransferenciaErrors.LoginError);

            Account userAccount = await _context.Account.Where(x => x.Id.Equals(user.Id)).FirstOrDefaultAsync();

            if (userAccount == null)
                return ErrorResponse(TransferenciaErrors.LoginError);

            string token = CriarToken();


            return SuccessResponse(new LoginResponseModel
            {
                IdAccount = userAccount.Id,
                IdUser = user.Id,
                TransferKey = userAccount.TransferKey,
                LoginMessage = "login realizado com sucesso",
                Amount = userAccount.Amount,
                Token = token,
                Name = user.Name
            });
        }




        ///// <summary>
        ///// Essa função gera um ID de conta único e de forma aleatória, buscando sempre um número entre 1 e 1000000000 e retorna para a função de criação de usuários
        ///// </summary>
        ///// <returns></returns>
        //private int GeraIdConta(int tipoConta)
        //{
        //    int min = 1;            //número mínimo possível
        //    int max = 1000000000;      //número máximo possível

        //    Random rnd = new Random();                  //função que cria o número de forma aleatória
        //    int randomIdConta = rnd.Next(min, max);
        //    //verifica se já existe um id da conta igual, caso exista a função é chamada novamente e um novo Id é criado, isso acontece até que 
        //    //um número válido seja criado
        //    var idContaPrivateUnico = _context.Conta.Where(x => x.IdUsuarioPrivate.Equals(randomIdConta)).FirstOrDefault();
        //    var idContaPublicUnica = _context.Conta.Where(x => x.IdUsuarioPublic.Equals(randomIdConta)).FirstOrDefault();
        //    if (tipoConta == 1 && idContaPrivateUnico != null)
        //        return GeraIdConta(1);
        //    if (tipoConta == 2 && idContaPublicUnica != null)
        //        return GeraIdConta(2);
        //    return randomIdConta;
        //}
        /// <summary>
        /// Essa função é responsável por gerar um token de acesso e retornar ao usuário ao se cadastrar ou logar
        /// </summary>
        /// <returns></returns>
        private string CriarToken()
        {
            List<Claim> listClaims = new();

            listClaims.Add(new Claim(ClaimTypes.Role, Claims.level1));

            JwtSecurityTokenHandler tokenHandler = new();

            var Key = Encoding.UTF8.GetBytes(KeySecret.secret);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(listClaims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

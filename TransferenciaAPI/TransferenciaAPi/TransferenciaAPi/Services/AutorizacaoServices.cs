using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TransferenciaAPi.Constans;
using TransferenciaAPi.Data;
using TransferenciaAPi.Data.Entidades;
using TransferenciaAPi.Models;

namespace TransferenciaAPi.Services
{
    public class AutorizacaoServices
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
        public CadastroResponseModel Cadastrar(CadastroRequestModel request)
        {   //verifica se já existe um email igual cadastrado
            var usuario = _context.Usuarios.Where(x => x.Email.Equals(request.Email)).FirstOrDefault(); 
 
            if (usuario != null)
                return new()    //retorna devida mensagem e status caso já exista um email cadastrado
                { 
                    StatusCadastro = false,             
                    MensagemCadastro = "Usuário já cadastrado",
                    Token = "",
                    IdConta = 0
                };
            //caso não exista email semelhante, o usuário é criado
            usuario = new()
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };
            //adiciona o usuário na tabela de usuários e aplica as mudanças
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            var idContaPrivate = GeraIdConta(1);
            var idContaPublic = GeraIdConta(2);
            //chama a private function que cria o id randomicamente 
            //transfere os dados criados para a tabela de Conta e adiciona um saldo de 100 reais automaticamente
            _context.Conta.Add(new()
            {
                IdConta = usuario.Id,
                IdUsuarioPrivate = idContaPrivate,
                IdUsuarioPublic = idContaPublic,
                Saldo = 100
            }) ;

            _context.SaveChanges();
            //cria o token de acesso
            string token = CriarToken();
            //retorna o status do cadastro
            return new()
            {
                Token = token,
                MensagemCadastro = "tudo certo com o cadastro!",
                StatusCadastro = true,
                IdConta = usuario.Id,
                IdContaPrivate = idContaPrivate,
                IdContaPublic = idContaPublic
            };

        }
        /// <summary>
        /// Esta função é responsável por enviar as informações de email e conta, fazer uma consulta de correspondência no banco de dados e gerar um Token de
        /// acesso e retornar as informações necessárias.
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public LoginResponseModel Login(LoginRequestModel request)
        {
            LoginResponseModel errorResponse = new() 
            {
                IdConta = 0,
                StatusMensagem = "Senha incorreta ou usuário não cadastrado",
                StatusLogin = false,
                Saldo = 0,
                Token = "",
                Nome = ""
            };
            var usuario = _context.Usuarios.Where(x => x.Email.Equals(request.Email)).FirstOrDefault();

            if (usuario == null || usuario.Senha != request.Senha)
                return errorResponse;

            var usuarioConta = _context.Conta.Where(x => x.Id.Equals(usuario.Id)).FirstOrDefault();

            if (usuarioConta == null)
                return errorResponse;

            string token = CriarToken();
            LoginResponseModel retornaUsuario = new()
            {
                IdConta = usuarioConta.IdConta,
                IdContaPrivate = usuarioConta.IdUsuarioPrivate,
                IdContaPublic = usuarioConta.IdUsuarioPublic,
                StatusMensagem = "login realizado com sucesso",
                StatusLogin = true,
                Saldo = usuarioConta.Saldo,
                Token = token,
                Nome = usuario.Nome
            };

            return retornaUsuario;
        }




        /// <summary>
        /// Essa função gera um ID de conta único e de forma aleatória, buscando sempre um número entre 1 e 1000000000 e retorna para a função de criação de usuários
        /// </summary>
        /// <returns></returns>
        private int GeraIdConta(int tipoConta)
        {
            int min = 1;            //número mínimo possível
            int max = 1000000000;      //número máximo possível

            Random rnd = new Random();                  //função que cria o número de forma aleatória
            int randomIdConta = rnd.Next(min, max);
            //verifica se já existe um id da conta igual, caso exista a função é chamada novamente e um novo Id é criado, isso acontece até que 
            //um número válido seja criado
            var idContaPrivateUnico = _context.Conta.Where(x => x.IdUsuarioPrivate.Equals(randomIdConta)).FirstOrDefault();
            var idContaPublicUnica = _context.Conta.Where(x => x.IdUsuarioPublic.Equals(randomIdConta)).FirstOrDefault();
            if (tipoConta == 1 && idContaPrivateUnico != null)
                return GeraIdConta(1);
            if (tipoConta == 2 && idContaPublicUnica != null)
                return GeraIdConta(2);
            return randomIdConta;
        }
        /// <summary>
        /// Essa função é responsável por gerar um token de acesso e retornar ao usuário ao se cadastrar ou logar
        /// </summary>
        /// <returns></returns>
        private string CriarToken()
        {
            List<Claim> listClaims = new();

            listClaims.Add(new Claim(ClaimTypes.Role, Claims.camada1));

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

using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Runtime.InteropServices;
using TransferenciaAPi.Models;
using TransferenciaAPi.Services;

namespace TransferenciaAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutorizacaoController : ControllerBase
    {
        // Construtor responsável por AutorizacaoService 
        private AutorizacaoServices _autorizacao;
        public AutorizacaoController(AutorizacaoServices autorizacao)
        {
            _autorizacao = autorizacao;
        }

        /// <summary>
        /// EndPoint responsável por fazer o cadastro do usuário no banco de dados, criar sua conta, adicionar seu saldo inicial e retornar Token de acesso
        /// </summary>
        /// <param name="Cadastrar"></param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] CadastroRequestModel request)
        {
            //faz todas as verificações necessárias para garantir que o obejto será enviado corretamente
            if (request == null)
                return BadRequest("Request não pode ser nula!!!!");
            if (request.Nome == null)
                return BadRequest("Nome não pode ser nulo");
            if (request.Email == null)
                return BadRequest("Email não pode ser nulo");
            if (request.Senha == null)
                return BadRequest("Senha não pode ser nulo");
            var response = _autorizacao.Cadastrar(request);
            if (!response.StatusCadastro)
                return BadRequest(response.MensagemCadastro);
            return Ok(response);
        }

        /// <summary>
        /// Este EndPoint irá receber os dados de acesso do usuário, validá-los e após isso retornar informações da contam bem como um token de acesso
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel request)
        {
            //faz todas as verificações necessárias para garantir que o obejto será enviado corretamente
            if (request == null)
                return BadRequest("Request não pode ser nula!!!!!");
            if (request.Email == null)
                return BadRequest("email não pode ser nulo");
            if (request.Senha == null)
                return BadRequest("senha não pode ser nula");
            var response = _autorizacao.Login(request);
            if (!response.StatusLogin)
                return BadRequest(response.StatusMensagem);
            return Ok(response);
        }
    }
}

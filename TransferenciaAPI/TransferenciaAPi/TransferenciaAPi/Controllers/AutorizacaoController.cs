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
        /// <param name="Register"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel request) =>
            await _autorizacao.Register(request);

        /// <summary>
        /// Este EndPoint irá receber os dados de acesso do usuário, validá-los e após isso retornar informações da contam bem como um token de acesso
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request) =>
            await _autorizacao.Login(request);
    }
}

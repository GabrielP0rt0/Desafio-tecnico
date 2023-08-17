using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferenciaAPi.Constans;
using TransferenciaAPi.Models;
using TransferenciaAPi.Services;

namespace TransferenciaAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciaController : Controller
    {
  
        // Construtor responsável por TransferenciaService 
        private TransferenciaService _transferenciaService;
        public TransferenciaController(TransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        /// <summary>
        /// Endpoint responsável por retornar uma lista do histórico de transação do usuário desejado, esse método requer um token de acesso gerado no método de login ou cadastro encontrado
        /// em AutorizacaoService || AutorizacaoController
        /// </summary>
        /// <param name="ConsultStatement"></param>
        /// <returns></returns>
        [Authorize(Roles = Claims.level1)]
        [HttpGet("consult/statement/{idAccount}")] ///como a entrada é do tipo int deve ser enviada na URL
        public async Task<IActionResult> ConsultStatement([FromRoute] int idAccount) =>
            await _transferenciaService.ConsultStatement(idAccount);

        /// <summary>
        /// Endpoint responsável por gerar uma transação entre usuários e fazer seu cadastro em um banco de dados que serve como histórico, esse método 
        /// requer um token de acesso gerado no método de login ou cadastro encontrado em AutorizacaoService || AutorizacaoController
        /// </summary>
        /// <param name="Transfer"></param>
        /// <returns></returns>
        [Authorize(Roles = Claims.level1)]
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequestModel request) =>
            await _transferenciaService.Transfer(request);
    }
}

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
        /// <param name="ConsultarExtrato"></param>
        /// <returns></returns>
        [Authorize(Roles = Claims.camada1)]
        [HttpGet("consultar/extrato/{idConta}")] ///como a entrada é do tipo int deve ser enviada na URL
        public IActionResult ConsultarExtrato([FromRoute] int idConta)
        {
            if (idConta == 0)
                BadRequest("requisição inválida, insira a identificação da conta");
            var response = _transferenciaService.ConsultarExtrato(idConta);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint responsável por gerar uma transação entre usuários e fazer seu cadastro em um banco de dados que serve como histórico, esse método 
        /// requer um token de acesso gerado no método de login ou cadastro encontrado em AutorizacaoService || AutorizacaoController
        /// </summary>
        /// <param name="ConsultarExtrato"></param>
        /// <returns></returns>
        [Authorize(Roles = Claims.camada1)]
        [HttpPost("transferir")]
        public IActionResult Transferir([FromBody] TransferirRequestModel request)
        {
            //faz todas as verificações necessárias para garantir que o obejto será enviado corretamente
            if (request.IdContaDestino == 0)
                return BadRequest("A identificação da conta destino não deve ser nula");
            if (request.IdContaOrigem == 0)
                return BadRequest("A identificação da conta atual não deve ser nula");
            if (request.ValorTransferencia == 0)
                return BadRequest("O valor a ser transferido não pode ser nulo");
            var response = _transferenciaService.Transferir(request);
            if (!response.StatusTransferencia)
                return BadRequest(response.StatusMensagem);
            return Ok(response);
        }
    }
}

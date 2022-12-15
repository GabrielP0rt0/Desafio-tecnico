using System.Data;
using TransferenciaAPi.Models;
using TransferenciaAPi.Data;
using TransferenciaAPi.Data.Entidades;

namespace TransferenciaAPi.Services
{
    public class TransferenciaService
    {
        private DBContext _context;
        public TransferenciaService(DBContext dbContext)
        {
            _context = dbContext;
        }
        /// <summary>
        /// A função de consulta ao histórico busca os dados contendo todas as transações feitas a partir do ID de origem
        /// </summary>
        /// <param name="ConsultarExtrato"></param>
        /// <returns></returns>
        public List<Extrato>  ConsultarExtrato(int idPublicConta)
        {
            var extrato = _context.Extrato.Where(x => x.IdPublicOrigem.Equals(idPublicConta)).ToList();
            return extrato;
        }
        /// <summary>
        /// Esta função é utilizada para executar a transferência de saldo entre usuários. A função recebe as informações do requerimento:
        /// id de origem, id de destino e valor. Através desses itens é gerado uma operação a qual fica salva no banco de dados Extrato e
        /// as informações de saldo no banco de dados Conta dos usuários é atualizado
        /// </summary>
        /// <param name="Transferir"></param>
        /// <returns></returns>
        public TransferirResponseModel Transferir(TransferirRequestModel request)
        {
            var contaOrigem = _context.Conta.Where(x => x.IdUsuarioPrivate.Equals(request.IdPrivateOrigem)).FirstOrDefault();
            /*_context.Update(_context.Conta.Where(x => x.IdConta.Equals(request.IdContaOrigem);*/
            if (contaOrigem == null)
                return new()
                {
                    StatusMensagem = "Conta de origem não encontrada",
                    StatusTransferencia = false
                };
            var contaDestino = _context.Conta.Where(x => x.IdUsuarioPublic.Equals(request.IdPublicDestino)).FirstOrDefault();
            if (contaDestino == null)
                return new() 
                { 
                    StatusMensagem = "Conta de destino não encontrada", 
                    StatusTransferencia = false 
                };
            //verifica se é possivel fazer a transferência (se o usuário possui saldo o suficiente).
            decimal diferenca = contaOrigem.Saldo - request.ValorTransferencia;
            if (diferenca < 0)
                return new()
                {
                    StatusMensagem = "Valor indisponível para transferência",
                    StatusTransferencia = false
                };
            contaOrigem.Saldo = diferenca;
            contaDestino.Saldo = contaDestino.Saldo + request.ValorTransferencia;
            _context.Extrato.Add(new()
            {
                IdPublicOrigem = request.IdPublicOrigem,
                IdPublicDestino = request.IdPublicDestino,
                Valor = request.ValorTransferencia,
                DataHora = DateTime.Now
            });
            _context.SaveChanges();

            return new()
            {
                StatusMensagem = "Transferência realizada com sucesso",
                StatusTransferencia = true
            };

        }
    }
}

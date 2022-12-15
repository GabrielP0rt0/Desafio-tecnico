namespace TransferenciaAPi.Models
{
    public class TransferirRequestModel
    {
        /// <summary>
        /// Dados de requisição do método Transferir
        /// </summary>
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal ValorTransferencia { get; set; }
    }
}

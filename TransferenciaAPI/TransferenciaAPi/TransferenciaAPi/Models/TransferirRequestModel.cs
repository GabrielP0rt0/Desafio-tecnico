namespace TransferenciaAPi.Models
{
    public class TransferirRequestModel
    {
        /// <summary>
        /// Dados de requisição do método Transferir
        /// </summary>
        public int IdPrivateOrigem { get; set; }
        public int IdPublicDestino { get; set; }
        public int IdPublicOrigem { get; set; }
        public decimal ValorTransferencia { get; set; }
    }
}

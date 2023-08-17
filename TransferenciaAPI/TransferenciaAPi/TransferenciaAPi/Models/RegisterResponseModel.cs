namespace TransferenciaAPi.Models
{
    public class RegisterResponseModel
    {
        /// <summary>
        /// Dados de retorno do método de cadastro
        /// </summary>
        public string RegisterMessage { get; set; }
        public string Token { get; set; }
        public int IdUser { get; set; }
        public Guid TransferKey { get; set; }
        public int IdAccount { get; set; }
    }
}

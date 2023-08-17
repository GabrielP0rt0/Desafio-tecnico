using Org.BouncyCastle.Asn1.Crmf;

namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados de retorno do método Login
    /// </summary>
    public class LoginResponseModel
    {
        public int IdAccount { get; set; }
        public Guid TransferKey { get; set; }
        public int IdUser { get; set; }
        public string LoginMessage { get; set; }
        public decimal Amount { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
    }
}

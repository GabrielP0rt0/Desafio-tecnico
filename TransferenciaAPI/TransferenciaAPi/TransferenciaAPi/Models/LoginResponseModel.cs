using Org.BouncyCastle.Asn1.Crmf;

namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados de retorno do método Login
    /// </summary>
    public class LoginResponseModel
    {
        public int IdConta { get; set; }
        public string StatusMensagem { get; set; }
        public bool StatusLogin { get; set; }
        public decimal Saldo { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
    }

}

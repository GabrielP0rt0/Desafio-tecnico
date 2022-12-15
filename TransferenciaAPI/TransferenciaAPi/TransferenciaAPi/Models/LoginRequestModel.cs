using Microsoft.AspNetCore.Authentication;

namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados para requisição do método de login
    /// </summary>
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

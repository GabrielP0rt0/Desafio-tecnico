using Microsoft.AspNetCore.Authentication;
using TransferenciaAPi.Data.Entidades;

namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados para requisição do método de login
    /// </summary>
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public (bool isValid, string err) Validate()
        {
            if (Email == null)
                return (false, "Por favor preencha o email");

            if (Password == null)
                return (false, "Por favor preencha o email");

            return (true, "");
        }
    }
}

using TransferenciaAPi.Data.Entidades;
using TransferenciaAPi.Errors;

namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados que o método de cadastro espera na requisição
    /// </summary>
    public class RegisterRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public (bool isValid, string err) Validate()
        {
            if (Name == null)
                return (false, TransferenciaErrors.NameEmpty);

            if (Email == null)
                return (false, TransferenciaErrors.EmailEmpty);

            if (Password == null)
                return (false, TransferenciaErrors.PasswordEmpty);

            return (true, string.Empty);
        }

    }
}

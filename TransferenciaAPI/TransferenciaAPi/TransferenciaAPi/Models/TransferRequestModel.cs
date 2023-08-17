using Microsoft.AspNetCore.Identity;
using TransferenciaAPi.Errors;

namespace TransferenciaAPi.Models
{
    public class TransferRequestModel
    {
        /// <summary>
        /// Dados de requisição do método Transferir
        /// </summary>
        public int IdUser { get; set; }
        public Guid KeyDestiny { get; set; }
        public decimal Amount { get; set; }
        public (bool isValid, string err) Validate()
        {
            if (IdUser <= 0) 
                return(false, TransferenciaErrors.UserEmpty);
            if (KeyDestiny == null)
                return (false, TransferenciaErrors.TransferKeyEmpty);
            if (Amount <= 0) 
                return (false, TransferenciaErrors.InvalidAmount);
            return (true, "");
        }
    }
}
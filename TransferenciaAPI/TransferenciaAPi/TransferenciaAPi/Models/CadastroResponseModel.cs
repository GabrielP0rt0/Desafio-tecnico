namespace TransferenciaAPi.Models
{
    public class CadastroResponseModel
    {
        /// <summary>
        /// Dados de retorno do método de cadastro
        /// </summary>
        public string MensagemCadastro { get; set; }
        public string Token { get; set; }
        public bool StatusCadastro { get; set; }
        public int IdContaPublic { get; set; }
        public int IdContaPrivate { get; set; }
        public int IdConta { get; set; }
    }
}

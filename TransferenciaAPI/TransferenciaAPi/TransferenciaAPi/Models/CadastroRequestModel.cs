namespace TransferenciaAPi.Models
{
    /// <summary>
    /// Dados que o método de cadastro espera na requisição
    /// </summary>
    public class CadastroRequestModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

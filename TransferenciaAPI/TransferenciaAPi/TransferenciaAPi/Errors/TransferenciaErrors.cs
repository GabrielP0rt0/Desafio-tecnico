namespace TransferenciaAPi.Errors
{
    public static class TransferenciaErrors
    {
        public static string UserExist = "Usuário ja cadastrado!";
        public static string NameEmpty = "Campo nome vazio";
        public static string EmailEmpty = "Campo email vazio";
        public static string PasswordEmpty = "Campo senha vazio";
        public static string LoginError = "Email ou senha incorretos";
        public static string TransferKeyEmpty = "Chave de transferência vazia";
        public static string UserEmpty = "Campo usuário vazio";
        public static string InvalidAmount = "Valor para transferência inválido";
        public static string AccountNotFound = "Conta não encontrada";
        public static string DestinyAccountNotFound = "Conta destino não encontrada";
        public static string InsufficientBalance = "Saldo insuficiente";
    }
}

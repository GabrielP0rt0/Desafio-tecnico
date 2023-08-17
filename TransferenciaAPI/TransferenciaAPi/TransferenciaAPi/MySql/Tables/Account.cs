using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferenciaAPi.Data.Entidades
{
    /// <summary>
    /// Inserindo a tabela de Contas onde teremos os dados mais "superficiais" dos usuários
    /// </summary>
    [Table("Account")]
    public class Account
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_user")]
        public int IdUser { get; set; }
        [Column("transfer_key")]
        public Guid TransferKey { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferenciaAPi.Data.Entidades
{
    /// <summary>
    /// Tabela de Extrato a qual teremos o histórico de todas as transações realizadas
    /// </summary>
    [Table("Statement")]
    public class Statement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("key_source_account")]
        public Guid KeySourceAccount { get; set; }
        [Column("key_destination_account")]
        public Guid KeyDestinationAccount { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }
    }
}

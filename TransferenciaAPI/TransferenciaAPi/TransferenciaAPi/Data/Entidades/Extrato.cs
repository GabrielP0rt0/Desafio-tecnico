using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferenciaAPi.Data.Entidades
{
    /// <summary>
    /// Tabela de Extrato a qual teremos o histórico de todas as transações realizadas
    /// </summary>
    [Table("Extrato")]
    public class Extrato
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_public_origem")]
        public int IdPublicOrigem { get; set; }
        [Column("id_public_destino")]
        public int IdPublicDestino { get; set; }
        [Column("valor")]
        public decimal Valor { get; set; }
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
    }
}

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
        [Column("id_usuario_origem")]
        public int IdUsuarioOrigem { get; set; }
        [Column("id_usuario_destino")]
        public int IdUsuarioDestino { get; set; }
        [Column("valor")]
        public decimal Valor { get; set; }
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
    }
}

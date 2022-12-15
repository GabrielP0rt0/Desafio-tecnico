using Microsoft.EntityFrameworkCore;
using TransferenciaAPi.Data.Entidades;

namespace TransferenciaAPi.Data
{
    /// <summary>
    /// Configuração do banco de dados, inserirndo as tabelas que utilizaremos
    /// </summary>
    public class DBContext : DbContext 
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Extrato> Extrato { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}

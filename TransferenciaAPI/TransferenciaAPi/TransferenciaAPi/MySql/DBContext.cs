using Microsoft.EntityFrameworkCore;
using TransferenciaAPi.Data.Entidades;

namespace TransferenciaAPi.Data
{
    /// <summary>
    /// Configuração do banco de dados, inserirndo as tabelas que utilizaremos
    /// </summary>
    public class DBContext : DbContext 
    {
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Statement> Statement { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}

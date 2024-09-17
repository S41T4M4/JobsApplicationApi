using Microsoft.EntityFrameworkCore;
using JobApplication.Domain.Models;
namespace JobApplication.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<Vagas> Vaga { get; set; }
        public DbSet<Candidaturas> Candidatura { get; set; }
        public DbSet<Empresas> Empresa { get; set; }
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
           : base(options)
        {
        }
        //String de conexão 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseNpgsql(
             "Server=database-postgres;" +
             "Port=5432;Database=job_application;" +
             "User Id=postgres;" +
             "Password=Staff4912;");


    }
}
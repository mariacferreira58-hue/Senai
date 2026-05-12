using Microsoft.EntityFrameworkCore;
using Senai.Models;
namespace Senai.Data
{
    public class GerenciaContext : DbContext
    {
        public GerenciaContext(DbContextOptions<GerenciaContext> options) : base(options)
        {
        
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}

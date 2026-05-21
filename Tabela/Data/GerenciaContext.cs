using Senai.Models;
using Microsoft.EntityFrameworkCore;

namespace Senai.Data
{
    public class GerenciaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Tarefa> Tarefas { get; set; }

        public GerenciaContext(DbContextOptions<GerenciaContext> options) : base(options)
        {

        }
    }
}
using Microsoft.EntityFrameworkCore;
using PedidosDaLoja.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PedidosDaLoja.Infrastructure.Persistence
{
    /// <summary>
    /// Clsse que realiza os tratamentos direto com o DB.
    /// Gerencia a persistência das entidades Order e Product.
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Configuração do modelo de dados.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

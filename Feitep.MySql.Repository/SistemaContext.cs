using Feitep.MySql.Models;
using Microsoft.EntityFrameworkCore;

namespace Feitep.MySql.Repository;

public class SistemaContext : DbContext
{
    public DbSet<Cliente>? Clientes { get; set; }

    public SistemaContext(DbContextOptions<SistemaContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(cliente => cliente.Id);

            entity.Property(cliente => cliente.Id)
                  .HasColumnName("id")
                  .ValueGeneratedOnAdd();

            entity.Property(cliente => cliente.Nome)
                  .HasColumnName("nome")
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(cliente => cliente.DataNascimento)
                  .HasColumnName("data_nascimento")
                  .IsRequired();
        });
    }
}

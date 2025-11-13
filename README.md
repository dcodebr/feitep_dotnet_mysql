# MySql With dotnet C# Console

Projeto criado com os alunos da UniFeitep, na disciplina Tópicos Avançados de Programação, com a finalidade de interligar um app console desenvolvido em C#.NET a um banco de dados MySQL usando EntityFramework Core.

## Dependências do Projeto

As dependências do projeto em dotnet são:

```sh
Microsoft.EntityFrameworkCore 8.0.15
Microsoft.EntityFrameworkCore.Design 8.0.15
Microsoft.EntityFrameworkCore.Proxies 8.0.15
Microsoft.EntityFrameworkCore.Relational 8.0.15
Microsoft.EntityFrameworkCore.Tools 8.0.15
Pomelo.EntityFrameworkCore.MySql 8.0.3
```

## Comandos do Migrations

Instalar donet-ef:

```sh
dotnet tool install --global dotnet-ef --version 8.0.15
```

Definir pasta do projeto Feitep.MySql.Repository como contexto do console:

```sh
cd .\Feitep.MySql.Repository\
```

Incluir Migration:
```sh
dotnet ef migrations add "Inicial Migration"
```

Atualizar banco de dados:
```sh
dotnet ef database update
```

## Trechos importantes

SistemaContext.cs:

```csharp
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
```

SistemaContextFactory.cs:

```csharp
public class SistemaContextFactory : IDesignTimeDbContextFactory<SistemaContext>
{
    public SistemaContext CreateDbContext(string[]? args = null)
    {
        var builder = new DbContextOptionsBuilder<SistemaContext>();
        var connectionString = "server=localhost;user=root;password=root;database=sistemadb;";

        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new SistemaContext(builder.Options);
    }

}
```

---

Desenvolvido por [Alex Rocha](https://www.linkedin.com/in/alexdiegorocha/).
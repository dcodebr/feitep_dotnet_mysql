using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Feitep.MySql.Repository;

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

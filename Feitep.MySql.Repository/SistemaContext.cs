using Feitep.MySql.Models;
using Microsoft.EntityFrameworkCore;

namespace Feitep.MySql.Repository;

public class SistemaContext : DbContext
{
    public DbSet<Cliente>? Clientes { get; set; }
}

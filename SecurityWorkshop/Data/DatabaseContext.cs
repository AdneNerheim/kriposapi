using Microsoft.EntityFrameworkCore;
using SecurityWorkshop.Models;

namespace SecurityWorkshop.Data;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration Configuration;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    public DbSet<User> User { get; set; } = null!;
    public DbSet<TopSecret> TopSecret { get; set;} = null!;
}
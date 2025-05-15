using finshark_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions myDbContextOptions) : base(myDbContextOptions)
    {
        
    }
    
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
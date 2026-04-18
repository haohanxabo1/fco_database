using fco_database.Models;
using Microsoft.EntityFrameworkCore;

namespace fco_database.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<SeasonCardModel> season_cards { get; set; }
}
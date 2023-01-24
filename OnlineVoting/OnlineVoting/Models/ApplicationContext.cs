using Microsoft.EntityFrameworkCore;

namespace OnlineVoting.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Candidate> Candidates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
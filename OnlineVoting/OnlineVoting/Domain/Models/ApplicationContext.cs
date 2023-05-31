using Microsoft.EntityFrameworkCore;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.Models.Entities;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PersonalInformation> PersonalInformations { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
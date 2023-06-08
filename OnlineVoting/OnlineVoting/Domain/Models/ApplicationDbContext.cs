using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.Models.Entities;
using OnlineVoting.Models.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Domain.Administration.Entities;
using OnlineVoting.Domain.Models.Identity.Entities;
using SystemOfWidget.Domain.Identity.Entities;

namespace OnlineVoting.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    
    // Identity
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<RefreshSession> RefreshSessions { get; set; }
    public DbSet<LoginMark> LoginMarks { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<PersonalInformation> PersonalInformations { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService,
        IDateTimeService dateTimeService, IConfiguration configuration)
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
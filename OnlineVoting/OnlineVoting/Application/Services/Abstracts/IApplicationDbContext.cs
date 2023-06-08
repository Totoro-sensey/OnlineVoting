using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OnlineVoting.Domain.Administration.Entities;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.Domain.Models.Identity.Entities;
using OnlineVoting.Models.Entities;
using OnlineVoting.Models.Identity.Entities;
using SystemOfWidget.Domain.Identity.Entities;

namespace OnlineVoting.Application.Services.Abstracts;

public interface IApplicationDbContext
{
    // Identity
     DbSet<ApplicationUser> Users { get; set; }
     DbSet<ApplicationRole> Roles { get; set; }
     DbSet<RefreshSession> RefreshSessions { get; set; }
     DbSet<LoginMark> LoginMarks { get; set; }
     DbSet<Candidate> Candidates { get; set; }
     DbSet<PersonalInformation> PersonalInformations { get; set; }
     DbSet<Vote> Votes { get; set; }
     
     DatabaseFacade Database { get; }
     
     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
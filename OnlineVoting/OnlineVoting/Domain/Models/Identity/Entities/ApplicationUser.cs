using Microsoft.AspNetCore.Identity;
using OnlineVoting.Domain.Common;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.Domain.Models.Identity.Entities;

namespace OnlineVoting.Models.Identity.Entities;

/// <summary>
/// User
/// </summary>
public class ApplicationUser : IdentityUser, IEntityWithId<string>
{
    /// <summary>
    /// Voter name
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public byte[] Password { get; set; }
    
    /// <summary>
    /// Salt
    /// </summary>
    public byte[] Salt { get; set; }
    
    // Navigation settings
    
    /// <summary>
    /// Navigation settings - Personal Information
    /// </summary>
    public PersonalInformation PersonalInformation { get; set; }
    
    /// <summary>
    /// Vote
    /// </summary>
    public Vote? Vote { get; set; }
        
    /// <summary>
    /// Навигационное свойство - Сессия для обновления JWT
    /// </summary>
    public List<RefreshSession> RefreshSessions { get; } = new();
}
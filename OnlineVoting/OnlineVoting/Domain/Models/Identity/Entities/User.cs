using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.Models.Common;

namespace OnlineVoting.Models.Identity.Entities;

/// <summary>
/// User
/// </summary>
public class User 
{
    /// <summary>
    /// Identificator
    /// </summary>
    public Guid Id { get; set; }
    
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
    public Vote Vote { get; set; }
}
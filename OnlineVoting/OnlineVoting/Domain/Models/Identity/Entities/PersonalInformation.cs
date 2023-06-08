using OnlineVoting.Models.Common;

namespace OnlineVoting.Models.Identity.Entities;

/// <summary>
/// Personal Information
/// </summary>
public class PersonalInformation : BaseEntity
{
    /// <summary>
    /// User Id
    /// </summary>
    public string ApplicationUserId { get; set; }
    
    /// <summary>
    /// First Name
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last Name
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Birthday
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// Passport series
    /// </summary>
    public string PassportSeries { get; set; }
    
    /// <summary>
    /// Passport number
    /// </summary>
    public string PassportNumber { get; set; }
    
    /// <summary>
    /// Insurance number of an individual personal account
    /// </summary>
    public string SNILS { get; set; }
}
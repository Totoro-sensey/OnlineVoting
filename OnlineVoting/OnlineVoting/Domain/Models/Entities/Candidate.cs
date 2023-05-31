using OnlineVoting.Models.Common;

namespace OnlineVoting.Models.Entities;

/// <summary>
/// Entity "Candidate"
/// </summary>
public class Candidate : BaseEntity
{
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Information
    /// </summary>
    public string Information { get; set; }
 
    /// <summary>
    /// Age
    /// </summary>
    public long Age { get; set; }
    
    /// <summary>
    /// Deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}
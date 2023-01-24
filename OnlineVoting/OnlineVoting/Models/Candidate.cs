namespace OnlineVoting.Models;

/// <summary>
/// Entity "Candidate"
/// </summary>
public class Candidate
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; }
 
    /// <summary>
    /// Age
    /// </summary>
    public long Age { get; set; }
    
    /// <summary>
    /// Deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}
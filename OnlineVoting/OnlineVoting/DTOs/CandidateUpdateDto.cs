using OnlineVoting.Mappings;
using OnlineVoting.Models;

namespace OnlineVoting.DTOs;

public class CandidateUpdateDto : IMapTo<Candidate>
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
}
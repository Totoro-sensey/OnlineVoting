using OnlineVoting.Mappings;
using OnlineVoting.Models;

namespace OnlineVoting.DTOs;

public class CandidateCreateDto : IMapTo<Candidate>
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
    /// Age
    /// </summary>
    public long Age { get; set; }
}
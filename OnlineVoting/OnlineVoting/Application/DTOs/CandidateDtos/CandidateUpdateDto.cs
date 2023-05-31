using OnlineVoting.Mappings;
using OnlineVoting.Models;
using OnlineVoting.Models.Entities;

namespace OnlineVoting.DTOs.CandidateDtos;

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
    
    /// <summary>
    /// Information
    /// </summary>
    public string Information { get; set; }
}
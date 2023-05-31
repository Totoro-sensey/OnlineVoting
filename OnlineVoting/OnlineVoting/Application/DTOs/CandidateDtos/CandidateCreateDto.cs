using OnlineVoting.Mappings;
using OnlineVoting.Models;
using OnlineVoting.Models.Entities;

namespace OnlineVoting.DTOs.CandidateDtos;

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
    
    /// <summary>
    /// Information
    /// </summary>
    public string Information { get; set; }

    /// <summary>
    /// Deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
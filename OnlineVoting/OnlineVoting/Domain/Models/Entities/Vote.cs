using OnlineVoting.Models.Common;
using OnlineVoting.Models.Entities;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Domain.Models.Entities;

/// <summary>
/// Entity "Vote"
/// </summary>
public class Vote : BaseEntity
{
    /// <summary>
    /// Vote date
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Users identifier
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Candidates identifier
    /// </summary>
    public Guid CandidateId { get; set; }
    
    //Navigation settings
    
    /// <summary>
    /// Candidate
    /// </summary>
    public Candidate Candidate { get; set; }
    
    /// <summary>
    /// User
    /// </summary>
    public User User { get; set; }
}
using AutoMapper;
using OnlineVoting.Mappings;
using OnlineVoting.Models;

namespace OnlineVoting.DTOs;

public class CandidateDto : IMapTo<Candidate>
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
    /// Deleted
    /// </summary>
    public bool IsDeleted { get; set; }
    
    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Candidate, CandidateDto>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Age))
            .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => s.IsDeleted));

        profile.CreateMap<CandidateDto, Candidate>();
    }
}
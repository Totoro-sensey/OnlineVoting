using AutoMapper;
using OnlineVoting.Mappings;
using OnlineVoting.Models.Entities;

namespace OnlineVoting.DTOs.CandidateDtos;

public class CandidateVm : IMapFrom<Candidate>
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Age
    /// </summary>
    public string Age { get; set; }
    
    /// <summary>
    /// Information
    /// </summary>
    public string Information { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Candidate, CandidateVm>()
            .ForMember(i => i.Name,
                opt => opt.MapFrom(j => $"{j.FirstName} { j.LastName}"))
            .ForMember(i => i.Age,
                opt => opt.MapFrom(j => $"{j.Age}"));
    }
}
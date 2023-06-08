using AutoMapper;
using OnlineVoting.Mappings;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.DTOs.UserDtos.GetVm;

public class UserVm : IMapFrom<ApplicationUser>
{
    /// <summary>
    /// Identificator
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Voter name
    /// </summary>
    public string Login { get; set; }
    
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
    /// Insurance number of an individual personal account
    /// </summary>
    public string SNILS { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, UserVm>()
            .ForMember(i => i.FirstName,
                opt => opt.MapFrom(j => j.PersonalInformation.FirstName))
            .ForMember(i => i.LastName,
                opt => opt.MapFrom(j => j.PersonalInformation.LastName))
            .ForMember(i => i.SNILS,
                opt => opt.MapFrom(j => j.PersonalInformation.SNILS))
            .ForMember(i => i.Birthday,
                opt => opt.MapFrom(j => j.PersonalInformation.Birthday));
    }
}
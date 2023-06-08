using AutoMapper;
using OnlineVoting.DTOs.UserDtos.CreateDto;
using OnlineVoting.Mappings;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.Application.DTOs.UserDtos.CreateDto;

public class ApplicationUserDto : IMapTo<ApplicationUser>
{
    /// <summary>
    /// Voter name
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Personal information model
    /// </summary>
    public PersonalInformationDto PersonalInformation { get; set; }
    
    /// <summary>
    /// Список наименований ролей
    /// </summary>
    public IEnumerable<string> Roles { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUserDto, ApplicationUser>()
            .ForMember(i => i.UserName,
                opt => opt.MapFrom(j => j.Login));
    }
}
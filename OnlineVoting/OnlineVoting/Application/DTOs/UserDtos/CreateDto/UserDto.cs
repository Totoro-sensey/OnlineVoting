using OnlineVoting.Mappings;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.DTOs.UserDtos.CreateDto;

public class UserDto : IMapTo<User>
{
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Personal information model
    /// </summary>
    public PersonalInformationDto PersonalInformation { get; set; }
}
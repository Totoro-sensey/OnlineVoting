using OnlineVoting.Mappings;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.DTOs.UserDtos.UpdateDto;

public class UserDto : IMapTo<ApplicationUser>
{
    /// <summary>
    /// Identifier
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Voter name
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public byte[] Password { get; set; }
    
    /// <summary>
    /// Personal Information model
    /// </summary>
    public PersonalInformationDto PersonalInformation { get; set; }
}
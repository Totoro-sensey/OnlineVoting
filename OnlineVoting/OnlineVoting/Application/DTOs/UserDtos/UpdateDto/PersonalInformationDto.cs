using OnlineVoting.Mappings;
using OnlineVoting.Models.Identity.Entities;

namespace OnlineVoting.DTOs.UserDtos.UpdateDto;

public class PersonalInformationDto : IMapTo<PersonalInformation>
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }
    
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
    /// Passport series
    /// </summary>
    public string PassportSeries { get; set; }
    
    /// <summary>
    /// Passport number
    /// </summary>
    public string PassportNumber { get; set; }
    
    /// <summary>
    /// Insurance number of an individual personal account
    /// </summary>
    public string SNILS { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using OnlineVoting.Application.DTOs.UserDtos.CreateDto;
using OnlineVoting.Controllers.Common;
using OnlineVoting.DTOs.UserDtos.CreateDto;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Services.Abstracts;
using UpdateDto = OnlineVoting.DTOs.UserDtos.UpdateDto.UserDto;

namespace OnlineVoting.Controllers;

public class UserController : ApiMediatorController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<string> SignIn([FromBody] ApplicationUserDto dto, CancellationToken cancellationToken)
        => await _userService.Create(dto, cancellationToken);
    
    [HttpPost]
    public async Task<string> SignUp([FromBody] ApplicationUserDto dto, CancellationToken cancellationToken)
        => await _userService.Create(dto, cancellationToken);
    
    [HttpGet]
    public async Task<UserVm> Get(string id, CancellationToken cancellationToken)
        => await _userService.Get(id, cancellationToken);
    
    [HttpPost]
    public async Task<long> Vote(string userId, long candidateId, CancellationToken cancellationToken)
        => await _userService.Vote(userId, candidateId, cancellationToken);
}
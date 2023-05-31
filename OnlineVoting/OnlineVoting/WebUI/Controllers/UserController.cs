using Microsoft.AspNetCore.Mvc;
using OnlineVoting.Controllers.Common;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Services.Abstracts;
using CreateDto = OnlineVoting.DTOs.UserDtos.CreateDto.UserDto;
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
    public async Task<Guid> SignIn([FromBody] CreateDto dto, CancellationToken cancellationToken)
        => await _userService.Create(dto, cancellationToken);
    
    [HttpPost]
    public async Task<Guid> SignUp([FromBody] CreateDto dto, CancellationToken cancellationToken)
        => await _userService.Create(dto, cancellationToken);
    
    [HttpGet]
    public async Task<UserVm> Get(Guid id, CancellationToken cancellationToken)
        => await _userService.Get(id, cancellationToken);
    
    [HttpPost]
    public async Task<long> Vote(Guid userId, Guid candidateId, CancellationToken cancellationToken)
        => await _userService.Vote(userId, candidateId, cancellationToken);
}
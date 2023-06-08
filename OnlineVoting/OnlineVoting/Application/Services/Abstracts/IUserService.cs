using MediatR;
using OnlineVoting.Application.DTOs.UserDtos.CreateDto;
using OnlineVoting.DTOs.UserDtos.CreateDto;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Models.Identity.Entities;
using UpdateDto = OnlineVoting.DTOs.UserDtos.UpdateDto.UserDto;

namespace OnlineVoting.Services.Abstracts;

public interface IUserService
{
    public Task<string> Create(ApplicationUserDto dto, CancellationToken cancellationToken);
    
    public Task<Unit> Delete(string id, CancellationToken cancellationToken);
    
    public Task<Unit> Restore(string id, CancellationToken cancellationToken);
    
    public Task<Unit> Update(UpdateDto dto, CancellationToken cancellationToken);
    
    public Task<UserVm> Get(string id, CancellationToken cancellationToken);
    
    public Task<long> Vote(string userId, long candidateId, CancellationToken cancellationToken);
}
using MediatR;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Models.Identity.Entities;
using UpdateDto = OnlineVoting.DTOs.UserDtos.UpdateDto.UserDto;
using CreateDto = OnlineVoting.DTOs.UserDtos.CreateDto.UserDto;

namespace OnlineVoting.Services.Abstracts;

public interface IUserService
{
    public Task<Guid> Create(CreateDto dto, CancellationToken cancellationToken);
    
    public Task<Unit> Delete(Guid id, CancellationToken cancellationToken);
    
    public Task<Unit> Restore(Guid id, CancellationToken cancellationToken);
    
    public Task<Unit> Update(UpdateDto dto, CancellationToken cancellationToken);
    
    public Task<UserVm> Get(Guid id, CancellationToken cancellationToken);
    
    public Task<long> Vote(Guid userId, Guid candidateId, CancellationToken cancellationToken);
}
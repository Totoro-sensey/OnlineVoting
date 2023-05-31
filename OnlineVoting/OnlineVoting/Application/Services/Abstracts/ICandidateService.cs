using MediatR;
using OnlineVoting.DTOs.CandidateDtos;
using OnlineVoting.Models.Entities;

namespace OnlineVoting.Services.Abstracts;

public interface ICandidateService
{
    public Task<long> Create(CandidateCreateDto createDto, CancellationToken cancellationToken);
    
    public Task<Unit> Delete(long id, CancellationToken cancellationToken);
    
    public Task<Unit> Restore(long id, CancellationToken cancellationToken);
    
    public Task<Unit> Update(CandidateUpdateDto dto, CancellationToken cancellationToken);
    
    public Task<Candidate> Get(long id, CancellationToken cancellationToken);
    
    public Task<List<CandidateVm>> GetList(CancellationToken cancellationToken);
}
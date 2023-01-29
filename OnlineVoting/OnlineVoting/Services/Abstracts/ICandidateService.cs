using MediatR;
using OnlineVoting.DTOs;
using OnlineVoting.Models;

namespace OnlineVoting.Services.Abstracts;

public interface ICandidateService
{
    public Task<long> CreateCandidate(CandidateCreateDto createDto, CancellationToken cancellationToken);
    
    public Task<Unit> DeleteCandidate(long id, CancellationToken cancellationToken);
    
    public Task<Unit> RestoreCandidate(long id, CancellationToken cancellationToken);
    
    public Task<Unit> UpdateCandidate(CandidateUpdateDto dto, CancellationToken cancellationToken);
    
    public Task<Candidate> GetCandidate(long id, CancellationToken cancellationToken);
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVoting.Controllers.Common;
using OnlineVoting.DTOs;
using OnlineVoting.Models;
using OnlineVoting.Services.Abstracts;

namespace OnlineVoting.Controllers;

public class CandidateController : ApiMediatorController
{
    private readonly ICandidateService _candidateService;

    public CandidateController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }
    
    [HttpPost]
    public async Task<long> Create(CandidateCreateDto createDto, CancellationToken cancellationToken)
        => await _candidateService.CreateCandidate(createDto, cancellationToken);
    
    [HttpDelete]
    public async Task<Unit> Delete(long id, CancellationToken cancellationToken)
        => await _candidateService.DeleteCandidate(id, cancellationToken);
    
    [HttpPost]
    public async Task<Unit> Restore(long id, CancellationToken cancellationToken)
        => await _candidateService.RestoreCandidate(id, cancellationToken);
    
    [HttpPost]
    public async Task<Unit> Update(CandidateUpdateDto dto, CancellationToken cancellationToken)
        => await _candidateService.UpdateCandidate(dto, cancellationToken);

    [HttpGet]
    public async Task<Candidate> Get(long id, CancellationToken cancellationToken)
        => await _candidateService.GetCandidate(id, cancellationToken);
   
}
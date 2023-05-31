using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVoting.Controllers.Common;
using OnlineVoting.DTOs.CandidateDtos;
using OnlineVoting.Models.Entities;
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
    public async Task<long> Create([FromBody] CandidateCreateDto createDto, CancellationToken cancellationToken)
        => await _candidateService.Create(createDto, cancellationToken);
    
    [HttpDelete]
    public async Task<Unit> Delete(long id, CancellationToken cancellationToken)
        => await _candidateService.Delete(id, cancellationToken);
    
    [HttpPost]
    public async Task<Unit> Restore(long id, CancellationToken cancellationToken)
        => await _candidateService.Restore(id, cancellationToken);
    
    [HttpPost]
    public async Task<Unit> Update([FromBody] CandidateUpdateDto dto, CancellationToken cancellationToken)
        => await _candidateService.Update(dto, cancellationToken);

    [HttpGet]
    public async Task<Candidate> Get(long id, CancellationToken cancellationToken)
        => await _candidateService.Get(id, cancellationToken);
    
    [HttpGet]
    public async Task<List<CandidateVm>> GetList(CancellationToken cancellationToken)
        => await _candidateService.GetList(cancellationToken);
}
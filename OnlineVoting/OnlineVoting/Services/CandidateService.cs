using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.DTOs;
using OnlineVoting.Models;
using OnlineVoting.Services.Abstracts;

namespace OnlineVoting.Services;

public class CandidateService : ICandidateService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public CandidateService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<long> CreateCandidate(CandidateCreateDto createDto, CancellationToken cancellationToken)
    {
         var candidate = _mapper.Map<Candidate>(createDto);
        
        await _context.Candidates.AddAsync(candidate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }

    public async Task<Unit> DeleteCandidate(long id, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException("Not found candidate");

        candidate.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    public async Task<Unit> RestoreCandidate(long id, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException("Not found candidate");

        candidate.IsDeleted = false;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    public async Task<Unit> UpdateCandidate(CandidateUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == dto.Id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(Candidate)}");
        
        _mapper.Map(dto, entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;    
    }

    public async Task<Candidate> GetCandidate(long id, CancellationToken cancellationToken = default)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException($"Not found");
        
        return candidate;
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.DTOs.CandidateDtos;
using OnlineVoting.Models;
using OnlineVoting.Models.Entities;
using OnlineVoting.Services.Abstracts;

namespace OnlineVoting.Services.Candidates;

public class CandidateService : ICandidateService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public CandidateService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<long> Create(CandidateCreateDto createDto, CancellationToken cancellationToken)
    {
         var candidate = _mapper.Map<Candidate>(createDto);
        
        await _context.Candidates.AddAsync(candidate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }

    public async Task<Unit> Delete(long id, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException("Not found candidate");

        candidate.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    public async Task<Unit> Restore(long id, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException("Not found candidate");

        candidate.IsDeleted = false;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    public async Task<Unit> Update(CandidateUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == dto.Id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(Candidate)}");
        
        _mapper.Map(dto, entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;    
    }

    public async Task<Candidate> Get(long id, CancellationToken cancellationToken = default)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException($"Not found");
        
        return candidate;
    }
    
    public async Task<List<CandidateVm>> GetList(CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .Where(i => !i.IsDeleted)
            .OrderBy(i => i.LastName)
            .ProjectTo<CandidateVm>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        if (candidate is null)
            throw new NullReferenceException($"Not found");
        
        return candidate;
    }
}
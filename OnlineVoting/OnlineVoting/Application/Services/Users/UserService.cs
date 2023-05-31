using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Models;
using OnlineVoting.Models.Identity.Entities;
using OnlineVoting.Services.Abstracts;
using UpdateDto = OnlineVoting.DTOs.UserDtos.UpdateDto.UserDto;
using CreateDto = OnlineVoting.DTOs.UserDtos.CreateDto.UserDto;

namespace OnlineVoting.Services.Users;

public class UserService : IUserService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public UserService(ApplicationContext context, IMapper mapper, IPasswordService passwordService)
    {
        _context = context;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<Guid> Create(CreateDto dto, CancellationToken cancellationToken)
    {
        var user = new User();
        
        byte[] salt = null;
        user.Password = _passwordService.HashPassword(Encoding.UTF8.GetBytes(dto.Password), ref salt);
        user.Salt = salt;
        user.PersonalInformation = new PersonalInformation();
        user.PersonalInformation.Birthday = dto.PersonalInformation.Birthday.Date;
        user.Login = dto.PersonalInformation.SNILS;
        
        user.PersonalInformation.FirstName = dto.PersonalInformation.FirstName;
        user.PersonalInformation.LastName = dto.PersonalInformation.LastName;
        user.PersonalInformation.SNILS = dto.PersonalInformation.SNILS;
        user.PersonalInformation.PassportSeries = dto.PersonalInformation.PassportSeries;
        user.PersonalInformation.PassportNumber = dto.PersonalInformation.PassportNumber;
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<Unit> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException(nameof(User));
        
        _context.Users.Remove(entity);
        
        return Unit.Value;
    }

    public Task<Unit> Restore(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Unit> Update(UpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FirstOrDefaultAsync(i => i.Id == dto.Id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(User)}");
        
        _mapper.Map(dto, entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;   
    }

    public async Task<UserVm> Get(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .ProjectTo<UserVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(i => i.Id == id , cancellationToken);
        
        if (user is null)
            throw new NullReferenceException($"Not found");
        
        return user;
    }
    
    public async Task<long> Vote(Guid userId, Guid candidateId, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .Include(i => i.Vote)
            .FirstOrDefaultAsync(i => i.Id == userId, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(User)}");
        
        if (entity.Vote is not null)
            throw new InvalidDataException($"User have a vote already");
        
        var vote = new Vote
        {
            CandidateId = candidateId,
            UserId = userId
        };
        
        await _context.Votes.AddAsync(vote, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return vote.Id;
    }
}
using System.Collections;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Application.DTOs.UserDtos.CreateDto;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Domain.Identity.Enums;
using OnlineVoting.Domain.Models.Entities;
using OnlineVoting.DTOs.UserDtos.CreateDto;
using OnlineVoting.DTOs.UserDtos.GetVm;
using OnlineVoting.Models;
using OnlineVoting.Models.Identity.Entities;
using OnlineVoting.Services.Abstracts;
using SystemOfWidget.Application.Common.Exceptions;
using SystemOfWidget.Domain.Identity.Entities;
using UpdateDto = OnlineVoting.DTOs.UserDtos.UpdateDto.UserDto;

namespace OnlineVoting.Services.Users;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IApplicationDbContext dbContext, IMapper mapper, IPasswordService passwordService, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordService = passwordService;
        _userManager = userManager;
    }

    public async Task<string> Create(ApplicationUserDto dto, CancellationToken cancellationToken)
    {
        byte[] salt = null;
        var personalInfo =  _mapper.Map<PersonalInformation>(dto.PersonalInformation);
        var password = _passwordService.HashPassword(Encoding.UTF8.GetBytes(dto.Password), ref salt);
        var entity = new ApplicationUser()
        {
            Login = dto.Login,
            Password = password,
            Salt = salt,
            UserName = dto.Login,
            PersonalInformation = personalInfo
        };

        await CreateUser(entity, dto.Password);
        await AddToRoles(entity, dto.Roles);
        
        return entity.Id;
    }
    private async Task CreateUser(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new BadRequestException(message: $"Не удалось создать пользователя {user.UserName}.");
    }

    private async Task AddToRoles(ApplicationUser user, IEnumerable<string> roles)
    {
        var result = await _userManager.AddToRolesAsync(user, roles);
        if (!result.Succeeded)
            throw new BadRequestException(message: $"Не удалось добавить роли к пользователю {user.UserName}.");
    }

    public async Task<Unit> Delete(string id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException(nameof(ApplicationUser));
        
        _dbContext.Users.Remove(entity);
        
        return Unit.Value;
    }

    public Task<Unit> Restore(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Unit> Update(UpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users
            .FirstOrDefaultAsync(i => i.Id == dto.Id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(ApplicationUser)}");
        
        _mapper.Map(dto, entity);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;   
    }

    public async Task<UserVm> Get(string id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .ProjectTo<UserVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(i => i.Id == id , cancellationToken);
        
        if (user is null)
            throw new NullReferenceException($"Not found");
        
        return user;
    }
    
    public async Task<long> Vote(string userId, long candidateId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users
            .Include(i => i.Vote)
            .FirstOrDefaultAsync(i => i.Id == userId, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"Not found {nameof(ApplicationUser)}");
        
        if (entity.Vote is not null)
            throw new InvalidDataException($"User have a vote already");
        
        var vote = new Vote
        {
            CandidateId = candidateId,
            ApplicationUserId = userId
        };
        
        await _dbContext.Votes.AddAsync(vote, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return vote.Id;
    }
}
namespace OnlineVoting.Application.Services.Abstracts
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
    }
}

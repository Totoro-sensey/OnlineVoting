namespace OnlineVoting.Application.Services.Abstracts
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}

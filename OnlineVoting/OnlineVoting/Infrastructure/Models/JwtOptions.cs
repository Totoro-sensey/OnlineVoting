namespace OnlineVoting.Infrastructure.Models
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int TokenExpiresMinutes { get; set; }
        public int RefreshTokenExpiresDays { get; set; }
        public string RefreshTokenCookieName { get; set; }
        public int RefreshTokenTtl { get; set; }
    }
}

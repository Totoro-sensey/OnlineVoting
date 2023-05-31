namespace OnlineVoting.Services.Abstracts;

public interface IPasswordService
{
    public byte[] HashPassword(byte[] password, ref byte[] salt);
    public bool VerifyHash(byte[] password, byte[] salt, byte[] hash);
}
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using OnlineVoting.Services.Abstracts;

namespace OnlineVoting.Services.Common;

public class PasswordService : IPasswordService
{
    public byte[] HashPassword(byte[] password, ref byte[] salt)
    {
        salt ??= CreateSalt();

        using var argon2 = new Argon2id(password);
        argon2.Salt = salt;
        argon2.DegreeOfParallelism = 8;
        argon2.Iterations = 4;
        argon2.MemorySize = 1024 * 128;

        return argon2.GetBytes(16);
    }
    private byte[] CreateSalt()
    {
        var buffer = new byte[16];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(buffer);
        return buffer;
    }

    public bool VerifyHash(byte[] password, byte[] salt, byte[] hash)
        => hash.SequenceEqual(HashPassword(password, ref salt));
}
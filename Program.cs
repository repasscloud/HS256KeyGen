using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HS256KeyGen;
class Program
{
    static async Task Main(string[] args)
    {
        int keyLength = 32; // Minimum key length for HS256
        string secretKey = await GenerateSecretKeyAsync(keyLength);
        
        Console.WriteLine($"Generated Secret Key: {secretKey}");
        Console.WriteLine($"Key Length: {secretKey.Length} characters");
    }

    static async Task<string> GenerateSecretKeyAsync(int length)
    {
        return await Task.Run(() =>
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{}|;:',.<>/?";
            StringBuilder key = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] byteBuffer = new byte[1];
                while (key.Length < length)
                {
                    rng.GetBytes(byteBuffer);
                    char character = (char)byteBuffer[0];
                    if (validChars.Contains(character))
                    {
                        key.Append(character);
                    }
                }
            }
            return key.ToString();
        });
    }
}

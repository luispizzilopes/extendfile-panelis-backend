using System.Text;

namespace ExtendFile.Panelis.Application.Extensions;

public static class StringExtensions
{
    public static string GenerateRandomPassword(this string source)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
        var random = new Random();
        var password = new StringBuilder();

        password.Append(chars[random.Next(0, 26)]); // Letra maiúscula
        password.Append(chars[random.Next(26, 52)]); // Letra minúscula
        password.Append(chars[random.Next(52, 62)]); // Número
        password.Append(chars[random.Next(62, chars.Length)]); // Caractere especial

        for (int i = 4; i < 12; i++)
        {
            password.Append(chars[random.Next(chars.Length)]);
        }

        return new string(password.ToString().ToCharArray().OrderBy(_ => random.Next()).ToArray());
    }
}

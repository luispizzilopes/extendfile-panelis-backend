using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Domain.Modules.User.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExtendFile.Panelis.Infrastructure.Services;

public class TokenJwtService : ITokenJwtService
{
  private readonly IConfiguration _configuration; 

    public TokenJwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenJwtInformationResponse CreateTokenUser(User user)
    {
        TokenJwtInformationResponse tokenJwtInformation = new();

        AssigningToken(tokenJwtInformation, user); 

        return tokenJwtInformation;
    }

    private void AssigningToken(TokenJwtInformationResponse tokenJwtInformation, User user) 
    {
        JwtSecurityTokenHandler handler = new();

        tokenJwtInformation.Token = handler.WriteToken(CreateJwtSecurityToken(user));
        tokenJwtInformation.DateExpiration = CreateExpirationDate(); 
    }

    private JwtSecurityToken CreateJwtSecurityToken(User user)
    {
        Claim[] claims = CreateUserClaims(user);
        SigningCredentials signingCredentials = CreateSigningCredentials();
        DateTime expirationDate = CreateExpirationDate();

        return new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expirationDate,
              signingCredentials: signingCredentials);
    }

    private Claim[] CreateUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id), 
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        if (!string.IsNullOrEmpty(user.Email) && user.Email.Contains("admin", StringComparison.OrdinalIgnoreCase))
            claims.Add(new Claim("admin", "true"));

        return claims.ToArray(); 
    }

    private SigningCredentials CreateSigningCredentials()
    {
        byte[] secretKeyEncoding = Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!);
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(secretKeyEncoding);
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256); 
    }

    private DateTime CreateExpirationDate()
    {
        var hoursExpiration = double.Parse(_configuration["TokenConfiguration:ExpireHours"]!);
        return DateTime.Now.AddHours(hoursExpiration);
    }
}
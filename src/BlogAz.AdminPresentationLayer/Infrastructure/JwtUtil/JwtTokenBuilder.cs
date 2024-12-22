using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogAz.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;

namespace DigiLearn.AdminPresentationLayer.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(User user, IConfiguration configuration)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role,"Admin"),
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
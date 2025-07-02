using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace kitchen_api.Database.Entities;

public class User
{
    [Key] 
    public int Id { get; set; }
    public string Username { get; set; } 
    public string Password { get; set; }
    
    public bool IsValidPassword(string password)
        => HashPassword(password) == Password;

    public static string HashPassword(string password) =>
        BitConverter
            .ToString(
                SHA512.HashData(
                    Encoding.UTF8.GetBytes(password)))
            .Replace("-", string.Empty);

    public static string GenerateToken(string userId)
    {
        var securityKey = new SymmetricSecurityKey("w7p9v2xH5JqL8tC0RyFzQa3mBnVuKdXG"u8.ToArray());
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), 
                ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
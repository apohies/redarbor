using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Redarbor.Application.Queries.GetEmployeeByCredentials;

namespace Redarbor.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthController(
        IMediator mediator,
        IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var employee = await _mediator.Send(new GetEmployeeByCredentialsQuery
        {
            Username = request.Username,
            Password = request.Password
        });

        if (employee is null)
            return Unauthorized(new { message = "Invalid credentials." });

        var token = GenerateToken(employee.Username, employee.Id);
        return Ok(new { token });
    }

    private string GenerateToken(string username, int id)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer:             _configuration["Jwt:Issuer"],
            audience:           _configuration["Jwt:Audience"],
            claims:             claims,
            expires:            DateTime.UtcNow.AddMinutes(
                                    int.Parse(_configuration["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
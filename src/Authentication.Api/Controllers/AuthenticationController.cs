using Authentication.Api.Models;
using Authentication.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthenticationController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult RequestToken(RequestToken request)
    {
        var user = GetUser(request.Username, request.Password);

        if (user is null)
            return NotFound(new { message = "User not found" });

        var token = _tokenService.GenerateToken(user);

        return Ok(new
        {
            token = token
        });
    }

    // Private Methods

    private UserModel? GetUser(string username, string password)
    {
        var users = SeedUsers();

        return users.FirstOrDefault(u =>
            u.Username == username &&
            u.Password == password
            );
    }

    private static List<UserModel> SeedUsers()
    {
        return new List<UserModel>
        {
            new UserModel { Id = new Guid("45eccd2b-c050-4a1c-b66c-e50a77086134"), Username = "tiago@email.com", Password = "dev@123", Role = "Admin" },
            new UserModel { Id = new Guid("55012284-e9a3-48b8-ab40-5f6ed491815d"), Username = "iran@email.com", Password = "dev@123", Role = "User", IsActive = true, PaidUser = true },
            new UserModel { Id = new Guid("c7df5c99-f0a7-4b55-bca5-9d049fa00f9f"), Username = "bruna@email.com", Password = "dev@123", Role = "User", IsActive = true },
        };
    }
}

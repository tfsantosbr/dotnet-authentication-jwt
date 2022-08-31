using Authentication.Api.Models;

namespace Authentication.Api.Services.Contracts;

public interface ITokenService
{
    string GenerateToken(UserModel user);
}

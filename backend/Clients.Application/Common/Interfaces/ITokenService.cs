using Users.Domain.Entities;

namespace Users.Application.Common.Interfaces;

public interface ITokenService
{
    string BuildToken(User client);
    bool ValidateToken(string key, string issuer, string audience, string token);
}

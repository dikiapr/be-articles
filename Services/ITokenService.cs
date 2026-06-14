using ArtikelKu.Api.Models;

namespace ArtikelKu.Api.Services;

public interface ITokenService
{
    string CreateToken(User user);
}

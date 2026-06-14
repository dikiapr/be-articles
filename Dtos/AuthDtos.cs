namespace ArtikelKu.Api.Dtos;

public record RegisterRequest(string Username, string Email, string Password);
public record LoginRequest(string Email, string Password);
public record UserDto(int Id, string Username, string Email);
public record AuthResponse(string Token, UserDto User);

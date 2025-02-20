namespace TodoCleanArchitecture.Infrastructure.Identity;

public record TokenModel(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);
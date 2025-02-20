using System.ComponentModel.DataAnnotations;

namespace TodoCleanArchitecture.Domain.Features.Users;

public class TokenInfo
{
    public int Id { get; set; }
 
    public required string Username { get; set; }  
 
    public required string RefreshToken { get; set; }  
 
    public DateTime ExpiredAt { get; set; }
}
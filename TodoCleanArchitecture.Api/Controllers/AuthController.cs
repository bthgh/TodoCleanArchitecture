using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TodoCleanArchitecture.Application.Abstractions.Identity;
using TodoCleanArchitecture.Domain.Features.Users;
using TodoCleanArchitecture.Infrastructure.Identity;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;

namespace TodoCleanArchitecture.Api.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IConfiguration configuration,
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    ApplicationDbContext dbContext)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User with this username is not registered");
            }

            bool isValidPassword = await userManager.CheckPasswordAsync(user, model.Password);
            if (isValidPassword == false)
            {
                return Unauthorized();
            }

            List<Claim> authClaims =
            [
                new(ClaimTypes.Name, user.UserName), 
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ];

            //Add Claims
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //Generate Tokens
            var token = tokenService.GenerateAccessToken(authClaims);
            string refreshToken = tokenService.GenerateRefreshToken();
            var expireAt = DateTime.UtcNow.AddDays(double.Parse(configuration["JWT:ExpireAt"]));
            
            TokenInfo? tokenInfo = dbContext.TokenInfos.FirstOrDefault(a => a.Username == user.UserName);
            if (tokenInfo == null)
            {
                tokenInfo = new TokenInfo
                {
                    Username = user.UserName,
                    RefreshToken = refreshToken,
                    ExpiredAt = expireAt
                };
                dbContext.TokenInfos.Add(tokenInfo);
            }
            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.ExpiredAt = expireAt;
            }

            await dbContext.SaveChangesAsync();

            return Ok(new TokenModel(token,refreshToken,expireAt));
        }
        catch (Exception ex)
        { 
            return Unauthorized();
        }
    }
    
    [HttpPost("token/refresh")]
    public async Task<IActionResult> Refresh(TokenModel tokenModel)
    {
        try
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
            var username = principal.Identity.Name;

            var tokenInfo = dbContext.TokenInfos.SingleOrDefault(u => u.Username == username);
            if (tokenInfo == null 
                || tokenInfo.RefreshToken != tokenModel.RefreshToken 
                || tokenInfo.ExpiredAt <= DateTime.UtcNow)
            {
                return BadRequest("Invalid refresh token. Please login again.");
            }

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            var expireAt = DateTime.UtcNow.AddDays(double.Parse(configuration["JWT:ExpireAt"]));
            tokenInfo.RefreshToken = newRefreshToken; 
            tokenInfo.ExpiredAt = expireAt;
            await dbContext.SaveChangesAsync();

            return Ok(new TokenModel(
            newAccessToken,
            newRefreshToken,
            expireAt
            ));
        }
        catch (Exception ex)
        { 
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost("token/revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke()
    {
        try
        {
            var username = User.Identity.Name;

            var user = dbContext.TokenInfos.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                return BadRequest();
            }

            user.RefreshToken = string.Empty;
            await dbContext.SaveChangesAsync();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
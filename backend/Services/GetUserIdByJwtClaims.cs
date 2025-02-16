using System.IdentityModel.Tokens.Jwt;
using Azure.Core;

namespace backend.Services;

public interface IJwtClaimsService
{
    string GetUserIdByJwt(string jwtToken);
}
public class GetUserIdByJwtClaims : IJwtClaimsService
{
    public string GetUserIdByJwt(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            throw new ArgumentException("JWT token is required.");
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new ArgumentException("Invalid JWT token.");
            }

            // Lấy claim với tên "nameid"
            var nameIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (string.IsNullOrEmpty(nameIdClaim))
            {
                throw new ArgumentException("nameid claim not found.");
            }

            return nameIdClaim;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid JWT token: " + ex.Message);
        }
    }
}
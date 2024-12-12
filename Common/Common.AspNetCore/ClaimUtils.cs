using System.Security.Claims;

namespace Common.AspNetCore;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal claims)
    {
        if (claims == null)
        {
            throw new ArgumentNullException(nameof(claims));
        }

        return Convert.ToInt64(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
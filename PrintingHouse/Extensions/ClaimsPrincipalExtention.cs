namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    using PrintingHouse.Core.Constants;

    public static class ClaimsPrincipalExtention
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string FullName(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == ApplicationConstants.FullNameClaim)?.Value ?? string.Empty;
    }
    }
}

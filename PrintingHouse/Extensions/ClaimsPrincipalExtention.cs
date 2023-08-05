namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    using PrintingHouse.Core.Constants;

    public static class ClaimsPrincipalExtention
    {
        public static Guid Id(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static string FullName(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == ApplicationConstants.FullNameClaim)?.Value ?? string.Empty;
    }
    }
}

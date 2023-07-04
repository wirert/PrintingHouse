namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtention
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

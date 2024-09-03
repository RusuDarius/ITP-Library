using System.Security.Claims;

namespace ITPLibrary.Api.Common
{
    public class CommonMethods
    {
        public static int GetUserIdFromContext(HttpContext httpContext)
        {
            int userId = int.Parse(
                (httpContext.User.Identity as ClaimsIdentity)!.FindFirst("Id")!.Value
            );
            return userId;
        }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// Returns the current authenticated user's Id.
        /// Throws InvalidOperationException if the claim is missing/invalid.
        /// </summary>
        protected Guid GetUserId()
        {
            var idValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrWhiteSpace(idValue) || !Guid.TryParse(idValue, out var userId))
            {
                throw new InvalidOperationException("Authenticated user id claim not found or invalid.");
            }

            return userId;
        }

    }
}

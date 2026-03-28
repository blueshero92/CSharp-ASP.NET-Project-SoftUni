using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.OutputMessages.BaseControllerErrors;

namespace GamingZoneApp.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
       
        // Returns the current authenticated user's Id.
        // Throws InvalidOperationException if the claim is missing or invalid.
        protected Guid GetUserId()
        {
            // First try to get the user id from the standard NameIdentifier claim, then fall back to a custom "id" claim if needed.
            string? idValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            // Validate that we found a claim and that it can be parsed as a Guid.
            if (string.IsNullOrWhiteSpace(idValue) || !Guid.TryParse(idValue, out var userId))
            {
                throw new InvalidOperationException(UserNotAuthenticatedError);
            }

            return userId;
        }

    }
}

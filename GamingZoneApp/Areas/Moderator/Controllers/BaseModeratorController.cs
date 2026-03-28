using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    [Authorize(Roles = "Moderator")]
    [AutoValidateAntiforgeryToken]
    public class BaseModeratorController : Controller
    {
        
    }
}

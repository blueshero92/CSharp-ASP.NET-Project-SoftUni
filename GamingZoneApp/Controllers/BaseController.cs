using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
         
    }
}

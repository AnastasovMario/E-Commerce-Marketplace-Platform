using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Commerce_Marketplace_Platform.Areas.Admin.Constants.AdminConstants;

namespace E_Commerce_Marketplace_Platform.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRolleName)]

    public class BaseController : Controller
    {
    }
}

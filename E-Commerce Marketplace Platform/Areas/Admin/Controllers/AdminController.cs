using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Areas.Admin.Controllers
{
    
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

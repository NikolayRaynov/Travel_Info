using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Travel_Info.Areas.Admin.Controllers
{
	using static Common.ApplicationConstants;

	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

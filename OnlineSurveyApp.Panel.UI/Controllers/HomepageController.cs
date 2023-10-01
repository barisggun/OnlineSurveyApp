using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class HomepageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

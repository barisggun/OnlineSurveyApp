using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var value = TempData["UserName"];
            ViewBag.v = value;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Email);

            if (user != null && user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                TempData["AccountActivated"] = "Hesabınız başarıyla aktifleştirildi.";
                return RedirectToAction("SignIn", "Account");
                //return RedirectToAction("VerificationSuccessful", "ConfirmMail");
            }
            else
            {
                ModelState.AddModelError("ConfirmCode", "Yanlış doğrulama kodu girdiniz.");

                ViewBag.v = confirmMailViewModel.Email;

                return View(confirmMailViewModel);
            }

        }

        public IActionResult VerificationSuccessful()
        {
            return View();
        }
    }
}

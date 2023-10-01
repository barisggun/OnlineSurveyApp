using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;
using System.ComponentModel.DataAnnotations;


namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel p)
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captchaImage))
            {
                ModelState.AddModelError("recaptcha", "Lütfen Google Recaptcha'yı doldurunuz.");

            }

            var verified = await checkCaptcha();

            if (!verified)
            {
                ModelState.AddModelError("recaptcha", "Doğrulanamadı.");
                return View(p);
            }


            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                var user = await _userManager.FindByEmailAsync(p.Email);
                if (user != null) 
                {
                    if (!user.EmailConfirmed)
                    {
                        user.ConfirmCode = code;

                        var email = _configuration["AppSettings:SmtpSettings:Email"];
                        var password = _configuration["AppSettings:SmtpSettings:Password"];

                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom = new MailboxAddress("Beni Tanıyor Musun? Kayıt", "benitaniyormusun.info@gmail.com");
                        MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                        mimeMessage.From.Add(mailboxAddressFrom);
                        mimeMessage.To.Add(mailboxAddressTo);

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = p.Email + " Beni Tanıyor Musun'a hoş geldin! " + "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
                        mimeMessage.Body = bodyBuilder.ToMessageBody();

                        mimeMessage.Subject = "Beni Tanıyor Musun Onay Kodu";

                        ISmtpClient client = new MailKit.Net.Smtp.SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate(email, password);
                        client.Send(mimeMessage);
                        client.Disconnect(true);

                        await _userManager.UpdateAsync(user);

                        TempData["UserName"] = p.Email;
                        return RedirectToAction("Index", "ConfirmMail");
                    }

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, p.Password, false, true);
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("lockOutAccount", "Hesabınız geçici olarak kilitlendi. Lütfen daha sonra tekrar deneyin.");
                        return View(p);
                    }
                    if (result.Succeeded)
                    {

                        HttpContext.Session.Remove(TestIdSessionKey);
                        HttpContext.Session.Remove("CurrentQuestionNumber");

                        return RedirectToAction("Index", "Homepage");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Kullanıcı adı veya şifre hatalı tekrar deneyiniz.");
                        return View(p);
                    }
                }
                else
                {
                    ModelState.AddModelError("noFoundUser", "Kullanıcı adı veya e-posta adresi bulunamadı.");
                    return View(p);
                }
            }
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel p)
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captchaImage))
            {
                ModelState.AddModelError("recaptcha", "Lütfen Google Recaptcha'yı doldurunuz.");

            }

            var verified = await checkCaptcha();
            if (!verified)
            {
                ModelState.AddModelError("recaptcha", "Doğrulanamadı.");
                return View(p);
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(p.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("signupError", "E-posta kayıtlı, lütfen farklı bir e-posta adresi giriniz.");
                    return View(p);
                }

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                AppUser user = new AppUser()
                {
                    Email = p.Email,
                    Name = p.Name,
                    Surname = p.Surname,
                    UserName = p.UserName,
                    ConfirmCode = code,
                };

                var result = await _userManager.CreateAsync(user,p.Password);

                if (result.Succeeded)
                {

                    var email = _configuration["AppSettings:SmtpSettings:Email"];
                    var password = _configuration["AppSettings:SmtpSettings:Password"];

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Beni Tanıyor Musun? Kayıt", "benitaniyormusun.info@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = p.Email + " Beni Tanıyor Musun'a hoş geldin! " + "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Beni Tanıyor Musun Onay Kodu";

                    ISmtpClient client = new MailKit.Net.Smtp.SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(email, password);
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    TempData["UserName"] = p.Email;


                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("signupError", item.Description);
                    }
                }
            }
            return View(p);
        }

        private int currentQuestionNumber;
        private const string TestIdSessionKey = "TestId";
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.Remove(TestIdSessionKey);
            HttpContext.Session.Remove("CurrentQuestionNumber");

            return RedirectToAction("Index", "Homepage");
        }


        public async Task<bool> checkCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6LcvSUcoAAAAAFJHgEx__LZ-itdZAgQDeFRL4RHL"),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])

            };

            var client = new HttpClient();

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }

    }
}

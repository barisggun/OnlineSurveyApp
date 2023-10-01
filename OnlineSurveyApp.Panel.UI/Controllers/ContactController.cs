using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequestModel mailRequest)
        {
            var email = _configuration["AppSettings:SmtpSettings:Email"];
            var password = _configuration["AppSettings:SmtpSettings:Password"];

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", email);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);
            if (ModelState.IsValid)
            {
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Bu mesaj BeniTaniyorMusun.com iletişim formundan gönderilmiştir." + "\n\nGönderen kişinin mail adresi: " + mailRequest.SenderMail + "\nGönderen kişinin adı: " + mailRequest.Name + "\n\n\nGönderen kişinin mesajı: " + mailRequest.Body;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = mailRequest.Subject;

                ISmtpClient client = new MailKit.Net.Smtp.SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(email, password);
                client.Send(mimeMessage);
                client.Disconnect(true);
               
                TempData["message"] = "Mesajınız başarıyla gönderildi sizinle en yakın zamanda iletişime geçeceğiz!";

                return RedirectToAction("Index", "Contact");
            }



            return View(mailRequest);
        }

    }
}

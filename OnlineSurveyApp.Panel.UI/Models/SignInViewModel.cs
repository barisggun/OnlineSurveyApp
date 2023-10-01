using System.ComponentModel.DataAnnotations;

namespace OnlineSurveyApp.Panel.UI.Models
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "Lütfen E-postanızı giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        public string Password { get; set; }
        public int ConfirmCode { get; set; }
    }
}

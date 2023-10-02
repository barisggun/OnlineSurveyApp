using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;

namespace OnlineSurveyApp.Panel.UI.ViewComponents.Homepage
{
    public class _HomePageUserName : ViewComponent
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var userID = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = userManager.TGetById(userID);
            return View(values);

        }

    }
}

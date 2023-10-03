using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;

namespace OnlineSurveyApp.Panel.UI.ViewComponents.Test
{
    public class _ScoreList : ViewComponent
    {
        Context context = new Context();
        ScoreListManager sm = new ScoreListManager(new EfScoreListRepository());
        public async Task<IViewComponentResult> InvokeAsync(int testId)
        {
            var scores = sm.Scores(testId);

            foreach (var score in scores)
            {
                
                score.AppUser = context.Users.FirstOrDefault(u => u.Id == score.AppUserId);

                
                score.Guest = context.Guests.FirstOrDefault(g => g.ID == score.GuestId);
            }

            return View(scores);
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.DataAccess.Concrete;

namespace OnlineSurveyApp.Panel.UI.ViewComponents.Test
{
    public class _ScoreList : ViewComponent
    {
        Context context = new Context();
        public async Task<IViewComponentResult> InvokeAsync(int testId)
        {
            var scores = context.ScoreLists
                .Where(sl => sl.TestId == testId)
                .OrderByDescending(sl => sl.Score)
                .ToList();

            foreach (var score in scores)
            {
                
                score.AppUser = context.Users.FirstOrDefault(u => u.Id == score.AppUserId);

                
                score.Guest = context.Guests.FirstOrDefault(g => g.ID == score.GuestId);
            }

            return View(scores);
        }
    }

}

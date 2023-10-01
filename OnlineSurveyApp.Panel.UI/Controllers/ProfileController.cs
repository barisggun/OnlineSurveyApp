using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        TestManager tm = new TestManager(new EfTestRepository());
        Context context = new Context();
        private readonly UserManager<AppUser> _userManager;


        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("Index/{userId}")]
        public IActionResult Index(int userId)
        {
            var user = userManager.GetById(userId);
            ViewBag.Name = user.Name + " " +user.Surname;
            ViewBag.UserId = user.Id;
            return View();
        }

        [Route("TestList/{userId}")]
        public IActionResult TestList(int userId)
        { 
            var userTests = context.Tests.Where(t=> t.AppUserId == userId).ToList();
            return View(userTests);
        }

        [Route("ScoreTable/{testId}")]
        public IActionResult ScoreTable(int testId)
        {
            
            var scores = context.ScoreLists
                .Where(sl => sl.TestId == testId)
                .OrderByDescending(sl => sl.Score)
                .ToList();

            foreach (var score in scores)
            {
                score.AppUser = context.Users.FirstOrDefault(u => u.Id == score.AppUserId);
            }

            ViewBag.TestId = testId;

            return View(scores);
        }

        [Route("DeleteTest/{testId}")]
        public IActionResult DeleteTest(int testId)
        {        
            var test = tm.GetById(testId);

            var scoreLists = context.ScoreLists.Where(sl => sl.TestId == testId).ToList();

            context.ScoreLists.RemoveRange(scoreLists);
            context.SaveChanges();

            tm.Delete(test);

            return RedirectToAction("TestList");
        }


        // SORU OLUŞTURMA ALANI
        [Route("CreateQuestion")]
        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateQuestion")]
        public IActionResult CreateQuestion(CreateQuestionViewModel m)
        {

            if (ModelState.IsValid)
            {
                string questionText = m.Text;
                List<string> answerTexts = m.AnswerTexts;

                var question = new Question
                {
                    Text = questionText,
                    Status = false,
                    Answers = new List<Answer>(),
                };

                foreach (string answerText in answerTexts)
                {
                    var answer = new Answer()
                    {
                        Text = answerText,
                        Question = question,
                    };
                    question.Answers.Add(answer);
                }

                context.Questions.Add(question);
                context.SaveChanges();

                return RedirectToAction("QuestionList", "Admin");
            }
            return View(m);
        }


    }
}

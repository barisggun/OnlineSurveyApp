using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;
using OnlineSurveyApp.Panel.UI.ValidationRules;
using System;
//using System.ComponentModel.DataAnnotations;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        TestManager tm = new TestManager(new EfTestRepository());
        QuestionManager qm = new QuestionManager(new EfQuestionRepository());
        ScoreListManager sm = new ScoreListManager(new EfScoreListRepository());
        Context context = new Context();
        private readonly UserManager<AppUser> _userManager;


        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("Index/{userId}")]
        public IActionResult Index(int userId)
        {
            var user = userManager.TGetById(userId);
            ViewBag.Name = user.Name + " " +user.Surname;
            ViewBag.UserId = user.Id;
            return View();
        }

        [Route("TestList/{userId}")]
        public IActionResult TestList(int userId)
        { 
            var userTests = tm.TestList(userId);    
            return View(userTests);
        }

        [Route("ScoreTable/{testId}")]
        public IActionResult ScoreTable(int testId)
        {
            ViewBag.TestId = testId;

            return View();
        }

        [Route("DeleteTest/{testId}")]
        public IActionResult DeleteTest(int testId)
        {        
            var test = tm.TGetById(testId);

            sm.RemoveTestWithScoreList(testId);

            //var scoreLists = context.ScoreLists.Where(sl => sl.TestId == testId).ToList();

            //context.ScoreLists.RemoveRange(scoreLists);
            //context.SaveChanges();

            tm.TDelete(test);

            return RedirectToAction("TestList", new { userId = test.AppUserId });
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
            var validator = new CreateQuestionValidator();
            var validationResult = validator.Validate(m);

            if (validationResult.IsValid)
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

                qm.TAdd(question);

                return RedirectToAction("Index","Homepage");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(m);
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using OnlineSurveyApp.BusinessLayer.Abstract;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        Context context = new Context();

        QuestionManager qm = new QuestionManager(new EfQuestionRepository());
        AnswerManager am = new AnswerManager(new EfAnswerRepository());
        GuestManager gm = new GuestManager(new EfGuestRepository());
        UserManager um = new UserManager(new EfUserRepository());

        private readonly UserManager<AppUser> _userManager;
        private int currentQuestionNumber;

        public TestController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private const string TestIdSessionKey = "TestId";

        public IActionResult Guest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guest(Guest guest)
        {
            gm.TAdd(guest);
            HttpContext.Session.SetInt32("guestId", guest.ID);
            return RedirectToAction("Create"/*, new { guestId = guest.ID }*/);
        }

        [Authorize(Roles = "User")]
        [Route("Create")]
        public IActionResult Create()
        {
            var username = User?.Identity?.Name;
            var userID = um.GetUserIdByUserName(username);
            var userId = userID;

            var currentQuestionNumber = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0;
            var guestId = HttpContext.Session.GetInt32("guestId") ?? 0;

            var viewModel = new CreateTestViewModel();


            //HttpContext.Session.Remove(TestIdSessionKey);
            //HttpContext.Session.Remove("CurrentQuestionNumber");


            if (guestId != 0)
            {
               viewModel.Questions = qm.TGetList().Where(x => x.Status == true).Take(10).ToList();
            }
            else
            {
                viewModel.Questions = qm.TGetList().Where(x => x.Status == true).ToList();
            }

            viewModel.CurrentQuestionNumber = currentQuestionNumber;

            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult GetAnswers(int questionId)
        {
            var answers = am.GetQuestionWithAnswers(questionId);

            return PartialView("_AnswersPartial", answers);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateTestWithAnswersAsync(CreateTestViewModel viewModel)
        {
            var guestId = HttpContext.Session.GetInt32("guestId") ?? 0;

            var currentQuestionNumber = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0; 

            var currentTestId = HttpContext.Session.GetInt32(TestIdSessionKey);

            if (currentQuestionNumber == 10)
            {
                if (currentTestId != null)
                {
                    HttpContext.Session.Remove(TestIdSessionKey);
                    HttpContext.Session.Remove("CurrentQuestionNumber");
                    HttpContext.Session.Remove("guestId");
                }
                return RedirectToAction("TestCreated", new { testId = currentTestId });
            }
            
            if(currentQuestionNumber == 4 && guestId != 0) 
            {
                if (currentTestId != null)
                {
                    HttpContext.Session.Remove(TestIdSessionKey);
                    HttpContext.Session.Remove("CurrentQuestionNumber");
                    HttpContext.Session.Remove("guestId");
                }
                return RedirectToAction("TestCreated", new { testId = currentTestId });
            }
            
            if (currentTestId == null)
            {
                if (viewModel.SelectedQuestionId == 0)
                {
                    return View("Create");
                }

                var username = User?.Identity?.Name;
                var userID = um.GetUserIdByUserName(username);
                var userId = userID;

                if (username == null)
                {
                    var testGuest = new Test
                    {
                        GuestId = guestId
                    };

                    context.Tests.Add(testGuest);

                    CorrectAnswer correctAnswerEntity = new CorrectAnswer
                    {
                        QuestionId = viewModel.SelectedQuestionId,
                        Correct = viewModel.CorrectAnswer,
                        Test = testGuest
                    };

                    context.CorrectAnswers.Add(correctAnswerEntity);

                    var testQuestion = new TestQuestion
                    {
                        QuestionId = viewModel.SelectedQuestionId,
                        Test = testGuest
                    };

                    context.TestQuestions.Add(testQuestion);

                    context.SaveChanges();

                    HttpContext.Session.SetInt32(TestIdSessionKey, testGuest.ID);
                    HttpContext.Session.SetInt32("CurrentQuestionNumber", currentQuestionNumber + 1);
                }
                else
                {
                    var test = new Test
                    {
                        AppUserId = userId
                    };

                    context.Tests.Add(test);

                    CorrectAnswer correctAnswerEntity = new CorrectAnswer
                    {
                        QuestionId = viewModel.SelectedQuestionId,
                        Correct = viewModel.CorrectAnswer,
                        Test = test
                    };

                    context.CorrectAnswers.Add(correctAnswerEntity);

                    var testQuestion = new TestQuestion
                    {
                        QuestionId = viewModel.SelectedQuestionId,
                        Test = test
                    };
                    context.TestQuestions.Add(testQuestion);

                    context.SaveChanges();

                    HttpContext.Session.SetInt32(TestIdSessionKey, test.ID);
                    HttpContext.Session.SetInt32("CurrentQuestionNumber", currentQuestionNumber + 1);
                }

            }
            else
            {
                var currentTestIdValue = currentTestId.Value;
                var test = context.Tests.FirstOrDefault(x => x.ID == currentTestIdValue);

                if (test != null)
                {
                    if (viewModel.SelectedQuestionId == 0)
                    {
                        return View("Create");
                    }

                    var existingTestQuestion = context.TestQuestions
                        .FirstOrDefault(tq => tq.TestId == test.ID && tq.QuestionId == viewModel.SelectedQuestionId);

                    if (existingTestQuestion == null)
                    {
                        
                        var testQuestion = new TestQuestion
                        {
                            QuestionId = viewModel.SelectedQuestionId,
                            Test = test
                        };
                        context.TestQuestions.Add(testQuestion);

                        CorrectAnswer correctAnswerEntity = new CorrectAnswer
                        {
                            QuestionId = viewModel.SelectedQuestionId,
                            Correct = viewModel.CorrectAnswer,
                            Test = test
                        };
                        context.CorrectAnswers.Add(correctAnswerEntity);

                        context.SaveChanges();

                        HttpContext.Session.SetInt32("CurrentQuestionNumber", currentQuestionNumber + 1);
                    }
                }
            }

            viewModel.CurrentQuestionNumber = currentQuestionNumber;

            return RedirectToAction("Create", viewModel);
        }

        [Authorize(Roles = "User")]
        public IActionResult TestCreated(int testId)
        {
            var model = new TestCreatedViewModel
            {
                TestId = testId
            };

            return View(model);
        }


    }
}

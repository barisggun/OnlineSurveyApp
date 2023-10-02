using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.Controllers
{

    [Route("Start")]
    [AllowAnonymous]
    public class StartController : Controller
    {
        Context context = new Context();
        QuestionManager qm = new QuestionManager(new EfQuestionRepository());
        AnswerManager am = new AnswerManager(new EfAnswerRepository());
        TestQuestionManager testQuestionManager = new TestQuestionManager(new EfTestQuestionRepository());
        TestManager tm = new TestManager(new EfTestRepository());
        GuestManager gm = new GuestManager(new EfGuestRepository());

        private const string TestIdSessionKey = "TestId";
        private int currentQuestionNumber;

        [Route("Index/{testId}")]
        public IActionResult Index(int testId)
        {
            var test = context.Tests.Where(x => x.ID == testId).FirstOrDefault();

            if (test != null)
            {
                var userId = test.AppUserId;
                var userName = context.Users.Where(x => x.Id == userId).FirstOrDefault();

                var guestId = test.GuestId;
                var guestName = context.Guests.Where(x => x.ID == guestId).FirstOrDefault();

                if (userName != null)
                {
                    ViewBag.UserName = userName.Name;
                    ViewBag.TestId = testId;
                    return View();
                }
                else
                {
                    ViewBag.TestId = testId;
                    ViewBag.GuestName = guestName.Name;
                    ViewBag.GuestId = guestId;
                    return View();
                }
            }
            return RedirectToAction("TestNotFound");
        }

        [Route("Test/{testId}")]
        public IActionResult Test(int testId)
        {
            var guestId = HttpContext.Session.GetInt32("GuestId");
            
            var test = context.Tests.Where(x => x.ID == testId).FirstOrDefault();

            var username = User?.Identity?.Name;
            var userID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var userId = userID;


            if (test != null)
            {
                //var userId = test.AppUserId;
                var userName = context.Users.Where(x => x.Id == userId).FirstOrDefault();

                //var guestId = test.GuestId;
                //var guestName = context.Guests.Where(x => x.ID == guestId).FirstOrDefault();

                if (userName != null)
                {
                    ViewBag.UserName = userName.Name;
                    ViewBag.TestId = testId;

                    var currentQuestionNumber = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0;
                    HttpContext.Session.SetInt32(TestIdSessionKey, testId);

                    var testQuestion = context.TestQuestions
                        .Where(tq => tq.TestId == testId)
                        .Include(tq => tq.Question)
                        .Skip(currentQuestionNumber)
                        .FirstOrDefault(); 

                    if (testQuestion != null)
                    {
                        var testQuestionViewModel = new TestQuestionViewModel
                        {
                            Questions = new List<Question> { testQuestion.Question },
                            Answers = context.Answers.Where(a => a.QuestionId == testQuestion.Question.ID).ToList(),
                            TestId = testQuestion.TestId,
                            CurrentQuestionNumber = currentQuestionNumber
                        };

                        return View(testQuestionViewModel);
                    }

                    return View(); 
                }
                var guestName = context.Guests.Where(x => x.ID == guestId).FirstOrDefault();
                if (guestName != null)
                {
                    ViewBag.TestId = testId;
                    ViewBag.GuestName = guestName.Name;


                    var currentQuestionNumber = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0;
                    HttpContext.Session.SetInt32(TestIdSessionKey, testId);

                    var testQuestion = context.TestQuestions
                        .Where(tq => tq.TestId == testId)
                        .Include(tq => tq.Question)
                        .Skip(currentQuestionNumber)
                        .FirstOrDefault(); 

                    if (testQuestion != null)
                    {
                        var testQuestionViewModel = new TestQuestionViewModel
                        {
                            Questions = new List<Question> { testQuestion.Question },
                            Answers = context.Answers.Where(a => a.QuestionId == testQuestion.Question.ID).ToList(),
                            TestId = testQuestion.TestId,
                            CurrentQuestionNumber = currentQuestionNumber
                        };

                        return View(testQuestionViewModel);
                    }

                    return View(); 
                }
                else
                {
                    return RedirectToAction("Index", new { testId = testId });

                }

            }
            //end

            return RedirectToAction("TestNotFound");

        }


        [HttpPost]
        [Route("Test/{testId}")]
        public IActionResult Test(TestQuestionViewModel testQuestionViewModel)
        {
            
            var guestId = HttpContext.Session.GetInt32("GuestId");
            
            var testId = HttpContext.Session.GetInt32(TestIdSessionKey) ?? 0;

            var test = context.Tests.FirstOrDefault(t => t.ID == testId);

            if (test != null && test.GuestId != null)
            {
                var numberOfResponses = context.ScoreLists
                    .Count(sl => sl.TestId == testId && sl.GuestId != null);

                if (numberOfResponses >= 5)
                {
                    
                    return RedirectToAction("FullCapacity");
                }

            }

            if (guestId != null)
            {
                var correctAnswerGuest = context.CorrectAnswers
    .Where(ca => ca.QuestionId == testQuestionViewModel.QuestionId && ca.TestId == testId)
    .Select(ca => ca.Correct)
    .FirstOrDefault();


                int scoreGuest = HttpContext.Session.GetInt32("UserScore") ?? 0;

                
                if (testQuestionViewModel.SelectedAnswer == correctAnswerGuest)
                {
                    scoreGuest++;
                }
                HttpContext.Session.SetInt32("UserScore", scoreGuest);

                var existingScoreGuest = context.ScoreLists
            .FirstOrDefault(sl => sl.GuestId == guestId && sl.TestId == testQuestionViewModel.TestId);

                if (existingScoreGuest != null)
                {
                    
                    existingScoreGuest.Score = scoreGuest;
                }
                else
                {
                    
                    var scoreList = new ScoreList
                    {
                        GuestId = guestId,
                        TestId = testQuestionViewModel.TestId,
                        Score = scoreGuest
                    };

                    context.ScoreLists.Add(scoreList);
                }

                context.SaveChanges();

                var currentQuestionNumberGuest = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0;
                currentQuestionNumberGuest++; 

                
                HttpContext.Session.SetInt32("CurrentQuestionNumber", currentQuestionNumberGuest);

                var hasMoreQuestionsGuest = context.TestQuestions
           .Where(tq => tq.TestId == testId)
           .Skip(currentQuestionNumberGuest)
           .Any();


                if (!hasMoreQuestionsGuest)
                {
                    HttpContext.Session.Remove(TestIdSessionKey);
                    HttpContext.Session.Remove("CurrentQuestionNumber");
                    HttpContext.Session.Remove("UserScore");
                    HttpContext.Session.Remove("GuestId");
                   
                    return RedirectToAction("TestCompleted", new { testId = testQuestionViewModel.TestId, scoreGuest });
                }


                
                return RedirectToAction("Test", new { testId = testQuestionViewModel.TestId });
            }


            var username = User?.Identity?.Name;
            var userID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var userId = userID;

            var correctAnswer = context.CorrectAnswers
      .Where(ca => ca.QuestionId == testQuestionViewModel.QuestionId && ca.TestId == testId)
      .Select(ca => ca.Correct)
      .FirstOrDefault();


            int score = HttpContext.Session.GetInt32("UserScore") ?? 0;

            
            if (testQuestionViewModel.SelectedAnswer == correctAnswer)
            {
                score++;
            }
            HttpContext.Session.SetInt32("UserScore", score);

            var existingScore = context.ScoreLists
        .FirstOrDefault(sl => sl.AppUserId == userId && sl.TestId == testQuestionViewModel.TestId);

            if (existingScore != null)
            {
                
                existingScore.Score = score;
            }
            else
            {
                
                var scoreList = new ScoreList
                {
                    AppUserId = userId,
                    TestId = testQuestionViewModel.TestId,
                    Score = score
                };

                context.ScoreLists.Add(scoreList);
            }

            context.SaveChanges();

            var currentQuestionNumber = HttpContext.Session.GetInt32("CurrentQuestionNumber") ?? 0;
            currentQuestionNumber++; 

            
            HttpContext.Session.SetInt32("CurrentQuestionNumber", currentQuestionNumber);

            var hasMoreQuestions = context.TestQuestions
       .Where(tq => tq.TestId == testId)
       .Skip(currentQuestionNumber)
       .Any();


            if (!hasMoreQuestions)
            {
                HttpContext.Session.Remove(TestIdSessionKey);
                HttpContext.Session.Remove("CurrentQuestionNumber");
                HttpContext.Session.Remove("UserScore");
                HttpContext.Session.Remove("GuestId");

                
                return RedirectToAction("TestCompleted", new { testId = testQuestionViewModel.TestId, score });
            }


            
            return RedirectToAction("Test", new { testId = testQuestionViewModel.TestId });
        }

        [Route("TestCompleted/{testId}")]
        public IActionResult TestCompleted(int testId, int score, int scoreGuest)
        {
            var testCompletedViewModel = new TestCompletedViewModel
            {
                Score = score,
                GuestScore = scoreGuest

            };

            ViewBag.TestId = testId;

            return View(testCompletedViewModel);
        }

        [AllowAnonymous]
        [Route("Guest/{testId}")]
        public IActionResult Guest(int testId)
        {
            ViewBag.TestId = testId;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Guest/{testId}")]
        public IActionResult Guest(Guest guest, int testId)
        {
            gm.TAdd(guest);
            int guestId = guest.ID;
            HttpContext.Session.SetInt32("GuestId", guestId);
            return RedirectToAction("Test", new { testId });
        }

        [Route("FullCapacity")]
        public IActionResult FullCapacity()
        {
            return View();
        }

        public IActionResult TestNotFound()
        {

            return View();
        }





    }
}

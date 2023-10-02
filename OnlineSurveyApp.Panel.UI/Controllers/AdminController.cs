using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSurveyApp.BusinessLayer.Concrete;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.EntityFramework;
using OnlineSurveyApp.EntityLayer.Entities;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.Controllers
{
    [Authorize(Roles =  "Admin")]
    public class AdminController : Controller
    {
        Context context = new Context();
        QuestionManager qm = new QuestionManager(new EfQuestionRepository());
        AnswerManager am = new AnswerManager(new EfAnswerRepository());
        CorrectAnswerManager ca = new CorrectAnswerManager(new EfCorrectAnswerRepository());
        TestManager tm = new TestManager(new EfTestRepository());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateQuestion(CreateQuestionViewModel m)
        {
            if (ModelState.IsValid)
            {
                string questionText = m.Text;
                List<string> answerTexts = m.AnswerTexts;

                var question = new Question
                {
                    Text = questionText,
                    Answers = new List<Answer>(),
                };

                foreach (string answerText in answerTexts)
                {
                    var answer = new Answer()
                    {
                        Text = answerText,
                        Question = question,
                    };
                    question.Status = true;
                    question.Answers.Add(answer);
                }           

                context.Questions.Add(question);
                context.SaveChanges();

                return RedirectToAction("QuestionList","Admin");
            }
            return View(m);
        }

        public IActionResult QuestionList()
        {
            var values = context.Questions.Where(x=> x.Status == true).ToList();
            return View(values);
        }

        public IActionResult DeleteQuestion(int id)
        {
            var question = qm.TGetById(id);
            qm.TDelete(question);

            return RedirectToAction("QuestionList","Admin");
        }

        public IActionResult QuestionApproval()
        {
            var values = context.Questions.Where(x=>x.Status == false).ToList();
            return View(values);
        }

        public IActionResult ApproveQuestion(int id)
        {
            var question = qm.TGetById(id);  
            if (question != null)
            {
                question.Status = true;
                qm.TUpdate(question);
            }
            return RedirectToAction("QuestionApproval", "Admin");
        }

    }
}

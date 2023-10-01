using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.Repositories;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.EntityFramework
{
    public class EfAnswerRepository : GenericRepository<Answer>, IAnswerDal
    {
      //  Context context = new Context();    
      //  public List<Answer> GetQuestionWithAnswers(int questionId)
      //  {
      //      return context.Answers
      //.Where(a => a.QuestionId == questionId)
      //.ToList();
      //  }
        public List<Answer> GetQuestionWithAnswers(int questionId)
        {
            using (var context = new Context())
            {
                var answers = context.Answers.Where(a => a.QuestionId == questionId).ToList();
                return answers;
            }
        }
    }
}

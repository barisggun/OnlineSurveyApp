using OnlineSurveyApp.BusinessLayer.Abstract;
using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Concrete
{
    public class AnswerManager : IAnswerService
    {
        IAnswerDal _answerDal;

        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }

        public void Create(Answer answer)
        {
            _answerDal.Create(answer);
        }

        public void Delete(Answer answer)
        {
            _answerDal.Delete(answer);
        }

        public List<Answer> GetAll()
        {
            return _answerDal.GetAll();
        }

        public Answer GetById(int id)
        {
            return _answerDal.GetById(id);
        }

        public void Update(Answer answer)
        {
            _answerDal.Update(answer);
        }

        public List<Answer> GetQuestionWithAnswers(int questionId)
        {
            return _answerDal.GetQuestionWithAnswers(questionId);
        }

    }
}

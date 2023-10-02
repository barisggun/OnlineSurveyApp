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

        public List<Answer> GetQuestionWithAnswers(int questionId)
        {
            return _answerDal.GetQuestionWithAnswers(questionId);
        }

        public void TAdd(Answer t)
        {
            _answerDal.Insert(t);
        }

        public void TDelete(Answer t)
        {
            _answerDal.Delete(t);
        }

        public Answer TGetById(int id)
        {
            return _answerDal.GetByID(id);
        }

        public List<Answer> TGetList()
        {
            return _answerDal.GetList();
        }

        public void TUpdate(Answer t)
        {
            _answerDal.Update(t);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OnlineSurveyApp.BusinessLayer.Abstract;
using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Concrete
{
    public class QuestionManager : IQuestionService
    {
        IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        public List<Question> StatusFalseQuestions()
        {
            return _questionDal.StatusFalseQuestions();
        }

        public List<Question> StatusTrueQuestions()
        {
            return _questionDal.StatusTrueQuestions();
        }

        public void TAdd(Question t)
        {
            _questionDal.Insert(t);
        }

        public void TDelete(Question t)
        {
            _questionDal.Delete(t);
        }

        public Question TGetById(int id)
        {
            return _questionDal.GetByID(id);
        }

        public List<Question> TGetList()
        {
            return _questionDal.GetList();
        }

        public void TUpdate(Question t)
        {
            _questionDal.Update(t);
        }
    }
}

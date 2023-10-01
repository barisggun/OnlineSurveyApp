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

        public void Create(Question question)
        {
            _questionDal.Create(question);
        }

        public void Delete(Question question)
        {
            _questionDal.Delete(question);
        }

        public List<Question> GetAll()
        {
            return _questionDal.GetAll();
        }

        public Question GetById(int id)
        {
            return _questionDal.GetById(id);
        }

        public void Update(Question question)
        {
            _questionDal.Update(question);
        }

    }
}

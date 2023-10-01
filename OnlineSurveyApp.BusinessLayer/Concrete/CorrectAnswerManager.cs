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
    public class CorrectAnswerManager : ICorrectAnswerService
    {
        ICorrectAnswerDal _correctAnswerDal;

        public CorrectAnswerManager(ICorrectAnswerDal correctAnswerDal)
        {
            _correctAnswerDal = correctAnswerDal;
        }

        public void Create(CorrectAnswer correctAnswer)
        {
            _correctAnswerDal.Create(correctAnswer);
        }

        public void Delete(CorrectAnswer correctAnswer)
        {
            _correctAnswerDal.Delete(correctAnswer);
        }

        public List<CorrectAnswer> GetAll()
        {
            return _correctAnswerDal.GetAll();
        }

        public CorrectAnswer GetById(int id)
        {
            return _correctAnswerDal.GetById(id);
        }

        public void Update(CorrectAnswer correctAnswer)
        {
            _correctAnswerDal.Update(correctAnswer);
        }
    }
}

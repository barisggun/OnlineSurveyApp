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

        public void TAdd(CorrectAnswer t)
        {
            _correctAnswerDal.Insert(t);
        }

        public void TDelete(CorrectAnswer t)
        {
            _correctAnswerDal.Delete(t);
        }

        public CorrectAnswer TGetById(int id)
        {
            return _correctAnswerDal.GetByID(id);
        }

        public List<CorrectAnswer> TGetList()
        {
            return _correctAnswerDal.GetList();
        }

        public void TUpdate(CorrectAnswer t)
        {
            _correctAnswerDal.Update(t);    
        }
    }
}

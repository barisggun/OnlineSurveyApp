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
    public class TestQuestionManager : ITestQuestionService
    {
        ITestQuestionDal _testQuestionDal;

        public TestQuestionManager(ITestQuestionDal testQuestionDal)
        {
            _testQuestionDal = testQuestionDal;
        }

        public void TAdd(TestQuestion t)
        {
            _testQuestionDal.Insert(t);
        }

        public void TDelete(TestQuestion t)
        {
            _testQuestionDal.Delete(t);
        }

        public TestQuestion TGetById(int id)
        {
            return _testQuestionDal.GetByID(id);
        }

        public List<TestQuestion> TGetList()
        {
            return _testQuestionDal.GetList();
        }

        public void TUpdate(TestQuestion t)
        {
            _testQuestionDal.Update(t);
        }
    }
}

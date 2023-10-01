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

        public void Create(TestQuestion testQuestion)
        {
            _testQuestionDal.Create(testQuestion);
        }

        public void Delete(TestQuestion testQuestion)
        {
            _testQuestionDal.Delete(testQuestion);
        }

        public List<TestQuestion> GetAll()
        {
            return _testQuestionDal.GetAll();
        }

        public TestQuestion GetById(int id)
        {
            return _testQuestionDal.GetById(id);    
        }

        public void Update(TestQuestion testQuestion)
        {
            _testQuestionDal.Update(testQuestion);  
        }
    }
}

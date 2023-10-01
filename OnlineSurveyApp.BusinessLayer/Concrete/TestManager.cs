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
    public class TestManager : ITestService
    {
        ITestDal _testDal;

        public TestManager(ITestDal testDal)
        {
            _testDal = testDal;
        }

        public void Create(Test test)
        {
            _testDal.Create(test);
        }

        public void Delete(Test test)
        {
            _testDal.Delete(test);  
        }

        public List<Test> GetAll()
        {
            return _testDal.GetAll();
        }

        public Test GetById(int id)
        {
            return _testDal.GetById(id);
        }

        public void Update(Test test)
        {
            _testDal.Update(test); 
        }
    }
}

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

        public void TAdd(Test t)
        {
            _testDal.Insert(t);
        }

        public void TDelete(Test t)
        {
            _testDal.Delete(t);
        }

        public Test TGetById(int id)
        {
            return _testDal.GetByID(id);
        }

        public List<Test> TGetList()
        {
            return _testDal.GetList();
        }

        public void TUpdate(Test t)
        {
            _testDal.Update(t);
        }
    }
}

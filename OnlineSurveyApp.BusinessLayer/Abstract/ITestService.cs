using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface ITestService
    {
        Test GetById(int id);
        List<Test> GetAll();
        void Create(Test test);
        void Update(Test test);
        void Delete(Test test);
    }
}

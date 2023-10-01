using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface ITestQuestionService
    {
        TestQuestion GetById(int id);
        List<TestQuestion> GetAll();
        void Create(TestQuestion testQuestion);
        void Update(TestQuestion testQuestion);
        void Delete(TestQuestion testQuestion);
    }
}

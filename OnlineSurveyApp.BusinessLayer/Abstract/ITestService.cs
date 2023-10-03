using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface ITestService : IGenericService<Test>
    {
        public List<Test> TestList(int userId);
    }
}

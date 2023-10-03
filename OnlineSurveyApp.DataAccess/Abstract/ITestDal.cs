using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Abstract
{
    public interface ITestDal : IGenericDal<Test>
    {
        public List<Test> TestList(int userId);

    }
}

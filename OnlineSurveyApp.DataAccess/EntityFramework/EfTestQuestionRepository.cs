using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Repositories;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.EntityFramework
{
    public class EfTestQuestionRepository : NoGenericRepository<TestQuestion>,ITestQuestionDal
    {
    }
}

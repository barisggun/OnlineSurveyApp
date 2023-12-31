﻿using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.Repositories;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.EntityFramework
{
    public class EfTestRepository : GenericRepository<Test>, ITestDal
    {
        Context c = new Context();

        public List<Test> TestList(int userId)
        {
            return c.Tests.Where(t => t.AppUserId == userId).ToList();
        }


    }
}

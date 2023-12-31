﻿using Microsoft.EntityFrameworkCore;
using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.Repositories;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.EntityFramework
{
    public class EfQuestionRepository : GenericRepository<Question>, IQuestionDal
    {
        Context c = new Context();
        public List<Question> StatusTrueQuestions()
        {
            return c.Questions.Where(x => x.Status == true).ToList();
        }

        public List<Question> StatusFalseQuestions()
        {
            return c.Questions.Where(x => x.Status == false).ToList();
        }
    }
}

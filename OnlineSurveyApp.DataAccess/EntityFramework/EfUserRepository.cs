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
    public class EfUserRepository : NoGenericRepository<AppUser>, IUserDal
    {
        Context c = new Context();
        public int GetUserIdByUserName(string username)
        {
            return c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
        }
    }
}

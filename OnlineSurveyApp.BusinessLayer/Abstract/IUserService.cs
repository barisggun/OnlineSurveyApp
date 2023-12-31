﻿using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<AppUser>
    {
        int GetUserIdByUserName(string username);
    }
}

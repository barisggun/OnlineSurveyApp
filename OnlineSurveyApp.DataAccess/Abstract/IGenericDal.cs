﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> filter);    
        void Create(T entity);  
        void Update(T entity);  
        void Delete(T entity);
    }
}
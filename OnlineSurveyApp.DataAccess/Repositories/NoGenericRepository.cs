using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Repositories
{
    public class NoGenericRepository<T> : IGenericDal<T> where T : class
    {
        //public void Delete(T t)
        //{
        //    using (var context = new Context())
        //    {
        //        context.Remove(t);
        //        context.SaveChanges();
        //    }
        //}

        //public T GetById(int id)
        //{
        //    using var c = new Context();
        //    return c.Set<T>().Find(id);
        //}

        //public void Create(T t)
        //{
        //    using var c = new Context();
        //    c.Add(t);
        //    c.SaveChanges();
        //}

        //public void Update(T t)
        //{
        //    using var c = new Context();
        //    c.Update(t);
        //    c.SaveChanges();
        //}


        //public List<T> GetAll()
        //{
        //    using var c = new Context();
        //    return c.Set<T>().ToList();
        //}

        //public List<T> GetAll(Expression<Func<T, bool>> filter)
        //{
        //    using var c = new Context();
        //    return c.Set<T>().Where(filter).ToList();
        //}
        Context c = new Context();

        public void Delete(T t)
        {
            c.Remove(t);
            c.SaveChanges();
        }

        public T GetByID(int id)
        {

            return c.Set<T>().Find(id);
        }

        public void Insert(T t)
        {

            c.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {

            c.Update(t);
            c.SaveChanges();
        }

        public List<T> GetList()
        {

            return c.Set<T>().ToList();
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {

            return c.Set<T>().Where(filter).ToList();
        }
    }
}


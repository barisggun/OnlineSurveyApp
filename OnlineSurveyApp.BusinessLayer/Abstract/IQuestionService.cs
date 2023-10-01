using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IQuestionService
    {
        Question GetById(int id);
        List<Question> GetAll();
        void Create(Question question);
        void Update(Question question);
        void Delete(Question question);
    }
}

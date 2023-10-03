using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IQuestionService : IGenericService<Question>
    {
        public List<Question> StatusTrueQuestions();
        public List<Question> StatusFalseQuestions();
    }
}

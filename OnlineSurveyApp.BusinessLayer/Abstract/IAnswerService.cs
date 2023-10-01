using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IAnswerService
    {
        Answer GetById(int id);
        List<Answer> GetAll();
        void Create(Answer answer);
        void Update(Answer answer);
        void Delete(Answer answer);
        List<Answer> GetQuestionWithAnswers(int questionId);
    }
}

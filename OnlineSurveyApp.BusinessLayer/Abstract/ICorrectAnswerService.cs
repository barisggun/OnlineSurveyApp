using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface ICorrectAnswerService
    {
        CorrectAnswer GetById(int id);
        List<CorrectAnswer> GetAll();
        void Create(CorrectAnswer correctAnswer);
        void Update(CorrectAnswer correctAnswer);
        void Delete(CorrectAnswer correctAnswer);
    }
}

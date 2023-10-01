using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Abstract
{
    public interface IAnswerDal : IGenericDal<Answer>
    {
        List<Answer> GetQuestionWithAnswers(int questionId);
    }
}

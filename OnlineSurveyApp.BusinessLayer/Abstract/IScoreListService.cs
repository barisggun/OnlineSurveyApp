using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IScoreListService
    {
        ScoreList GetById(int id);
        List<ScoreList> GetAll();
        void Create(ScoreList scoreList);
        void Update(ScoreList scoreList);
        void Delete(ScoreList scoreList);
    }
}

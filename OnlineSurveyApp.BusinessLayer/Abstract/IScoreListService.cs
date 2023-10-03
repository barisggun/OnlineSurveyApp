using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IScoreListService : IGenericService<ScoreList>
    {
        public List<ScoreList> Scores(int testId);
        public List<ScoreList> RemoveTestWithScoreList(int testId);
    }
}

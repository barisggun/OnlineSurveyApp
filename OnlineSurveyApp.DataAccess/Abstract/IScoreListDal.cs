using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.Abstract
{
    public interface IScoreListDal : IGenericDal<ScoreList>
    {
        public List<ScoreList> Scores(int testId);
        public List<ScoreList> RemoveTestWithScoreList(int testId);
    }
}

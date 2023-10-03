using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.DataAccess.Repositories;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.DataAccess.EntityFramework
{
    public class EfScoreListRepository : GenericRepository<ScoreList>, IScoreListDal
    {
        Context c = new Context();

        public List<ScoreList> RemoveTestWithScoreList(int testId)
        {
            var scoreLists = c.ScoreLists.Where(sl => sl.TestId == testId).ToList();

            c.ScoreLists.RemoveRange(scoreLists);
            c.SaveChanges();

            return scoreLists;
        }

        public List<ScoreList> Scores(int testId)
        {
            return c.ScoreLists
                .Where(sl => sl.TestId == testId)
                .OrderByDescending(sl => sl.Score)
                .ToList();
        }
    }
}

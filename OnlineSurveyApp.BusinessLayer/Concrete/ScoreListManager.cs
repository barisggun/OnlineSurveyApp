using OnlineSurveyApp.BusinessLayer.Abstract;
using OnlineSurveyApp.DataAccess.Abstract;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Concrete
{
    public class ScoreListManager : IScoreListService
    {
        IScoreListDal _scoreListDal;

        public ScoreListManager(IScoreListDal scoreListDal)
        {
            _scoreListDal = scoreListDal;
        }

        public void Create(ScoreList scoreList)
        {
            _scoreListDal.Create(scoreList);    
        }

        public void Delete(ScoreList scoreList)
        {
            _scoreListDal.Delete(scoreList);    
        }

        public List<ScoreList> GetAll()
        {
            return _scoreListDal.GetAll();
        }

        public ScoreList GetById(int id)
        {
            return _scoreListDal.GetById(id);
        }

        public void Update(ScoreList scoreList)
        {
            _scoreListDal.Update(scoreList);
        }
    }
}

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

        public void TAdd(ScoreList t)
        {
            _scoreListDal.Insert(t);
        }

        public void TDelete(ScoreList t)
        {
            _scoreListDal.Delete(t);
        }

        public ScoreList TGetById(int id)
        {
            return _scoreListDal.GetByID(id);
        }

        public List<ScoreList> TGetList()
        {
            return _scoreListDal.GetList();
        }

        public void TUpdate(ScoreList t)
        {
            _scoreListDal.Update(t);
        }
    }
}

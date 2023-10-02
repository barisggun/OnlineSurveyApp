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
    public class GuestManager : IGuestService
    {
        IGuestDal _guestDal;

        public GuestManager(IGuestDal guestDal)
        {
            _guestDal = guestDal;
        }

        public void TAdd(Guest t)
        {
            _guestDal.Insert(t);
        }

        public void TDelete(Guest t)
        {
            _guestDal.Delete(t);
        }

        public Guest TGetById(int id)
        {
            return _guestDal.GetByID(id);
        }

        public List<Guest> TGetList()
        {
            return _guestDal.GetList();
        }

        public void TUpdate(Guest t)
        {
            _guestDal.Update(t);
        }
    }
}

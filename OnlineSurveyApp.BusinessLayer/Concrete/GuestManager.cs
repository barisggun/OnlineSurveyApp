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

        public void Create(Guest guest)
        {
            _guestDal.Create(guest);    
        }

        public void Delete(Guest guest)
        {
            _guestDal.Delete(guest);    
        }

        public List<Guest> GetAll()
        {
            return _guestDal.GetAll();
        }

        public Guest GetById(int id)
        {
            return _guestDal.GetById(id);  
        }

        public void Update(Guest guest)
        {
            _guestDal.Update(guest);
        }
    }
}

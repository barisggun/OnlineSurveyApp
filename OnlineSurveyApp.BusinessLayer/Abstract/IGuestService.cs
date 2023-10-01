using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.BusinessLayer.Abstract
{
    public interface IGuestService
    {
        Guest GetById(int id);
        List<Guest> GetAll();
        void Create(Guest guest);
        void Update(Guest guest);
        void Delete(Guest guest);
    }
}

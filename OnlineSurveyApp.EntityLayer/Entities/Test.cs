using OnlineSurveyApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.EntityLayer.Entities
{
    public class Test : BaseEntity
    {
        public int? AppUserId { get; set; }  
        public AppUser? AppUsers { get; set; }
        public int? GuestId { get; set; }
        public Guest? Guests { get; set; }
        public List<Question>? Questions { get; set; }
        public List<ScoreList>? ScoreLists { get; set; }
    }
}

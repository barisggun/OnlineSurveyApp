using OnlineSurveyApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.EntityLayer.Entities
{
    public class ScoreList : BaseEntity
    {
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? GuestId { get; set; }
        public Guest? Guest { get; set; }
        public int TestId { get; set; } 
        public Test Test { get; set; }
        public int Score {  get; set; } 
    }
}

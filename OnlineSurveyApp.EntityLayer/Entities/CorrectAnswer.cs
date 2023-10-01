using OnlineSurveyApp.EntityLayer.Base;
using OnlineSurveyApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.EntityLayer.Entities
{
    public class CorrectAnswer : BaseEntity
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int Correct { get; set; }
        public int TestId { get; set; } 
        public Test Test { get; set; }
    }
}

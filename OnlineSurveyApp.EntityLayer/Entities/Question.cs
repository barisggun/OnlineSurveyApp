using OnlineSurveyApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.EntityLayer.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public bool? Status { get; set; }
        public List<Test>? Tests { get; set; }
    }
}

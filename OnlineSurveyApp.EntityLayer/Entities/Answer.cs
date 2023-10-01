﻿using OnlineSurveyApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSurveyApp.EntityLayer.Entities
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }  
    }
}
 
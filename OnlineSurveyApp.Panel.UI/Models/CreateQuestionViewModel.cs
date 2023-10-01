using OnlineSurveyApp.EntityLayer.Entities;

namespace OnlineSurveyApp.Panel.UI.Models
{
    public class CreateQuestionViewModel
    {
        public string Text { get; set; }
        public List<string> AnswerTexts { get; set; }
        //public int? CorrectAnswerIndex { get; set; }
        public int? AppUserId { get; set; }  

    }
}

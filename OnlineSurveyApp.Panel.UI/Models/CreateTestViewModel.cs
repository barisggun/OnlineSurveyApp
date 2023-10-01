using OnlineSurveyApp.EntityLayer.Entities;

namespace OnlineSurveyApp.Panel.UI.Models
{
    public class CreateTestViewModel
    {
        //public int  QuestionId {get; set;}
        //public Question Question { get; set; }  
        public int SelectedQuestionId { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> SelectedQuestionAnswers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public int CorrectAnswer { get; set; }
        public int CurrentQuestionNumber { get; set; }
    }
}

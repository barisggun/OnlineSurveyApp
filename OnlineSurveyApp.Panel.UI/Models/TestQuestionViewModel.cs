using OnlineSurveyApp.EntityLayer.Entities;

namespace OnlineSurveyApp.Panel.UI.Models
{
    public class TestQuestionViewModel
    {
        public int? AppUserId { get; set; }
        public int? GuestId { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public int? SelectedAnswer { get; set; }
        public int CorrectAnswerId { get; set; }
        public int TestId { get; set; } 
        public int QuestionId { get; set; } 
    }
}

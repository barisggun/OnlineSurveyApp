using FluentValidation;
using OnlineSurveyApp.Panel.UI.Models;

namespace OnlineSurveyApp.Panel.UI.ValidationRules
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionViewModel>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.Text).MinimumLength(3).WithMessage("Soru alanı minimum 3 karakterli olmalıdır");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Bu alanı boş geçemezsiniz");
            RuleFor(x => x.AnswerTexts)
    .Must(list => list != null && list.All(item => !string.IsNullOrWhiteSpace(item)))
    .WithMessage("Cevap alanları boş geçilemez");

            RuleForEach(x => x.AnswerTexts)
    .NotEmpty().WithMessage("Cevap şıkkı boş geçilemez");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspireSurvey.Core
{
    public class SurveyResult
    {
        public Guid Id { get; set; }

        public Guid SurveyId { get; set; }

        public List<SurveyQuestionAnswer> Answers { get; set; }

        public T GetAnswerForQuestionWithId<T>(Guid questionId) where T : SurveyQuestionAnswer
        {
            return Answers.OfType<T>().First(a => a.QuestionId == questionId);
        }

        public static SurveyResult CreateNewFrom(Survey survey)
        {
            return new SurveyResult()
            {
                Id = Guid.NewGuid(),
                SurveyId = survey.Id,
                Answers = survey.Questions.Select(q => CreateAnswerFrom(q)).ToList(),
            };
        }

        private static SurveyQuestionAnswer CreateAnswerFrom(SurveyQuestion surveyQuestion)
        {
            switch (surveyQuestion)
            {
                case FreeText freeText:
                    return new FreeTextAnswer()
                    {
                        QuestionId = freeText.Id,
                        Id = Guid.NewGuid(),
                        Answer = string.Empty,
                    };
                default:
                    throw new NotImplementedException($"No survey answer for type {surveyQuestion.GetType()}");
            }
        }
    }

    [JsonDerivedType(typeof(FreeTextAnswer), nameof(FreeTextAnswer))]
    public abstract class SurveyQuestionAnswer
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
    }

    public class FreeTextAnswer : SurveyQuestionAnswer
    {
        public string Answer { get; set; }
    }
}

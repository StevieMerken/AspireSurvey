using System.Text.Json.Serialization;

namespace AspireSurvey.Core
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<SurveyQuestion> Questions { get; set; }
    }

    [JsonDerivedType(typeof(FreeText), nameof(FreeText))]
    public abstract class SurveyQuestion
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public bool Required { get; set; }
    }

    public class FreeText : SurveyQuestion
    {
        public int MaxLength { get; set; }
    }
}

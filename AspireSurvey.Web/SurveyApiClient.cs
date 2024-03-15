using AspireSurvey.Core;

namespace AspireSurvey.Web;

public class SurveyApiClient(HttpClient httpClient)
{
    public async Task<Survey> GetSurveyByIdAsync(string id)
    {
        return await httpClient.GetFromJsonAsync<Survey>($"/survey/{id}");
    }

    public async Task<HttpResponseMessage> SubmitSurveyAsync(SurveyResult surveyResult)
    {
        return await httpClient.PostAsJsonAsync<SurveyResult>("/survey", surveyResult);
    }
}

using Newtonsoft.Json;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.Data
{
    public class ProvServices
    {
        private readonly HttpClient _httpClient;

        public ProvServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizViewModel>> GetQuizzesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44320/api/quiz");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<QuizViewModel>>(json);
        }
    }
}

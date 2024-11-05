namespace umbraco_lingoquest.Moduller
{
    public class QuizViewModel
    {
        public int Id { get; set; }
        public string? EnglishText { get; set; }
        public string? SwedishText { get; set; }
        public List<string>? MissingWords { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}

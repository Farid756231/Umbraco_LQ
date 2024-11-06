namespace umbraco_lingoquest.Moduller
{
    public class Prov2Quizz
    {
        public int id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}

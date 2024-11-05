using umbraco_lingoquest.Data;
using umbraco_lingoquest.Moduller;

namespace umbraco_lingoquest.ServiceModil
{
    public class SeedDatas
    {

        public static void Initialize(DataContext context)
        {
            if (!context.Quizzes.Any())
            {
                var quizzes = new List<QuizViewModel>
                {
                    new QuizViewModel
                    {
                        EnglishText = "The cat is sitting comfortably on the roof enjoying the view.",
                        SwedishText = "Katten sitter bekvämt på ___ och njuter av utsikten.",
                        MissingWords = new List<string> { "taket", "golvet", "trädet" },
                        CorrectAnswer = "taket"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "I love to eat pasta because it's delicious and easy to make.",
                        SwedishText = "Jag älskar att äta ___ eftersom det är gott och lätt att göra.",
                        MissingWords = new List<string> { "pasta", "pizza", "sushi" },
                        CorrectAnswer = "pasta"
                    },
                     new QuizViewModel
                    {
                        EnglishText = "The sky is blue today, with no clouds in sight.",
                        SwedishText = "Himlen är ___ idag, utan några moln i sikte.",
                        MissingWords = new List<string> { "blå", "grön", "grå" },
                         CorrectAnswer = "blå"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "She is reading a very interesting book about history.",
                        SwedishText = "Hon läser en mycket intressant ___ om historia.",
                        MissingWords = new List<string> { "bok", "tidning", "tidsskrift" },
                        CorrectAnswer = "bok"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "The sun is very bright today, making it hard to see without sunglasses.",
                        SwedishText = "Solen är väldigt ___ idag, vilket gör det svårt att se utan solglasögon.",
                        MissingWords = new List<string> { "ljus", "mörk", "kall" },
                        CorrectAnswer = "ljus"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "He is a great teacher who makes learning fun for everyone.",
                        SwedishText = "Han är en fantastisk ___ som gör lärandet roligt för alla.",
                        MissingWords = new List<string> { "lärare", "förare", "sångare" },
                        CorrectAnswer = "lärare"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "They went to the park to enjoy the sunny weather and relax.",
                        SwedishText = "De gick till ___ för att njuta av det soliga vädret och koppla av.",
                        MissingWords = new List<string> { "parken", "stranden", "köpcentret" },
                        CorrectAnswer = "parken"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "My favorite color is blue, but I also like green and red.",
                        SwedishText = "Min favoritfärg är ___, men jag gillar också grönt och rött.",
                        MissingWords = new List<string> { "blå", "grön", "röd" },
                        CorrectAnswer = "blå"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "I enjoy playing soccer with my friends during the weekends.",
                        SwedishText = "Jag gillar att spela ___ med mina vänner under helgerna.",
                        MissingWords = new List<string> { "fotboll", "schack", "gitarr" },
                        CorrectAnswer = "fotboll"
                    },
                    new QuizViewModel
                    {
                        EnglishText = "Winter is very cold here, with temperatures often dropping below freezing.",
                        SwedishText = "Vintern är väldigt ___ här, med temperaturer som ofta faller under fryspunkten.",
                        MissingWords = new List<string> { "kall", "het", "regnig" },
                        CorrectAnswer = "kall"
                    }
                };

                context.Quizzes.AddRange(quizzes);
               
            }

            if (!context.Prov2Quizzs.Any())
            {
                var grammarQuiz = new List<Prov2Quizz>
                {
                    new Prov2Quizz
                    {
                        Question = "Which of the following sentences is grammatically correct?",
                        Options = new List<string>
                        {
                            "She don't like apples.",
                            "She doesn't likes apples.",
                            "She doesn't like apples.",
                            "She isn't like apples."
                        },
                        CorrectAnswer = "She doesn't like apples."
                    },
                    new Prov2Quizz
                    {
                        Question = "Choose the correct form of the verb in the following sentence: 'Neither of the students _____ finished their work.'",
                        Options = new List<string> { "has", "have", "is", "are" },
                        CorrectAnswer = "has"
                    },
                    new Prov2Quizz
                    {
                        Question = "Which sentence uses the correct conditional tense?",
                        Options = new List<string>
                        {
                            "If I knew the answer, I would have told you.",
                            "If I know the answer, I will have told you.",
                            "If I had known the answer, I would tell you.",
                            "If I had known the answer, I would have told you."
                        },
                        CorrectAnswer = "If I had known the answer, I would have told you."
                    },
                    new Prov2Quizz
                    {
                        Question = "Which sentence contains a dangling modifier?",
                        Options = new List<string>
                        {
                            "Running quickly, the finish line seemed close.",
                            "Running quickly, she reached the finish line.",
                            "She was running quickly to the finish line.",
                            "The finish line was reached by her running quickly."
                        },
                        CorrectAnswer = "Running quickly, the finish line seemed close."
                    },
                    new Prov2Quizz
                    {
                        Question = "What is the correct usage of 'who' and 'whom' in the following sentence? 'To _____ did you give the book?'",
                        Options = new List<string> { "who", "whom", "whoever", "whomever" },
                        CorrectAnswer = "whom"
                    },
                    new Prov2Quizz
                    {
                        Question = "Which of the following sentences correctly uses a semicolon?",
                        Options = new List<string>
                        {
                            "I have a big exam tomorrow; I can't go out tonight.",
                            "I have a big exam tomorrow; because of that, I can't go out tonight.",
                            "I have a big exam tomorrow; and I can't go out tonight.",
                            "I have a big exam tomorrow; but I can't go out tonight."
                        },
                        CorrectAnswer = "I have a big exam tomorrow; I can't go out tonight."
                    },
                    new Prov2Quizz
                    {
                        Question = "Identify the sentence with the correct subject-verb agreement.",
                        Options = new List<string>
                        {
                            "The group of students are working on their project.",
                            "The group of students is working on their project.",
                            "The group of students were working on their project.",
                            "The group of students has been working on their project."
                        },
                        CorrectAnswer = "The group of students is working on their project."
                    },
                    new Prov2Quizz
                    {
                        Question = "Which of the following sentences is in the passive voice?",
                        Options = new List<string>
                        {
                            "The chef prepared the meal.",
                            "The meal was prepared by the chef.",
                            "The chef is preparing the meal.",
                            "The meal is preparing by the chef."
                        },
                        CorrectAnswer = "The meal was prepared by the chef."
                    },
                    new Prov2Quizz
                    {
                        Question = "What is the correct plural form of 'analysis'?",
                        Options = new List<string> { "analysis's", "analysises", "analyses", "analysis" },
                        CorrectAnswer = "analyses"
                    },
                    new Prov2Quizz
                    {
                        Question = "In which sentence is 'fewer' used correctly?",
                        Options = new List<string>
                        {
                            "I have fewer water than you.",
                            "There are fewer students in the class today.",
                            "Fewer people are attending the concert than last year.",
                            "The fewer cars on the road, the better the traffic will be."
                        },
                        CorrectAnswer = "There are fewer students in the class today."
                    }
                };

                context.Prov2Quizzs.AddRange(grammarQuiz);
            }
            context.SaveChanges();
        }
    }
}

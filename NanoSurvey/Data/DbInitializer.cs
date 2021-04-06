using System.Linq;
using NanoSurvey.Data.Entities;

namespace NanoSurvey.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SurveyContext context)
        {
            if (context.Surveys.Any()) return;

            var surveys = new[]
            {
                new Survey
                {
                    Title = "Новый век",
                    Description = "Телефоны и прочее"
                },
                new Survey
                {
                    Title = "Любимые места Москвы",
                    Description = "Москва и москвичи"
                }
            };

            context.Surveys.AddRange(surveys);
            context.SaveChanges();

            var questions = new[]
            {
                new Question
                {
                    Text = "Пользуетесь ли вы телефоном?",
                    Survey = surveys[0]
                },
                new Question
                {
                    Text = "Какой у вас телефон?",
                    Survey = surveys[0]
                },
                new Question
                {
                    Text = "Какой ваше любимое место Москвы?",
                    Survey = surveys[1]
                }
            };

            context.Questions.AddRange(questions);
            context.SaveChanges();

            var answers = new[]
            {
                new Answer
                {
                    Text = "Да",
                    Question = questions[0]
                },
                new Answer
                {
                    Text = "Нет",
                    Question = questions[0]
                },
                new Answer
                {
                    Text = "Айфон",
                    Question = questions[1]
                },
                new Answer
                {
                    Text = "Андроид",
                    Question = questions[1]
                },
                new Answer
                {
                    Text = "Красная площадь",
                    Question = questions[2]
                },
                new Answer
                {
                    Text = "ВДНХ",
                    Question = questions[2]
                },
                new Answer
                {
                    Text = "Джипси",
                    Question = questions[2]
                }
            };

            context.Answers.AddRange(answers);
            context.SaveChanges();
        }
    }
}
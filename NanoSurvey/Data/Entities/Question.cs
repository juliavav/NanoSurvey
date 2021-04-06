using System.Collections.Generic;

namespace NanoSurvey.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Survey Survey { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
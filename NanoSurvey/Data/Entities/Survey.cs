using System.Collections.Generic;

namespace NanoSurvey.Data.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Interview> Interviews { get; set; }
    }
}
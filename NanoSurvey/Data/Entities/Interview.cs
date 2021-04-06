using System;
using System.Collections.Generic;

namespace NanoSurvey.Data.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset Date { get; set; }

        public ICollection<Result> Results { get; set; }
    }
}
namespace NanoSurvey.Data.Entities
{
    public class Result
    {
        public int Id { get; set; }

        public Interview Interview { get; set; }
        public Survey Survey { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
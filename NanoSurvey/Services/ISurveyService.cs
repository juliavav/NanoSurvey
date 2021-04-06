using System.Threading.Tasks;
using NanoSurvey.Data.Entities;

namespace NanoSurvey.Services
{
    public interface ISurveyService
    {
        Task<Question> GetQuestion(int surveyId, int questionId);
        Task AddResult(int surveyId, int questionId, int answerId);
        Task<int> GetNextQuestionId(int surveyId, int questionId);
    }
}
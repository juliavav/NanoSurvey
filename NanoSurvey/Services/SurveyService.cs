using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NanoSurvey.Data;
using NanoSurvey.Data.Entities;

namespace NanoSurvey.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly Func<SurveyContext> factory;

        public SurveyService(Func<SurveyContext> factory)
        {
            this.factory = factory;
        }

        public async Task<Question> GetQuestion(int surveyId, int questionId)
        {
            var survey = await factory().Surveys.Include(n => n.Questions).ThenInclude(a => a.Answers)
                .SingleAsync(s => s.Id == surveyId);
            var question = survey.Questions.Single(q => q.Id == questionId);
            return question;
        }

        public async Task AddResult(int surveyId, int questionId, int answerId)
        {
            var survey = await factory().Surveys.Include(n => n.Questions).ThenInclude(n => n.Answers)
                .SingleAsync(s => s.Id == surveyId);
            var question = survey.Questions.Single(q => q.Id == questionId);
            var answer = question.Answers.Single(a => a.Id == answerId);
            //необходимо ещё обновить Interview, оставила без изменений, так как создавать Interview логичнее при первом открытии опроса
            await factory().Results
                .AddAsync(new Result {Answer = answer, Question = question, Survey = survey});
            await factory().SaveChangesAsync();
        }

        public async Task<int> GetNextQuestionId(int surveyId, int questionId)
        {
            //необходимо обговорить правила, по которым выбирается след вопрос
            var survey = await factory().Surveys.Include(n => n.Questions)
                .SingleAsync(s => s.Id == surveyId);
            var question = survey.Questions.Single(q => q.Id == questionId);
            var questionList = survey.Questions.ToList();
            var questionIndex = questionList.IndexOf(question);
            
            //необходимо обговорить, как сообщать то, что вопрос последний
            return questionList[questionIndex + 1].Id;
        }
    }
}
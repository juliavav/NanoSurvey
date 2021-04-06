using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NanoSurvey.Data.Models;
using NanoSurvey.Services;

namespace NanoSurvey.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            this.surveyService = surveyService;
        }

        [HttpGet("surveys/{idSurvey}/questions/{idQuestion}")]
        public async Task<IActionResult> GetQuestion(int idSurvey, int idQuestion)
        {
            try
            {
                var question = await surveyService.GetQuestion(idSurvey, idQuestion);
                var model = new QuestionModel
                {
                    Text = question.Text,
                    Answers = question.Answers.Select(a => a.Text).ToArray()
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpPost("surveys/{idSurvey}/questions/{idQuestion}")]
        public async Task<IActionResult> UpdateStatus(int idSurvey, int idQuestion, [FromBody] ResultModel answer)
        {
            //ResultModel должна обговариваться с фронтом
            try
            {
                await surveyService.AddResult(idSurvey, idQuestion, answer.Id);
                var nextQuestion = await surveyService.GetNextQuestionId(idSurvey, idQuestion);
                return Ok(nextQuestion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}
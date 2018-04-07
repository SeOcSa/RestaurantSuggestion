using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSuggestion.Data.Models;
using RestaurantSuggestion.Data.Services;

namespace RestaurantSuggestion.InferenceMachine
{
    public class InferenceMachine : IInferenceMachine
    {
        private readonly IQuestionSql _questionService;
        private readonly IAnswerSql _answerService;

        public InferenceMachine(IQuestionSql questionService, IAnswerSql answerService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        public AnswerModel GetAnswer(Guid Id)
        {
            return _answerService.GetAnswer(Id);
        }

        public List<AnswerModel> GetAnswersForQuestion(Guid Id)
        {
            return _answerService.GetAnswersForQuestion(Id).OrderBy(x => x.AnswerText).ToList();
        }

        public QuestionModel GetQuestion(Guid Id)
        {
            return _questionService.GetQuestion(Id);
        }

        public QuestionModel GetStartUpQuestion()
        {
            return _questionService.GetFristQuestion();
        }


    }
}

using RestaurantSuggestion.Data.Models;
using System;
using System.Collections.Generic;

namespace RestaurantSuggestion.InferenceMachine
{
    public interface IInferenceMachine
    {
        QuestionModel GetStartUpQuestion();
        QuestionModel GetQuestion(Guid Id);
        AnswerModel GetAnswer(Guid Id);
        List<AnswerModel> GetAnswersForQuestion(Guid Id);

    }
}

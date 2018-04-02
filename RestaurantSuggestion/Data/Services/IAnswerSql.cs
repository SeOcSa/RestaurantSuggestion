using RestaurantSuggestion.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantSuggestion.Data.Services
{
    public interface IAnswerSql
    {
        List<AnswerModel> GetAnswersForQuestion(Guid Id);
        void AddAnswer(AnswerModel answerModel);
        void Dispose();
        AnswerModel GetAnswer(Guid Id);
    }
}

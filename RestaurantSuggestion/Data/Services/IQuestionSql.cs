using RestaurantSuggestion.Data.Models;
using System;

namespace RestaurantSuggestion.Data.Services
{
    public interface IQuestionSql
    {
        QuestionModel GetQuestion(Guid Id);
        QuestionModel GetFristQuestion();
        void AddQuestion(QuestionModel model);
    }
}

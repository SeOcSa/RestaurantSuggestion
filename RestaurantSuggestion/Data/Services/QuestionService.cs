using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantSuggestion.Data.DbContext;
using RestaurantSuggestion.Data.Models;

namespace RestaurantSuggestion.Data.Services
{
    public class QuestionService : IQuestionSql
    {
        private readonly RestaurantSuggestionDbContext _context;
        public QuestionService(RestaurantSuggestionDbContext context)
        {
            _context = context;
        }

        public QuestionModel GetFristQuestion()
        {
            return _context.Questions.FirstOrDefault(x=> x.QuestionId.ToString() == "2cc17814-e2f7-4c58-8f3f-7c93613fe731");
        }

        public QuestionModel GetQuestion(Guid Id)
        {
            return _context.Questions.FirstOrDefault(x => x.QuestionId == Id);
        }

        public void AddQuestion(QuestionModel question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }
    }
}

using RestaurantSuggestion.Data.DbContext;
using RestaurantSuggestion.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSuggestion.Data.Services
{
    public class AnswerService : IAnswerSql
    {
        private readonly RestaurantSuggestionDbContext _context;
        public AnswerService(RestaurantSuggestionDbContext context)
        {
            _context = context;
        }

        public void AddAnswer(AnswerModel answerModel)
        {
            _context.Answers.Add(answerModel);
            _context.SaveChanges();
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public AnswerModel GetAnswer(Guid Id)
        {
            return _context.Answers.FirstOrDefault(x => x.AnswerId == Id);
        }

        public List<AnswerModel> GetAnswersForQuestion(Guid Id)
        {
            return _context.Answers.Where(x => x.QuestionId == Id).ToList();
        }


    }
}

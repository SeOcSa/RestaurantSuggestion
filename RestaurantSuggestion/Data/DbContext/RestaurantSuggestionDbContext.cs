using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantSuggestion.Data.Models;

namespace RestaurantSuggestion.Data.DbContext
{
    public class RestaurantSuggestionDbContext :IdentityDbContext
    {
        public RestaurantSuggestionDbContext(DbContextOptions dbContextOption) :base(dbContextOption){}

        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
    }
}

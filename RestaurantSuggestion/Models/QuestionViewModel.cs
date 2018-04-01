using System;
using System.Collections.Generic;

namespace RestaurantSuggestion.Models
{
    public class QuestionViewModel
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerViewModel> QuestionAnswers {get; set;}
    }
}

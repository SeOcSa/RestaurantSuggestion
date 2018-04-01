using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSuggestion.Data.Models
{
    public class AnswerModel
    {
        [Key]
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }
        [ForeignKey("QuestionModel")]
        public Guid QuestionId { get; set; }
        [ForeignKey("QuestionModel")]
        public Guid NextQuestionId { get; set; }
    }
}

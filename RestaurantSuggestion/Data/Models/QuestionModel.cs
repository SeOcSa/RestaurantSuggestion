using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSuggestion.Data.Models
{
    public class QuestionModel
    {
        [Key]
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}

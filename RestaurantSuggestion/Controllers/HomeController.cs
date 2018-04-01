using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSuggestion.Models;

namespace RestaurantSuggestion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new QuestionViewModel{
                QuestionId= Guid.NewGuid(),
                QuestionText= "Ce tip de mancare preferi?",
                QuestionAnswers= new List<AnswerViewModel>
                {
                    new AnswerViewModel{ AnswerId= Guid.NewGuid(), AnswerText="Gatit"},
                    new AnswerViewModel{AnswerId= Guid.NewGuid(), AnswerText="Fast food"}

                }
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

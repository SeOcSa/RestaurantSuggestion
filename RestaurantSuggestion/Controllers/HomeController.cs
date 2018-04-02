using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSuggestion.Data.Models;
using RestaurantSuggestion.Data.Services;
using RestaurantSuggestion.Models;

namespace RestaurantSuggestion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionSql _questionService;
        private readonly IAnswerSql _answerService;
        public HomeController(IQuestionSql questionService, IAnswerSql answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }
        bool FirstRun = true;
        public IActionResult Index()
        {
            var item = _questionService.GetFristQuestion();
            var itemAnswers = _answerService.GetAnswersForQuestion(item.QuestionId).OrderBy(x => x.AnswerText).ToList();
            return View(new BigViewModel
            {
                QuestionViewModel= new QuestionViewModel
                {
                    QuestionId = item.QuestionId,
                    QuestionText = item.QuestionText,
                    QuestionAnswers = mapAnswer(itemAnswers)
                }
            });
        }
        private List<AnswerViewModel> mapAnswer(List<AnswerModel> list)
        {
            List<AnswerViewModel> retVal = new List<AnswerViewModel>();

            foreach (var item in list)
                retVal.Add(new AnswerViewModel { AnswerId = item.AnswerId, AnswerText = item.AnswerText });

            return retVal;
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpPost]
        public IActionResult About(BigViewModel viewModel)
        {
            var item = new QuestionModel
            {
                QuestionId = Guid.NewGuid(),
                QuestionText = viewModel.AddViewModel.QuestionText
            };
            _questionService.AddQuestion(item);

            var answer1 = new AnswerModel
            {
                AnswerId = Guid.NewGuid(),
                AnswerText = viewModel.AddViewModel.Answer1,
                QuestionId = item.QuestionId
            };
            _answerService.AddAnswer(answer1);

            if (viewModel.AddViewModel.Answer2 != null)
            {
                var answer2 = new AnswerModel
                {
                    AnswerId = Guid.NewGuid(),
                    AnswerText = viewModel.AddViewModel.Answer2,
                    QuestionId = item.QuestionId
                };
                _answerService.AddAnswer(answer2);
            }

            if (viewModel.AddViewModel.Answer3 != null)
            {
                var answer3 = new AnswerModel
                {
                    AnswerId = Guid.NewGuid(),
                    AnswerText = viewModel.AddViewModel.Answer3,
                    QuestionId = item.QuestionId
                };
                _answerService.AddAnswer(answer3);
                _answerService.Dispose();
            }
           
            return RedirectToAction("Index");
        }

        public IActionResult NextQuestion(Guid AnswerId)
        {
            var answerItem = _answerService.GetAnswer(AnswerId);
            var questionItem = _questionService.GetQuestion(answerItem.NextQuestionId);
            if (questionItem == null)
                return View(new BigViewModel
                {
                    QuestionViewModel = new QuestionViewModel
                    {
                        QuestionId = answerItem.QuestionId,
                        QuestionText= _questionService.GetQuestion(answerItem.QuestionId).QuestionText,
                        QuestionAnswers = mapAnswer(_answerService.GetAnswersForQuestion(answerItem.QuestionId))
                    }
                });
            var questionItems = _answerService.GetAnswersForQuestion(questionItem.QuestionId);

            var viewModel = new BigViewModel
            {
                QuestionViewModel = new QuestionViewModel
                {
                    QuestionId = questionItem.QuestionId,
                    QuestionText = questionItem.QuestionText,
                    QuestionAnswers = mapAnswer(questionItems)
                }
            };

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

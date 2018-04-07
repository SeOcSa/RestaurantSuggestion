using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSuggestion.Data.Models;
using RestaurantSuggestion.Data.Services;
using RestaurantSuggestion.InferenceMachine;
using RestaurantSuggestion.Models;

namespace RestaurantSuggestion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionSql _questionService;
        private readonly IAnswerSql _answerService;
        private readonly IInferenceMachine _infecenceMachine;

        public HomeController(IQuestionSql questionService, IAnswerSql answerService, IInferenceMachine infecenceMachine)
        {
            _questionService = questionService;
            _answerService = answerService;
            _infecenceMachine = infecenceMachine;
        }
        public IActionResult Index()
        {
            var item = _infecenceMachine.GetStartUpQuestion();
            var itemAnswers = _infecenceMachine.GetAnswersForQuestion(item.QuestionId);

            return View(new BigViewModel
            {
                QuestionViewModel = new QuestionViewModel
                {
                    QuestionId = item.QuestionId,
                    QuestionText = item.QuestionText,
                    QuestionAnswers = mapAnswer(itemAnswers)
                },
                ShowQuestionForm = false
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
            var answerItem = _infecenceMachine.GetAnswer(AnswerId);
            var questionItem = _infecenceMachine.GetQuestion(answerItem.NextQuestionId);

            if (questionItem == null)
            {
                var finalAnswer = _infecenceMachine.GetAnswer(answerItem.NextQuestionId);
                var model = new AnswerViewModel { AnswerId = finalAnswer.AnswerId, AnswerText = finalAnswer.AnswerText };
                return RedirectToAction("FinalAnswer", model);
            }

            var questionItems = _infecenceMachine.GetAnswersForQuestion(questionItem.QuestionId);

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

        public IActionResult FinalAnswer(AnswerViewModel model)
        {
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

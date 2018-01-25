using MVCClient.QuizReference;
using MVCClient.CategoryReference;
using MVCClient.AnswerReference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class QuizViewModel
    {
        public LoginViewModel loginViewModel;
        public CurrentQuizViewModel CurrentQuizViewModel { get; set; }

        public QuizViewModel()
        {
            loginViewModel = new LoginViewModel();
            CurrentQuizViewModel = new CurrentQuizViewModel();
        }

        //QuizReference
        public List<QuizReference.Category> Categories { get; set; }
        public List<QuizReference.Question> Questions { get; set; }

        //UserReference
        public UserReference.User User { get; set; }
    }
}
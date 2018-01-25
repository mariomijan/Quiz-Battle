using MVCClient.LobbyReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class CurrentQuizViewModel
    {
        public CurrentQuizViewModel()
        {
            Login = new LoginViewModel();
            lobbyViewModel = new LobbyViewModel();

            //LobbyReference
            Lobby = new LobbyReference.Lobby();
        }

        public LoginViewModel Login { get; set; }
        public LobbyViewModel lobbyViewModel { get; set; }
        public int CategoryId { get; set; }
        public int lobbyId { get; set; }
        public int ToSkip { get; set; }
        public int Points { get; set; }

        //QuizReference
        public List<QuizReference.Question> Questions { get; set; }
        public QuizReference.Question CurrentQuestion { get; set; }

        //LobbyReference
        public LobbyReference.Lobby Lobby { get; set; }

        //UserReference
        public UserReference.User User { get; set; }
    }
}
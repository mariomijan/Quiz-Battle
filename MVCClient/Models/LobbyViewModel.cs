using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class LobbyViewModel
    {
        //LobbyReferences
        public LobbyReference.Lobby lobby { get; set; }
        public List<LobbyReference.Lobby> lobbies { get; set; }
        public LobbyReference.Category category { get; set; }

        //CategoryReference
        public CategoryReference.Category quizCategory { get; set; }

        //UserReference
        public UserReference.User user { get; set; }
        public bool IsUserInLobby { get; set; }
        public bool DoesUserAlreadyOwnALobby { get; set; }
        public LobbyReference.User lobbyUser { get; set; }
        public List<UserReference.User> users { get; set; }

        //QuizReferences
        public QuizReference.UserQuestion userQuestion { get; set; }
        public QuizReference.User qUser { get; set; }
        public QuizReference.Lobby qLobby { get; set; }
        public QuizReference.Question qQuestion { get; set; }
        public QuizReference.Category qCategory { get; set; }

        public LobbyViewModel()
        {
            //LobbyReferences
            lobbyUser = new LobbyReference.User();
            category = new LobbyReference.Category();
            lobby = new LobbyReference.Lobby();
            lobbies = new List<LobbyReference.Lobby>();

            //CategoryReference
            quizCategory = new CategoryReference.Category();

            //UserReference
            users = new List<UserReference.User>();
            user = new UserReference.User();
            
            //QuizReferences
            userQuestion = new QuizReference.UserQuestion();
            qUser = new QuizReference.User();
            qLobby = new QuizReference.Lobby();
            qQuestion = new QuizReference.Question();
            qCategory = new QuizReference.Category();
        }

        public string PlayerName { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Please specify a lobby name !")]
        [Display(Name = "Lobby name")]
        public string name { get; set; }
        public int categoryId { get; set; }
        [Display(Name = "Is started")]
        public bool isStarted { get; set; }
    }
}
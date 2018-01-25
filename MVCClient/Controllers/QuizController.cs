using Microsoft.AspNet.SignalR;
using MVCClient.AnswerReference;
using MVCClient.CategoryReference;
using MVCClient.LobbyReference;
using MVCClient.LoginServiceReference;
using MVCClient.Models;
using MVCClient.QuizReference;
using MVCClient.Security;
using MVCClient.SignalR;
using MVCClient.UserReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCClient.Controllers
{
    public class QuizController : Controller
    {
        private QuizServiceClient quizClient = new QuizServiceClient();
        private LoginServiceClient loginClient = new LoginServiceClient();
        private UserServiceClient userClient = new UserServiceClient();
        private AnswerServiceClient answerClient = new AnswerServiceClient();
        private LobbyServiceClient lobbyClient = new LobbyServiceClient();
        private CategoryServiceClient categoryClient = new CategoryServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                registerViewModel.user.name = registerViewModel.Name;
                registerViewModel.user.lastName = registerViewModel.LastName;
                registerViewModel.user.country = registerViewModel.country;
                registerViewModel.user.phone = registerViewModel.Phone;
                registerViewModel.login.username = registerViewModel.Username;
                registerViewModel.login.password = registerViewModel.Password;

                if (loginClient.DoesUserExist(registerViewModel.login))
                {
                    loginClient.AddGuestAndLoginEncryptPw(registerViewModel.login, registerViewModel.user);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "A user with the specified username already exist !");
                }
            }
            return View(registerViewModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                loginViewModel.login.username = loginViewModel.Username;
                loginViewModel.login.password = loginViewModel.Password;

                try
                {
                    if (loginClient.DoesUserExist(loginViewModel.login))
                    {
                        ModelState.AddModelError("", "Sorry, we can't find you in our system, please try again or register");
                    }
                    else
                    {
                        var login = loginClient.DecryptPassword(loginViewModel.login);

                        CurrentQuizViewModel quizVm = new CurrentQuizViewModel();
                        quizVm.Login.Username = loginViewModel.Username;
                        SessionLogin.UserName = quizVm.Login.Username;
                        return RedirectToAction("Category");
                    }
                }
                catch (FaultException)
                {
                    ModelState.AddModelError("", "Sorry, we can't find you in our system, please try again or register");
                }
            }
            return View(loginViewModel);
        }

        public ActionResult Category()
        {
            QuizViewModel quizViewModel = new QuizViewModel();

            if (SessionLogin.UserIsInSession() && Session["Point"] == null)
            {
                Session["Point"] = null;
                quizViewModel.Categories = quizClient.GetAllCategories();
                quizViewModel.loginViewModel.Username = SessionLogin.UserName;
                return View(quizViewModel);
            }
            SessionLogin.CloseSession();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CheckAnswer(int answerId, int lobbyId, int categoryId, int userId)
        {
            try
            {
                var dto = answerClient.AnswerQuestion(answerId, lobbyId, userId, categoryId);
                //var hub = GlobalHost.ConnectionManager.GetHubContext<LobbyHub>();
                //hub.Clients.Group(lobbyId.ToString()).hasBeenAnswered(categoryId, lobbyId);
                return Json(dto);
            }
            catch (Exception)
            { 
                return Json(new AnswerQuestionDTO { IsAnswered = true, Answer = new AnswerReference.Answer { isCorrect = false } });
            }
        }                          
        [HttpPost]
        public void Point(int answerId)
        {
            int answerPointAmount = answerClient.GetAnswer(answerId).pointAmount;

            if (Session["Point"] == null)
            {
                Session["Point"] = answerPointAmount;
            }
            else 
            {
                int previousAnswerValue = (int)Session["Point"];
                int newAnswerValue = previousAnswerValue += answerPointAmount;
                Session["Point"] = newAnswerValue;
            }
        }

        //Close session and redirect user to front page on reload
        [HttpPost]
        public ActionResult RedirectToPage(int lobbyId)
        {
            SessionLogin.CloseSession();
            quizClient.ClearTableAfterFinish(lobbyId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void IsStartedTrue(int lobbyId)
        {
            LobbyViewModel lobbyViewModel = new LobbyViewModel();
            lobbyViewModel.lobby.id = lobbyId;
            lobbyClient.UpdateLobbyStatus(lobbyViewModel.lobby);
        }

        [NoDirectAccess]
        public ActionResult Question(int CategoryId, int lobbyId)
        {
            if (SessionLogin.UserIsInSession())
            {
                if (Session["currentQuizViewModel"] == null)
                {
                    var quizVm = new CurrentQuizViewModel { CategoryId = CategoryId, lobbyId = lobbyId };
                    quizVm.User = userClient.GetUserByUsername(SessionLogin.UserName);
                    quizVm.Questions = quizClient.GetAllQuestionsAndAnswersByCategoryId(CategoryId);
                    quizVm.CurrentQuestion = quizVm.Questions.Skip(quizVm.ToSkip).Take(1).FirstOrDefault();
                    quizVm.ToSkip++;
                    Session["currentQuizViewModel"] = quizVm;
                    return View(quizVm);
                }
                else
                {
                    CurrentQuizViewModel quizVm = (CurrentQuizViewModel)Session["currentQuizViewModel"];
                    if (quizVm.CategoryId != CategoryId)
                    {
                        SessionLogin.CloseSession();
                        return RedirectToAction("Index");
                        //Hvis han prøver at skifte quiz undervejs !?
                    }

                    quizVm.CurrentQuestion = quizVm.Questions.Skip(quizVm.ToSkip).Take(1).FirstOrDefault();
                    quizVm.Points = (int)Session["Point"];
                    quizVm.ToSkip++;

                    if (quizVm.CurrentQuestion == null)
                    {
                        Session["currentQuizViewModel"] = null;
                        int totalPoint = (int)Session["Point"];
                        var user = userClient.GetUserByUsername(SessionLogin.UserName);
                        user.point += totalPoint;
                        userClient.AddPointsToUser(user);
                        return RedirectToAction("Finish", new { totalPoints = totalPoint, lobbyId = lobbyId });
                    }
                    return View(quizVm);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Finish(int totalPoints, int lobbyId)
        {
            if (SessionLogin.UserIsInSession() && Session["Point"] != null)
            {
                quizClient.ClearTableAfterFinish(lobbyId);
                QuizViewModel quizViewModel = new QuizViewModel();
                quizViewModel.CurrentQuizViewModel.Points = totalPoints;
                quizViewModel.User = userClient.GetUserByUsername(SessionLogin.UserName);
                quizViewModel.User.login.username = SessionLogin.UserName;
                return View(quizViewModel);
            }
            SessionLogin.CloseSession();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Finish()
        {
            Session["Point"] = null;
            return RedirectToAction("Category");
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Lobby(int categoryId)
        {
            if (SessionLogin.UserIsInSession())
            {
                LobbyViewModel lobbyViewModel = new LobbyViewModel();
                lobbyViewModel.PlayerName = SessionLogin.UserName;
                lobbyViewModel.lobbies = lobbyClient.GetAllLobbiesWithCategoryId(categoryId).OrderByDescending(x => x.id).ToList();
                lobbyViewModel.categoryId = categoryId;
                lobbyViewModel.user = userClient.GetUserByUsername(SessionLogin.UserName);
                lobbyViewModel.IsUserInLobby = lobbyClient.IsUserInLobby(SessionLogin.UserName);
                lobbyViewModel.DoesUserAlreadyOwnALobby = lobbyClient.DoesUserAlreadyOwnALobby(SessionLogin.UserName);
                return View(lobbyViewModel);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateLobby(int categoryId)
        {
            if (SessionLogin.UserIsInSession())
            {
                LobbyViewModel lobbyViewModel = new LobbyViewModel();
                lobbyViewModel.categoryId = categoryId;
                lobbyViewModel.user = userClient.GetUserByUsername(SessionLogin.UserName);
                return View(lobbyViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateLobby(LobbyViewModel lobbyViewModel, int categoryId)
        {
            if (ModelState.IsValid)
            {
                lobbyViewModel.lobby.name = lobbyViewModel.name;
                lobbyViewModel.category.id = categoryId;
                lobbyViewModel.lobby.category = lobbyViewModel.category;
                lobbyViewModel.lobby.isStarted = false;
                lobbyViewModel.lobbyUser.id = userClient.GetUserByUsername(SessionLogin.UserName).id;
                lobbyClient.CreateLobby(lobbyViewModel.lobby, lobbyViewModel.lobbyUser);
                return RedirectToAction("Lobby", new { categoryId = categoryId });
            }
            return View(lobbyViewModel);
        }

        public ActionResult JoinLobby(int lobbyId, int categoryId)
        {
            if (SessionLogin.UserIsInSession())
            {
                LobbyViewModel lobbyViewModel = new LobbyViewModel();
                lobbyViewModel.PlayerName = SessionLogin.UserName;
                lobbyViewModel.quizCategory = categoryClient.GetCategory(categoryId);
                lobbyViewModel.lobby = lobbyClient.GetLobby(lobbyId);
                lobbyViewModel.users = userClient.GetUsersInLobby(lobbyId);
                lobbyViewModel.user = userClient.GetUserByUsername(SessionLogin.UserName);
                return View(lobbyViewModel);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult JoinLobby(LobbyViewModel lobbyViewModel, int lobbyId)
        {
            lobbyViewModel.lobbyUser.id = userClient.GetUserByUsername(SessionLogin.UserName).id;
            lobbyViewModel.lobby.id = lobbyId;
            lobbyViewModel.user.joinedLobbyId = lobbyId;
            lobbyClient.JoinLobby(lobbyViewModel.lobbyUser, lobbyViewModel.lobby);
            lobbyViewModel.lobby = lobbyClient.GetLobby(lobbyId);
            lobbyViewModel.users = userClient.GetUsersInLobby(lobbyId);
            return View(lobbyViewModel);
        }

        public ActionResult LeaveLobby(int userId, int categoryId)
        {
            LobbyViewModel lobbyViewModel = new LobbyViewModel();
            lobbyViewModel.user.id = userId;
            userClient.ClearUsersJoinedLobbyId(lobbyViewModel.user);
            return RedirectToAction("Lobby", new { categoryId = categoryId });
        }

        public ActionResult DeleteLobby(int userId, int lobbyOwnedId, int categoryId)
        {
            LobbyViewModel lobbyViewModel = new LobbyViewModel();
            lobbyViewModel.user.id = userId;
            lobbyViewModel.user.lobbyOwnedId = lobbyOwnedId;
            userClient.ClearsUsersLobbyOwnedIdAndDeleteLobby(lobbyViewModel.user);
            return RedirectToAction("Lobby", new { categoryId = categoryId });
        }

        [HttpPost]
        public void Start(int lobbyId, int categoryId)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LobbyHub>();
            var questions = quizClient.GetAllQuestionsAndAnswersByCategoryId(categoryId);
            hub.Clients.Group(lobbyId.ToString()).startGame(questions);
        }
    }
}


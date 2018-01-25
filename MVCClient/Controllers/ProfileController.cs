using MVCClient.LoginServiceReference;
using MVCClient.Models;
using MVCClient.Security;
using MVCClient.UserReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class ProfileController : Controller
    {
        private UserServiceClient userClient = new UserServiceClient();
        private LoginServiceClient loginClient = new LoginServiceClient();

        public ActionResult Index()
        {
            if (SessionLogin.UserIsInSession())
            {
                return RedirectToAction("MyProfile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel)
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
                        loginClient.DecryptPassword(loginViewModel.login);
                        SessionLogin.UserName = loginViewModel.Username;
                        return RedirectToAction("MyProfile");
                    }
                }
                catch (FaultException)
                {
                    ModelState.AddModelError("", "Sorry, we can't find you in our system, please try again or register");
                }
            }
            return View(loginViewModel);
        }

        public ActionResult MyProfile()
        {
            if (SessionLogin.UserIsInSession())
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.User = userClient.GetUserByUsername(SessionLogin.UserName);
                return View(profileViewModel);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (SessionLogin.UserIsInSession())
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.User = userClient.GetUser(id);
                return View(profileViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProfileViewModel profileViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                profileViewModel.User.name = profileViewModel.Name;
                profileViewModel.User.lastName = profileViewModel.LastName;
                profileViewModel.User.country = profileViewModel.Country;
                profileViewModel.User.phone = profileViewModel.Phone;
                try
                {
                    userClient.UpdateUser(profileViewModel.User);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data was changed by someone else, please refresh and try again !");
                }
                return RedirectToAction("MyProfile");
            }
            return View(profileViewModel);
        }

        public ActionResult LogOff()
        {
            SessionLogin.CloseSession();
            return RedirectToAction("Index", "Quiz");
        }
    }
}

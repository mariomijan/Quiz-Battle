using MVCClient.Models;
using MVCClient.Security;
using MVCClient.UserReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCClient.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindUser(string username)
        {
            UserServiceClient userClient = new UserServiceClient();
            var user = userClient.GetUserByUsername(username);
            if (userClient.GetUserByUsername(username) == null)
            {
                TempData["Message"] = "Nothing Found !";
            }
            return View(user);
        }
    }
}
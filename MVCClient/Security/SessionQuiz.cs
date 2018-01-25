using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCClient.Security
{ 
    public static class SessionQuiz
    {
        //Session, Set and Get first user
        public static string FirstUser
        {
            get
            {
                if (HttpContext.Current.Session["FirstUser"] != null)
                    return HttpContext.Current.Session["FirstUser"].ToString();
                else
                    return null;
            }

            set
            {
                HttpContext.Current.Session["FirstUser"] = value;
            }
        }

        //Session, Set and Get Second User
        public static string SecondUser
        {
            get
            {
                if (HttpContext.Current.Session["SecondUser"] != null)
                    return HttpContext.Current.Session["SecondUser"].ToString();
                else
                    return null;
            }

            set
            {
                HttpContext.Current.Session["SecondUser"] = value;
            }
        }

        //Check if users are in session
        public static bool UsersAreInSession()
        {
            if (!string.IsNullOrEmpty(FirstUser) && !string.IsNullOrEmpty(SecondUser))
            {
                return true;
            }
            return false;
        }

        //Close the session
        public static void CloseSession()
        {
            HttpContext.Current.Session.Clear();
            //HttpContext.Current.Session.Abandon();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCClient.Security
{ 
    public static class SessionLogin
    {
        //Session, Set and Get the userName
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["session"] != null)
                    return HttpContext.Current.Session["session"].ToString();
                else
                    return null;
            }

            set
            {
                HttpContext.Current.Session["session"] = value;
            }
        }

        //Check if user is in session
        public static bool UserIsInSession()
        {
            if (!string.IsNullOrEmpty(UserName))
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

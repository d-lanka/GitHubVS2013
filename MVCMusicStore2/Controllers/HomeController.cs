﻿using APITestWebApp.WebServiceCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVCMusicStore2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QSU()
        {
         
            return View();
        }
        public ActionResult RecordQSU()
        {
            string fName = Request["name"];
            string lName = Request["lastname"];
            string emailAddress = Request["email"];

            APICall apiTest = new APICall();
            apiTest.login();
            apiTest.mergeListMembers("DK_CONTACTS_LIST", "DK", emailAddress, fName, lName);
            apiTest.logout();
            apiTest.login();
            apiTest.triggerCustomEvent(emailAddress);
            apiTest.logout();
            return View();
        }
        public ActionResult Unsubscribe_View()
        {
            // ViewBag.Message = "Member Deleted";

            return View();

        }
        public ActionResult Unsubscribed_View()
        {
            ViewBag.Message = "Member Deleted";
            string fName = Request["name"];
            string lName = Request["lastname"];
            string emailAddress = Request["email"];

            APICall apiTest = new APICall();
            apiTest.login();
            apiTest.mergeListMembers("DK_CONTACTS_LIST", "DK", emailAddress, fName, lName,"O");
            apiTest.logout();

            return View();
        }

    }
}

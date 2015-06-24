using APITestWebApp.WebServiceCode;
using MVCMusicStore2.Models;
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
        MusicStoreEntities storeDB = new MusicStoreEntities();
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
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
            apiTest.mergeListMembers("DK_CONTACTS_LIST", "DK", emailAddress, fName, lName,"I");
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
        public List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore2.Models;
using APITestWebApp.WebServiceCode;

namespace MVCMusicStore2.Controllers
{
   // [Authorize]
    public class CheckoutController : Controller
    {
        //
        // GET: /Checkout/
        MusicStoreEntities storeDB = new MusicStoreEntities();
        const string PromoCode = "FREE";

        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            string emailAddress = Request["email"];
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId ,email = order.Email});
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id, string email)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);
            string points = id.ToString();
            APICall apiTest = new APICall();
            apiTest.login();
           // apiTest.triggerCustomEvent(email, points);
            apiTest.triggerCampaignMessage(email,points);
            apiTest.logout();

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }


    }
}

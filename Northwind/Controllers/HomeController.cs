using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Models;
using System.Web.Security;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                // Filter by date
                DateTime now = DateTime.Now;
                var Dis = (from d in db.Discounts.Where(s => s.StartTime <= now && s.EndTime > now)
                           orderby Guid.NewGuid()
                           select new DiscountDTO
                           {
                               Code = d.Code,
                               Title = d.Title,
                               Description = d.Description,
                               DiscountPercent = d.DiscountPercent,
                               EndTime = d.EndTime
                           }).Take(3).ToList();
                return View(Dis);
            }
        }
        // GET: Home/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(actionName: "SignedOut", controllerName: "Home");
        }
        // GET: Home/SignedOut
        public ActionResult SignedOut()
        {
            return View();
        }

        public ActionResult ReviewCart()
        {
                return RedirectToAction(actionName: "CheckOut", controllerName: "Home");
        }

        public ActionResult CheckOut()
        {
            return View();
        }

    }
}
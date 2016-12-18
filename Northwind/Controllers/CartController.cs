using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Models;
using System.Net;

namespace Northwind.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        //[HttpPost]
        //public List<String> DisplayCart(int? id)
        //{
        //    using (NorthwindEntities db = new NorthwindEntities())
        //    {

        //        return View(db.Carts.Where(c => c.CustomerID == id).ToList());
        //    }

        //}



        public ActionResult DisplayCart(Cart c1)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {

                var cart = new CartDTO();

                //cart.ProductID = c1.ProductID;
                //cart.CustomerID = c1.CustomerID.GetValueOrDefault();
                //cart.Quantity = c1.Quantity.GetValueOrDefault();

                return View(db.Carts.Where(c => c.CustomerID == cart.CustomerID));


            }
        }



        [HttpPost]
        public JsonResult AddToCart(CartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            Cart sc = new Models.Cart();
            sc.ProductID = cartDTO.ProductID;
            sc.CustomerID = cartDTO.CustomerID;
            sc.Quantity = cartDTO.Quantity;
           
            using (NorthwindEntities db = new NorthwindEntities())
            {

                // if there is a duplicate product id in cart, simply update the quantity
                if (db.Carts.Any(c => c.ProductID == sc.ProductID && c.CustomerID == sc.CustomerID))
                {
                    // this product is already in the customer's cart,
                    // update the existing cart item's quantity
                    Cart cart = db.Carts.FirstOrDefault(c => c.ProductID == sc.ProductID && c.CustomerID == sc.CustomerID);
                    cart.Quantity += sc.Quantity;
                    sc = new Cart()
                    //{
                    //    from c in db.Carts
                    //    join p in db.Products on c.ProductID equals p.ProductID
                    //    CartId = cart.CartId,
                    //    ProductID = cart.ProductID,
                    //    ProductName = cart.ProductName,
                    //    CustomerID = cart.CustomerID,
                    //    Quantity = cart.Quantity
                    //};
                }
                else
                {
                    // this product is not in the customer's cart, add the product
                    db.Carts.Add(sc);
                    
                }
                db.SaveChanges();
                //System.Threading.Thread.Sleep(1500);
            }
            return Json(sc, JsonRequestBehavior.AllowGet);
        }

    }
}

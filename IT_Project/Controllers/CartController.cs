using IT_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Project.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        public ActionResult AddToCart(int productId)
        {
            // Fetch the product from the database (assuming you have a DbContext)
            var product = dbContext.Products.Find(productId);

            if (product != null)
            {
                // Get or create the shopping cart (could be stored in session)
                var cart = Session["Cart"] as ShoppingCart ?? new ShoppingCart();

                // Add the product to the cart
                cart.AddProduct(product);

                // Save the cart back to session
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewCart()
        {
            // Retrieve the cart from session
            var cart = Session["Cart"] as ShoppingCart ?? new ShoppingCart();

            // Pass the cart to the view
            return View(cart.GetProducts());
        }
    }
}
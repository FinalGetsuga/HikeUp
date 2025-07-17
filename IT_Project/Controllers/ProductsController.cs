using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Project.Models;
using Microsoft.AspNet.Identity;

namespace IT_Project.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            int Pid = 1;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        public ActionResult Trousers()
        {
            int Pid = 2;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        public ActionResult Shirts()
        {
            int Pid = 3;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        public ActionResult Footwear()
        {
            int Pid = 4;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        public ActionResult Accessories()
        {
            int Pid = 5;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        public ActionResult CampingGear()
        {
            int Pid = 6;
            var products = db.Products.Where(p => p.CategoryId == Pid).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,ProductURL,Size,Color,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                if (product.CategoryId == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (product.CategoryId == 2)
                {
                    return RedirectToAction("Trousers");
                }
                else if (product.CategoryId == 3)
                {
                    return RedirectToAction("Shirts");
                }
                else if (product.CategoryId == 4)
                {
                    return RedirectToAction("Footwear");
                }
                else if (product.CategoryId == 5)
                {
                    return RedirectToAction("Accessories");
                }
                else if (product.CategoryId == 6)
                {
                    return RedirectToAction("CampingGear");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,ProductURL,Size,Color,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                if (product.CategoryId == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (product.CategoryId == 2)
                {
                    return RedirectToAction("Trousers");
                }
                else if (product.CategoryId == 3)
                {
                    return RedirectToAction("Shirts");
                }
                else if (product.CategoryId == 4)
                {
                    return RedirectToAction("Footwear");
                }
                else if (product.CategoryId == 5)
                {
                    return RedirectToAction("Accessories");
                }
                else if (product.CategoryId == 6)
                {
                    return RedirectToAction("CampingGear");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        [Authorize]
        public ActionResult AddToCart(int id)
        {

            var userId = User.Identity.GetUserId();
            var product = db.Products.Find(id);

            if (product != null)
            {
                var cart = db.ShoppingCarts
                    .Include(sc => sc.products)
                    .FirstOrDefault(sc => sc.UserId == userId);

                if (cart == null) 
                {
                    cart = new ShoppingCart { UserId = userId };
                    db.ShoppingCarts.Add(cart);
                }

                cart.AddProduct(product);
                db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        [Authorize]
        public ActionResult RemoveFromCart(int id)
        {

            var userId = User.Identity.GetUserId();
            var product = db.Products.Find(id);

            if (product != null)
            {
                var cart = db.ShoppingCarts
                    .Include(sc => sc.products)
                    .FirstOrDefault(sc => sc.UserId == userId);

                if (cart == null)
                {
                    cart = new ShoppingCart { UserId = userId };
                    db.ShoppingCarts.Add(cart);
                }

                cart.RemoveProduct(product);
                db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        [Authorize]
        public ActionResult ViewCart()
        {
            var userId = User.Identity.GetUserId();

            var cart = db.ShoppingCarts
                .Include (sc => sc.products)
                .FirstOrDefault(sc => sc.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
            }

            return View(cart.GetProducts());
        }

        [Authorize]
        public ActionResult PlaceOrder()
        {
            var userId = User.Identity.GetUserId();

            var cart = db.ShoppingCarts
                .Include(sc => sc.products)
                .FirstOrDefault(sc => sc.UserId == userId);

            if (cart == null)
            {
                TempData["ErrorMessage"] = "Your cart is already empty.";
                return RedirectToAction("ViewCart");
            }

            cart.ClearCart();
            db.SaveChanges();

            return View("ConfirmationPage");

        }

    }
}

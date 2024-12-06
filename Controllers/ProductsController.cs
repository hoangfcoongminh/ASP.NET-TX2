using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TX2.Models;

namespace TX2.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Products
        public ActionResult Index(string searchString)
        {
            var productsList = db.Products.Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                String x = Request.Form["searchType"];
                if(x == "price")
                {
                    int searchInt = int.Parse(searchString);
                    productsList = db.Products.Where(p => p.Price < searchInt);
                } else
                {
                    productsList = db.Products.Where(p => p.ProdName.Contains(searchString));
                }
            }
            ViewBag.findByPrice = db.Products.OrderByDescending(p => p.Price).Take(10).ToList();
            ViewBag.findByID = db.Products.OrderByDescending(p => p.Pid).Take(5).ToList();
            return View(productsList.ToList());
        }

        [Route("Shop/DanhMuc/{Categoryid?}")]
        public ActionResult ProductByCategory(int categoryID)
        {
            return View(db.Products.Where(p => p.Categoryid == categoryID).ToList());
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["uploadImg"];
                string fileName = System.IO.Path.GetFileName(f.FileName);
                string uploadPath = Server.MapPath("~/Content/Images/") + fileName;
                f.SaveAs(uploadPath);

                product.ImagePath = fileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error";
            return View(product);
        }

        // GET: Products/Edit/5
        [HttpGet]
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["uploadImg"];
                string fileName = System.IO.Path.GetFileName(f.FileName);
                string uploadPath = Server.MapPath("~/Content/Images/") + fileName;
                f.SaveAs(uploadPath);

                product.ImagePath = fileName;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
    }
}

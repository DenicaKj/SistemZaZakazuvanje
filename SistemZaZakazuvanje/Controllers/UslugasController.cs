using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemZaZakazuvanje.Models;

namespace SistemZaZakazuvanje.Controllers
{
    public class UslugasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uslugas
        public ActionResult Index()
        {
            var tipovi = new LinkedList<string>();
            foreach(var pom in db.Uslugi.ToList())
            {
                if (!tipovi.Contains(pom.Tip))
                {
                    tipovi.AddLast(pom.Tip);
                }
            }
            ViewBag.tipovi = tipovi;
            return View(db.Uslugi.ToList());
        }

        // GET: Uslugas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Uslugi.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // GET: Uslugas/Create
        public ActionResult Create()
        {
            var tipovi = new LinkedList<string>();
            foreach (var pom in db.Uslugi.ToList())
            {
                if (!tipovi.Contains(pom.Tip))
                {
                    tipovi.AddLast(pom.Tip);
                }
            }
            ViewBag.tipovi = tipovi;
            return View();
        }

        // POST: Uslugas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Duration,Price,Tip")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Uslugi.Add(usluga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usluga);
        }

        // GET: Uslugas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Uslugi.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Uslugas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Duration,Price,Tip")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usluga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usluga);
        }

        // GET: Uslugas/Delete/5
        
        public ActionResult Delete(int id)
        {
            Usluga usluga = db.Uslugi.Find(id);
            db.Uslugi.Remove(usluga);
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

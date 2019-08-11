using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaginaParroquia.Models;

namespace PaginaParroquia.Controllers
{
    public class PrimeraComunionsController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        // GET: PrimeraComunions
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string BuscarPor)
        {
            var primeraComunions = db.PrimeraComunions.Include(p => p.Bautismo).Include(p => p.Persona);
            return View(primeraComunions.ToList());
        }

        // GET: PrimeraComunions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimeraComunion primeraComunion = db.PrimeraComunions.Find(id);
            if (primeraComunion == null)
            {
                return HttpNotFound();
            }
            return View(primeraComunion);
        }

        // GET: PrimeraComunions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia");
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula");
            return View();
        }

        // POST: PrimeraComunions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPrimeraComunion,IDPersona,IDBautismo,Lugar_Comunion,Fecha,Libro,Folio,Asiento")] PrimeraComunion primeraComunion)
        {
            if (ModelState.IsValid)
            {
                db.PrimeraComunions.Add(primeraComunion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", primeraComunion.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", primeraComunion.IDPersona);
            return View(primeraComunion);
        }

        // GET: PrimeraComunions/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimeraComunion primeraComunion = db.PrimeraComunions.Find(id);
            if (primeraComunion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", primeraComunion.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", primeraComunion.IDPersona);
            return View(primeraComunion);
        }

        // POST: PrimeraComunions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPrimeraComunion,IDPersona,IDBautismo,Lugar_Comunion,Fecha,Libro,Folio,Asiento")] PrimeraComunion primeraComunion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primeraComunion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", primeraComunion.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", primeraComunion.IDPersona);
            return View(primeraComunion);
        }

        // GET: PrimeraComunions/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimeraComunion primeraComunion = db.PrimeraComunions.Find(id);
            if (primeraComunion == null)
            {
                return HttpNotFound();
            }
            return View(primeraComunion);
        }

        // POST: PrimeraComunions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimeraComunion primeraComunion = db.PrimeraComunions.Find(id);
            db.PrimeraComunions.Remove(primeraComunion);
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

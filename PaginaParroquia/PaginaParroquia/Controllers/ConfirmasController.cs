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
    public class ConfirmasController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        [Authorize]
        // GET: Confirmas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string BuscarPor)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.fechaSortParm = sortOrder == "fecha" ? "fecha_desc" : "fecha";
            var confirma = from c in db.Confirmas
                           select c;

            switch (sortOrder)
            {
                case "name_desc":
                    confirma = confirma.OrderByDescending(c => c.Persona);
                    break;
                case "fecha":
                    confirma = confirma.OrderBy(c => c.Fecha);
                    break;
                case "fecha_desc":
                    confirma = confirma.OrderByDescending(c => c.Fecha);
                    break;
                default:
                    confirma = confirma.OrderBy(c => c.Persona);
                    break;

            }


           var confirmas = db.Confirmas.Include(c => c.Bautismo).Include(c => c.Persona).Include(c => c.RelacionFamiliar);
            return View(confirmas.ToList());
        }
        [Authorize]
        // GET: Confirmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirma confirma = db.Confirmas.Find(id);
            if (confirma == null)
            {
                return HttpNotFound();
            }
            return View(confirma);
        }
        [Authorize]
        // GET: Confirmas/Create
        public ActionResult Create()
        {
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia");
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula");
            ViewBag.IDRelacion = new SelectList(db.RelacionFamiliars, "IDRelacion", "Relacion");
            return View();
        }

        // POST: Confirmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDConfirma,IDPersona,IDRelacion,IDBautismo,Padrino,Obispo,Lugar_Confirma,Fecha,Libro,Folio,Asiento")] Confirma confirma)
        {
            if (ModelState.IsValid)
            {
                db.Confirmas.Add(confirma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", confirma.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", confirma.IDPersona);
            ViewBag.IDRelacion = new SelectList(db.RelacionFamiliars, "IDRelacion", "Relacion", confirma.IDRelacion);
            return View(confirma);
        }
        [Authorize(Roles = "Admin")]
        // GET: Confirmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirma confirma = db.Confirmas.Find(id);
            if (confirma == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", confirma.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", confirma.IDPersona);
            ViewBag.IDRelacion = new SelectList(db.RelacionFamiliars, "IDRelacion", "Relacion", confirma.IDRelacion);
            return View(confirma);
        }

        // POST: Confirmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDConfirma,IDPersona,IDRelacion,IDBautismo,Padrino,Obispo,Lugar_Confirma,Fecha,Libro,Folio,Asiento")] Confirma confirma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confirma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBautismo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", confirma.IDBautismo);
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", confirma.IDPersona);
            ViewBag.IDRelacion = new SelectList(db.RelacionFamiliars, "IDRelacion", "Relacion", confirma.IDRelacion);
            return View(confirma);
        }
        [Authorize(Roles ="Admin")]
        // GET: Confirmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirma confirma = db.Confirmas.Find(id);
            if (confirma == null)
            {
                return HttpNotFound();
            }
            return View(confirma);
        }

        // POST: Confirmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Confirma confirma = db.Confirmas.Find(id);
            db.Confirmas.Remove(confirma);
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

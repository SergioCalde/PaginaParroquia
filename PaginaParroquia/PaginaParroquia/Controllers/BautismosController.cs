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
    public class BautismosController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        // GET: Bautismos
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string BuscarPor)
        {
            ViewBag.CurrentSort = sortOrder;

            var bautismoes = db.Bautismoes.Include(b => b.Persona);
            return View(bautismoes.ToList());
        }

        // GET: Bautismos/Details/
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bautismo bautismo = db.Bautismoes.Find(id);
            if (bautismo == null)
            {
                return HttpNotFound();
            }
            return View(bautismo);
        }

        // GET: Bautismos/Create
        [Authorize]
        public ActionResult Create()
        {

            Persona person = new Persona();


            //ViewData["IDPersona"] = new SelectList(db.Personas, "IDPersona", "Cedula" );
            ViewData["IDPersona"] = new SelectList((from p in db.Personas.ToList() select new {
                                                    IDPersona = p.IDPersona,
                                                    Dato = p.Cedula + " "+ p.Nombre}),
                                                    "IDPersona","Dato",null);



            return View();
        }

        // POST: Bautismos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBautismo,IDPersona,Parroquia,Fecha_Bautismo,Presbitero,Parroco,Barrio,Distrito,Canton,Provincia,Padrinos,Declarante,Ced_Declarante,Libro,Folio,Asiento")] Bautismo bautismo)
        {
            if (ModelState.IsValid)
            {
                db.Bautismoes.Add(bautismo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", bautismo.IDPersona);
            return View(bautismo);
        }

        // GET: Bautismos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bautismo bautismo = db.Bautismoes.Find(id);
            if (bautismo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", bautismo.IDPersona);
            return View(bautismo);
        }

        // POST: Bautismos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBautismo,IDPersona,Parroquia,Fecha_Bautismo,Presbitero,Parroco,Barrio,Distrito,Canton,Provincia,Padrinos,Declarante,Ced_Declarante,Libro,Folio,Asiento")] Bautismo bautismo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bautismo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPersona = new SelectList(db.Personas, "IDPersona", "Cedula", bautismo.IDPersona);
            return View(bautismo);
        }

        // GET: Bautismos/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bautismo bautismo = db.Bautismoes.Find(id);
            if (bautismo == null)
            {
                return HttpNotFound();
            }
            return View(bautismo);
        }

        // POST: Bautismos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bautismo bautismo = db.Bautismoes.Find(id);
            db.Bautismoes.Remove(bautismo);
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

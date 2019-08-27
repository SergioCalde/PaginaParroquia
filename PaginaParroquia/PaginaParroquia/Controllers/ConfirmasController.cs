using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaginaParroquia.Models;
using PagedList;

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
            ViewBag.fechaSortParm = String.IsNullOrEmpty(sortOrder) ? "fecha_desc" : "";
            ViewBag.libroSortParm = sortOrder == "libro" ? "libro_desc" : "libro";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var confirma = from c in db.Confirmas
                           select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (BuscarPor == "Fecha")
                {
                    DateTime fecha = DateTime.Parse(searchString);
                    confirma = confirma.Where(c => c.Fecha.Day.Equals(fecha.Day) && c.Fecha.Month.Equals(fecha.Month)
                   && c.Fecha.Year.Equals(fecha.Year));
                }
                else if (BuscarPor == "Libro")
                {
                    confirma = confirma.Where(b => b.Libro.Equals(searchString));
                }
            }


            switch (sortOrder)
            {
                case "fecha_desc":
                    confirma = confirma.OrderByDescending(c => c.Fecha);
                    break;
                case "libro":
                    confirma = confirma.OrderBy(c => c.Fecha);
                    break;
                case "libro_desc":
                    confirma = confirma.OrderByDescending(c => c.Fecha);
                    break;
                default:
                    confirma = confirma.OrderBy(c => c.Fecha);
                    break;

            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //var confirmas = db.Confirmas.Include(c => c.Bautismo).Include(c => c.Persona).Include(c => c.RelacionFamiliar);
            return View(confirma.ToPagedList(pageNumber, pageSize));
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

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
using System.Web.Services;

namespace PaginaParroquia.Controllers
{
    public class RelacionFamiliarsController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        // GET: RelacionFamiliars

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string BuscarPor)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.relación = String.IsNullOrEmpty(sortOrder) ? "relacion_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var relacionFamiliar = from r in db.RelacionFamiliars
                           select r;

            //var relacionFamiliars = db.RelacionFamiliars.Include(r => r.Persona).Include(r => r.Persona1);

            if (!String.IsNullOrEmpty(searchString))
            {
                relacionFamiliar = relacionFamiliar.Where(r => r.Relacion.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "relacion_desc":
                    relacionFamiliar = relacionFamiliar.OrderByDescending(c => c.Relacion);
                    break;
                default:
                    relacionFamiliar = relacionFamiliar.OrderBy(c => c.Relacion);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(relacionFamiliar.ToPagedList(pageNumber, pageSize));
        }

        // GET: RelacionFamiliars/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacionFamiliar relacionFamiliar = db.RelacionFamiliars.Find(id);
            if (relacionFamiliar == null)
            {
                return HttpNotFound();
            }
            return View(relacionFamiliar);
        }

        // GET: RelacionFamiliars/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDPersona1 = new SelectList(db.Personas, "IDPersona", "Cedula");
            ViewBag.IDPersona2 = new SelectList(db.Personas, "IDPersona", "Cedula");
            return View();
        }

        // POST: RelacionFamiliars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRelacion,IDPersona1,IDPersona2,Relacion")] RelacionFamiliar relacionFamiliar)
        {
            if (ModelState.IsValid)
            {
                db.RelacionFamiliars.Add(relacionFamiliar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPersona1 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona1);
            ViewBag.IDPersona2 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona2);
            return View(relacionFamiliar);
        }

        // GET: RelacionFamiliars/Edit/5
        [Authorize (Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacionFamiliar relacionFamiliar = db.RelacionFamiliars.Find(id);
            if (relacionFamiliar == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPersona1 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona1);
            ViewBag.IDPersona2 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona2);
            return View(relacionFamiliar);
        }

        // POST: RelacionFamiliars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRelacion,IDPersona1,IDPersona2,Relacion")] RelacionFamiliar relacionFamiliar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relacionFamiliar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPersona1 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona1);
            ViewBag.IDPersona2 = new SelectList(db.Personas, "IDPersona", "Cedula", relacionFamiliar.IDPersona2);
            return View(relacionFamiliar);
        }

        // GET: RelacionFamiliars/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacionFamiliar relacionFamiliar = db.RelacionFamiliars.Find(id);
            if (relacionFamiliar == null)
            {
                return HttpNotFound();
            }
            return View(relacionFamiliar);
        }

        // POST: RelacionFamiliars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RelacionFamiliar relacionFamiliar = db.RelacionFamiliars.Find(id);
            db.RelacionFamiliars.Remove(relacionFamiliar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // POST: 
        [HttpPost, ActionName("ConsultarRelaciones")]
        [WebMethod]
        [AllowAnonymous]
        public JsonResult ConsultarRelaciones(int id)
        {
            var relaciones = new List<RelacionFamiliar>();
            try
            {

               var rel = db.RelacionFamiliars.Where(s => s.IDPersona1 == id)
                              .Select(s => s).ToList();

                return Json(rel);

            }
            catch(Exception err)
            {
                return Json(err.Message);
            }
            //return Json(relaciones);
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

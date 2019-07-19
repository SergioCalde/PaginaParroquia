using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PaginaParroquia.Models;

namespace PaginaParroquia.Controllers
{
    public class PersonasController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        // GET: Personas
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string BuscarPor)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Apellido1SortParm = sortOrder == "Apellido" ? "apellido_desc": "Apellido" ;
            ViewBag.Apellido2SortParm = sortOrder == "Apellido2" ? "apellido2_desc" : "Apellido2";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.NacionalidadSortParm = sortOrder == "Nacionalidad" ? "nacionalidad_desc" : "Nacionalidad";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var persona = from p in db.Personas
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (BuscarPor == "Nombre")
                {
                    persona = persona.Where(p => p.Nombre.Contains(searchString));
                }
                else if (BuscarPor == "Apellido")
                {
                    persona = persona.Where(p => p.Apellido.Contains(searchString));
                }else if (BuscarPor == "Fecha_Nacimiento")
                {
                    DateTime fecha = DateTime.Parse(searchString);
                    persona = persona.Where(p => p.Fecha_Nacimiento.Day.Equals(fecha.Day) && p.Fecha_Nacimiento.Month.Equals(fecha.Month)
                    && p.Fecha_Nacimiento.Year.Equals(fecha.Year));
                }
            }

            switch(sortOrder)
            {
                case "name_desc":
                    persona = persona.OrderByDescending(p => p.Nombre);
                    break;
                case "Apellido":
                    persona = persona.OrderBy(p => p.Apellido);
                    break;
                case "apellido_desc":
                    persona = persona.OrderByDescending(p => p.Apellido);
                    break;
                case "Apellido2":
                    persona = persona.OrderBy(p => p.Apellido2);
                    break;
                case "apellido2_desc":
                    persona = persona.OrderByDescending(p => p.Apellido2);
                    break;
                case "Date":
                    persona = persona.OrderBy(p => p.Fecha_Nacimiento);
                    break;
                case "date_desc":
                    persona = persona.OrderByDescending(p => p.Fecha_Nacimiento);
                    break;
                case "Nacionalidad":
                    persona = persona.OrderBy(p => p.Nacionalidad);
                    break;
                case "nacionalidad_desc":
                    persona = persona.OrderByDescending(p => p.Nacionalidad);
                    break;
                default:
                    persona = persona.OrderBy(p => p.Nombre);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(persona.ToPagedList(pageNumber,pageSize));
        }

        // GET: Personas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPersona,Cedula,Nombre,Apellido,Apellido2,Fecha_Nacimiento,Lugar_Nacimiento,Nacionalidad,Estado_Civil,Lugar_Residencia,Profesion,Religion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPersona,Cedula,Nombre,Apellido,Apellido2,Fecha_Nacimiento,Lugar_Nacimiento,Nacionalidad,Estado_Civil,Lugar_Residencia,Profesion,Religion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
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

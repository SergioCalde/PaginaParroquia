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
using Microsoft.AspNet.Identity;

namespace PaginaParroquia.Controllers
{
    public class MatrimoniosController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        [Authorize]
        // GET: Matrimonios
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


            var matrimonio = from m in db.Matrimonios
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (BuscarPor == "Fecha")
                {
                    DateTime fecha = DateTime.Parse(searchString);
                    matrimonio = matrimonio.Where(m => m.Fecha.Day.Equals(fecha.Day) && m.Fecha.Month.Equals(fecha.Month)
                   && m.Fecha.Year.Equals(fecha.Year));
                }
                else if (BuscarPor == "Libro")
                {
                    matrimonio = matrimonio.Where(m => m.Libro.Equals(searchString));
                }
            }


            switch (sortOrder)
            {
                case "fecha_desc":
                    matrimonio = matrimonio.OrderByDescending(c => c.Fecha);
                    break;
                case "libro":
                    matrimonio = matrimonio.OrderBy(c => c.Fecha);
                    break;
                case "libro_desc":
                    matrimonio = matrimonio.OrderByDescending(c => c.Fecha);
                    break;
                default:
                    matrimonio = matrimonio.OrderBy(c => c.Fecha);
                    break;

            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(matrimonio.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        // GET: Matrimonios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matrimonio matrimonio = db.Matrimonios.Find(id);
            if (matrimonio == null)
            {
                return HttpNotFound();
            }
            return View(matrimonio);
        }

        // GET: Matrimonios/Create
        public ActionResult Create()
        {
            ViewBag.IDBautismoEsposa = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia");
            ViewBag.IDBautismoEsposo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia");
            ViewBag.IDConfirmaEsposa = new SelectList(db.Confirmas, "IDConfirma", "Padrino");
            ViewBag.IDConfirmaEsposo = new SelectList(db.Confirmas, "IDConfirma", "Padrino");
            ViewBag.IDEsposa = new SelectList(db.Personas, "IDPersona", "Cedula");
            ViewBag.IDEsposo = new SelectList(db.Personas, "IDPersona", "Cedula");
            return View();
        }

        // POST: Matrimonios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IDMatrimonio,IDEsposo,IDEsposa,Parroquia,Fecha,Presbitero,IDBautismoEsposo,IDBautismoEsposa,IDConfirmaEsposo,IDConfirmaEsposa,Testigo1,EstadoCivil_T1,Profesion_T1,Cedula_T1,Residencia_T1,Testigo2,EstadoCivil_T2,Profesion_T2,Cedula_T2,Residencia_T2,Conyuges,Libro,Folio,Asiento")] Matrimonio matrimonio)
        {
            if (ModelState.IsValid)
            {
                db.Matrimonios.Add(matrimonio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBautismoEsposa = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposa);
            ViewBag.IDBautismoEsposo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposo);
            ViewBag.IDConfirmaEsposa = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposa);
            ViewBag.IDConfirmaEsposo = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposo);
            ViewBag.IDEsposa = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposa);
            ViewBag.IDEsposo = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposo);
            return View(matrimonio);
        }

        // GET: Matrimonios/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matrimonio matrimonio = db.Matrimonios.Find(id);
            if (matrimonio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBautismoEsposa = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposa);
            ViewBag.IDBautismoEsposo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposo);
            ViewBag.IDConfirmaEsposa = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposa);
            ViewBag.IDConfirmaEsposo = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposo);
            ViewBag.IDEsposa = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposa);
            ViewBag.IDEsposo = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposo);
            return View(matrimonio);
        }

        // POST: Matrimonios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMatrimonio,IDEsposo,IDEsposa,Parroquia,Fecha,Presbitero,IDBautismoEsposo,IDBautismoEsposa,IDConfirmaEsposo,IDConfirmaEsposa,Testigo1,EstadoCivil_T1,Profesion_T1,Cedula_T1,Residencia_T1,Testigo2,EstadoCivil_T2,Profesion_T2,Cedula_T2,Residencia_T2,Conyuges,Libro,Folio,Asiento")] Matrimonio matrimonio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matrimonio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBautismoEsposa = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposa);
            ViewBag.IDBautismoEsposo = new SelectList(db.Bautismoes, "IDBautismo", "Parroquia", matrimonio.IDBautismoEsposo);
            ViewBag.IDConfirmaEsposa = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposa);
            ViewBag.IDConfirmaEsposo = new SelectList(db.Confirmas, "IDConfirma", "Padrino", matrimonio.IDConfirmaEsposo);
            ViewBag.IDEsposa = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposa);
            ViewBag.IDEsposo = new SelectList(db.Personas, "IDPersona", "Cedula", matrimonio.IDEsposo);
            return View(matrimonio);
        }

        // GET: Matrimonios/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matrimonio matrimonio = db.Matrimonios.Find(id);
            if (matrimonio == null)
            {
                return HttpNotFound();
            }
            return View(matrimonio);
        }

        // POST: Matrimonios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matrimonio matrimonio = db.Matrimonios.Find(id);
            db.Matrimonios.Remove(matrimonio);
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

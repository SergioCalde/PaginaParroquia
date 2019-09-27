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
    public class MatrimoniosController : Controller
    {
        private SacramentosModel db = new SacramentosModel();

        // GET: Matrimonios
        public ActionResult Index()
        {
            var matrimonios = db.Matrimonios.Include(m => m.Bautismo).Include(m => m.Bautismo1).Include(m => m.Confirma).Include(m => m.Confirma1).Include(m => m.Persona).Include(m => m.Persona1);
            return View(matrimonios.ToList());
        }

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

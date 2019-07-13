using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PaginaParroquia.Models;
using PagedList;

namespace PaginaParroquia.Controllers
{
    public class UsuariosController : Controller
    {
        private Model1 db = new Model1();

        // GET: Usuarios
        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "usuarioDesc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var usuarios = from u in db.Usuarios
                            select u;

            if (!String.IsNullOrEmpty(searchString)) {
                usuarios = usuarios.Where(u => u.usuario.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "usuarioDesc":
                    usuarios = usuarios.OrderByDescending(s => s.usuario);
                    break;
                default:
                    usuarios = usuarios.OrderBy(s => s.usuario);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuario,password")] Usuario usuario2)
        {
            try
            {

                LoginController lc = new LoginController();

                if (ModelState.IsValid)
                {

                    usuario2.password = lc.Encrypt(usuario2.password);
                    usuario2.rol = 2;
                    db.Usuarios.Add(usuario2);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {

            }
            return View(usuario2);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario, usuario, password, rol")] Usuario usuario2)
        {
            LoginController lc = new LoginController();

            if (ModelState.IsValid)
            {

                usuario2.password = lc.Encrypt(usuario2.password);
                db.Entry(usuario2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario2);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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

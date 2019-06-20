using PaginaParroquia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PaginaParroquia.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autorizar(Usuario objUsuario)
        {
            //Lectura de la DBS
            using (UsuarioModel DBS = new UsuarioModel())
            {

                var pass = Encrypt(objUsuario.password);
                var SQL = DBS.Usuarios.Where(x => x.usuario == objUsuario.usuario &&
                                         x.password == pass).FirstOrDefault();

                //VALIDO REGISTROS

                if (SQL == null)
                {
                    objUsuario.PpMensaje = "Datos incorrectos, Por favor revisar!";
                    return View("Index", objUsuario);
                }
                // SI ES VALIDO EL USUARIO AUTORIZO ACCESSO A VIP
                else
                {
                    Session["UsuarioID"] = SQL.idUsuario;
                    Session["usuario"] = SQL.usuario;
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        public ActionResult LogOutUser()
        {
            //FINALIZAMOS SESIÓN...

            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


        protected static string Encrypt(string Password) {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
                
        }

    }
}
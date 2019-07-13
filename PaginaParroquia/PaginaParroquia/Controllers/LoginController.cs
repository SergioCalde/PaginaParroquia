using PaginaParroquia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        
        public ActionResult Autorizar (Usuario objUsuario)
        {
            //SCalderon: Lectura de la DBS
            using (Model1 DBS = new Model1())
            {
                //SCalderon: CREA UNA VARIABLE Y ENCRIPTA LO QUE EL CLIENTE NOS ENVIA PARA PODER VALIDARLO CON DB
                var pass = Encrypt(objUsuario.password);
                //VALIDO REGISTROS
                var SQL = DBS.Usuarios.Where(x => x.usuario == objUsuario.usuario &&
                                         x.password == pass).FirstOrDefault();

                if (SQL == null)
                {
                    objUsuario.PpMensaje = "Datos incorrectos, Por favor revisar!";
                    return View("Index", objUsuario);
                }
                //SCalderon: SI ES VALIDO EL USUARIO AUTORIZO ACCESSO A LA PLATAFORMA
                else
                {
                    Session["idUsuario"] = SQL.idUsuario;
                    Session["usuario"] = SQL.usuario;
                    FormsAuthentication.SetAuthCookie(SQL.usuario.Trim(), false);
                    //Roles.AddUsersToRole(SQL.usuario.Trim, "Admin");
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOutUser()
        {
            //SCalderon: FINALIZAMOS SESIÓN...

            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


        public string Encrypt(string Password) {
            //SCalderon: Se crea un SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                try
                {
                    //SCalderon: Toma el string password que recibe como parametro y crea un array de tipo byte
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

                    //SCalderon: Se transforma ese byte array a string
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    return builder.ToString();
                }
                catch (Exception)
                {
                    return "Falta la contraseña";
                }
                
            }
                
        }

    }
}
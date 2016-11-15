using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Clases;

namespace TuLista.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DoLogin(string email, string pass)
        {
            BBDDUsuarios db = new BBDDUsuarios();
            //la funcion login de la clase base de datos devuelve el id del usuario como entero
            int idUsuario = db.login(email, pass);
            //si el id del usuario es distinto de 0, es decir, el usuario existe
            if (idUsuario != 0)
            {
                //guardamos su id en una variable de sesión
                Session["idUsuario"] = idUsuario;
                //redireccionamos a la pagina principal
                return Redirect("/Home/Index");
            }
            //si nos ha devuelto un 0
            else
            {
                //volvemos al login para intentarlo de nuevo
                return Redirect("/Login/Index");
            }
        }
        //si el usuario no esta registrado le damos la posibilidad de que se registre y le redireccionamos a su controlador
        public ActionResult Registro()
        {
            return Redirect("/Registro/Index");
        }
    }
}
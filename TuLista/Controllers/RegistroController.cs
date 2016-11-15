using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Clases;

namespace TuLista.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }
        //funcion para realizar el registro de un usuario, recogemos los datos que nos vienen por post del formulario
        public ActionResult Registro(string email, string pass)
        {
            BBDDUsuarios db = new BBDDUsuarios();
            //se lo pasamos a la funcion de la clase base de datos que realiza el registro
            db.registro(email, pass);
            //redirigimos al login para que acceda a la apliación
            return Redirect("/Login/Index");
        }
    }
}
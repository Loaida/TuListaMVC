using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Clases;
using TuLista.Models;

namespace TuLista.Controllers
{
    public class HomeController : Controller
    {
        BBDDListas db = new BBDDListas();
        public ActionResult Index()
        {
            if(Session["idUsuario"]== null)
            {
                return Redirect("/Login/Index");
            }
            return View();
        }
        public ActionResult verListaActiva()
        {
            int idUSuario = int.Parse(Session["idUsuario"].ToString());
            List<CompraActiva> listCA = db.verCompraActiva(idUSuario);
            return PartialView(listCA);
        }
        
    }
}
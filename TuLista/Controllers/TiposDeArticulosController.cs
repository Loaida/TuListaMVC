using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Models;
using TuLista.Clases;

namespace TuLista.Controllers
{
    public class TiposDeArticulosController : Controller
    {
        BBDDTiposDeArticulos db = new BBDDTiposDeArticulos();
        // GET: TiposDeArticulos
        public ActionResult Index()
        {
            if (Session["idUsuario"] == null)
            {
                return Redirect("/Login/Index");
            }
            List<tabTipoArticulos> listTA = db.TiposDeArticulosList();
            return View(listTA);
        }
        public ActionResult NuevoTipo()
        {
            return View();
        }
        public ActionResult GuardarNuevoTipo(string nombre)
        {
            db.guardarNuevoTipoDeArticulo(nombre);
            return Redirect("/TiposDeArticulos/Index");
        }
    }
}
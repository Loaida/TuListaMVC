using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Models;
using TuLista.Clases;

namespace TuLista.Controllers
{
    public class ArticulosController : Controller
    {
        BBDDArticulos db = new BBDDArticulos();
        BBDDTiposDeArticulos db2 = new BBDDTiposDeArticulos();
        // GET: Articulos 
        public ActionResult Index()
        {
            if (Session["idUsuario"] == null)
            {
                return Redirect("/Login/Index");
            }
            List<ArticulosConTipo> listA = db.GetAllArticulos();
            return View(listA);
        }
        public ActionResult NuevoArticulo()
        {
            return View();
        }
        public ActionResult ListaTiposSelect()
        {
            List<tabTipoArticulos> listTA = db2.TiposDeArticulosList();
            return PartialView(listTA);
        }
        public ActionResult GuardarNuevoArticulo(string nombre, int tipo)
        {
            db.GuardarArticulo(nombre,tipo);
            return Redirect("/Articulos/Index");
        }
    }
}
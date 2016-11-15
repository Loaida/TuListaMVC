using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuLista.Models;
using TuLista.Clases;

namespace TuLista.Controllers
{
    public class ListasController : Controller
    {
        BBDDListas db = new BBDDListas();
        BBDDUsuarios db2 = new BBDDUsuarios();
        BBDDArticulos db3 = new BBDDArticulos();
        BBDDTiposDeArticulos db4 = new BBDDTiposDeArticulos();
        // GET: Listas

        //*********************REGION DE INICIO******************************
        #region inicio
        public ActionResult Index()
        {
            if (Session["idUsuario"] == null)
            {
                return Redirect("/Login/Index");
            }
            return View();
        }
        public ActionResult verListasActivas()
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            List<CompraActiva> listCA = db.verCompraActiva(idUsuario);
            return PartialView(listCA);
        }
        #endregion

        //*********************REGION DE NUEVA LISTA*************************
        #region Nueva Lista
        public ActionResult CrearNuevaLista()
        {
            return View();
        }
        //función para mostrar los usuarios existentes en el sistema.
        public ActionResult VerUsuarios()
        {
            List<Usuarios> listU = db2.GetAllUsuarios();
            return PartialView(listU);
        }
        //función para guardar el titulo y los usuarios existentes en el sistema, nos devolverá el id de la compra
        public ActionResult GuardarTituloYUsuarios(string titulo, int[] usuario)
        {
            int idCompra = db.guardarUsuariosYTituloNuevaLista(titulo, usuario);
            Session["idCompra"] = idCompra;
            return Redirect("/Listas/ContinuarLista");
        }
        public ActionResult ContinuarLista()
        {
            return View();
        }
        public ActionResult VerArticulosMasUsados()
        {
            List<ArticulosConTipo> listA = db3.getArticulosMasUsados();
            return PartialView(listA);
        }
        public ActionResult VerUltimosArticulosAnadidos()
        {
            List<ArticulosConTipo> listA = db3.getUltimosAnadidos();
            return PartialView(listA);
        }
        public ActionResult GuardarYContinuar(int?[] articulo)
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            if(articulo != null)
            {
                db.guardarArticulosEnLista(articulo, idCompra);
            }
            return Redirect("/Listas/ContinuarComprando");
        }
        public ActionResult ContinuarComprando()
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            List<ArticulosConTipo> listT = db3.GetTodoSinAnadidosYa(idCompra);
            return View(listT);
        }
        public ActionResult FinalizarLista()
        {
            //Session["idCompra"] = null;
            return View();
        }
        public ActionResult VerTituloDeLista()
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            string titulo = db.getTitulo(idCompra);
            ViewBag.titulo = titulo;
            return PartialView();
        }
        public ActionResult verUsuariosDeLista()
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            List<Usuarios> listU = db.UsuariosDeLista(idCompra);
            return PartialView(listU);
        }
        public ActionResult VerArticulosDeLista()
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            List<ArticulosConTipo> listA = db.GetAllArticulosInLista(idCompra);
            return PartialView(listA);
        }
        #endregion

        //*********************REGIÓN DE DETALLES DE LISTA*******************
        #region Detalles de lista
        public ActionResult verDetallesDeLista()
        {
            int idLista = int.Parse(Request.QueryString["idLista"]);
            Session["idCompra"] = idLista;
            //llamar los 3 metodos que muestran el resumen al finalizar una lista nueva
            return View();
        }

        #endregion

        //*********************REGIÓN DE FINALIZAR LISTA*********************
        #region Dar lista por finalizada
        public ActionResult darPorFinalizadaLaLista()
        {
            int idLista = int.Parse(Request.QueryString["idLista"]);
            Session["idCompra"] = idLista;
            //llamar los 3 metodos que muestran el resumen al finalizar una lista nueva
            return View();
        }
        public ActionResult finalizar()
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            db.finalizarLista(idCompra);
            return Redirect("/Listas/Index");
        }
        #endregion

        //*********************REGIÓN DE EDITAR LISTA************************
        #region Editar lista
        public ActionResult editarLista()
        {
            int idLista = int.Parse(Session["idCompra"].ToString());
            List<ArticulosConTipo> listT = db3.GetTodoSinAnadidosYa(idLista);
            return View(listT);
        }
        public ActionResult guardarMasArticulos(int?[] articulo)
        {
            int idCompra = int.Parse(Session["idCompra"].ToString());
            if (articulo != null)
            {
                db.guardarArticulosEnLista(articulo, idCompra);
            }
            return Redirect("/Listas/verDetallesDeLista?idLista="+idCompra);
        }
        public ActionResult MarcarArticuloComoComprado()
        {
            int idLista = int.Parse(Session["idCompra"].ToString());
            int idArticulo = int.Parse(Request.QueryString["idArticulo"]);
            db.marcarArticuloComprado(idArticulo);
            return Redirect("/Listas/verDetallesDeLista?idLista=" + idLista);
        }
        #endregion
    }
}

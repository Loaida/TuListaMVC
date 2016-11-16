using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuLista.Models;

namespace TuLista.Clases
{
    public class BBDDArticulos
    {
        tulistaEntities db = new tulistaEntities();
        public List<ArticulosConTipo> GetAllArticulos()
        {
            List<ArticulosConTipo> listA = new List<ArticulosConTipo>();
            var articulos = from a in db.tabArticulo orderby a.IdTipo select a;
            foreach (var a in articulos)
            {
                ArticulosConTipo at = new ArticulosConTipo();
                at.IdArticulo = a.Id;
                at.nombreArticulo = a.Articulo;
                at.nombreTipo = (from t in db.tabTipoArticulos where t.Id == a.IdTipo select t.Tipo).First();
                listA.Add(at);
            }
            return listA;
        }
        public void GuardarArticulo(string nombre, int tipo)
        {
            tabArticulo ta = new tabArticulo();
            ta.Articulo = nombre;
            ta.IdTipo = tipo;
            ta.usos = 0;
            db.tabArticulo.Add(ta);
            db.SaveChanges();
        }
        public List<ArticulosConTipo> getArticulosMasUsados()
        {
            List<ArticulosConTipo> listA = new List<ArticulosConTipo>();
            var articulosMasUsados = (from a in db.tabArticulo orderby a.usos descending select a).Take(5);
            foreach (var a in articulosMasUsados)
            {
                ArticulosConTipo at = new ArticulosConTipo();
                at.IdArticulo = a.Id;
                at.nombreArticulo = a.Articulo;
                at.nombreTipo = (from t in db.tabTipoArticulos where t.Id == a.IdTipo select t.Tipo).First();
                listA.Add(at);
            }
            return listA;
        }
        public List<ArticulosConTipo> getUltimosAnadidos()
        {
            List<ArticulosConTipo> listA = new List<ArticulosConTipo>();
            var articulosMasUsados = (from a in db.tabArticulo
                                      where !((from ar in db.tabArticulo
                                               orderby ar.usos descending
                                               select ar.Id).Take(5)).Contains(a.Id)
                                      orderby a.Id descending
                                      select a).Take(10);
            foreach (var a in articulosMasUsados)
            {
                ArticulosConTipo at = new ArticulosConTipo();
                at.IdArticulo = a.Id;
                at.nombreArticulo = a.Articulo;
                at.nombreTipo = (from t in db.tabTipoArticulos where t.Id == a.IdTipo select t.Tipo).First();
                listA.Add(at);
            }
            return listA;
        }
        public List<ArticulosConTipo> GetTodoSinAnadidosYa(int idCompra)
        {
            List<ArticulosConTipo> listA = new List<ArticulosConTipo>();
            var articulosSinEstar = (from a in db.tabArticulo
                                   where !(from ar in db.tabCompraArticulos
                                           where ar.idCompra == idCompra
                                           select ar.idAritulo).Contains(a.Id)
                                   select a);
            
            foreach (var ainl in articulosSinEstar)
            {
                ArticulosConTipo at = new ArticulosConTipo();
                at.IdArticulo = ainl.Id;
                at.nombreArticulo = ainl.Articulo;
                at.nombreTipo = (from t in db.tabTipoArticulos
                                 where t.Id == ainl.IdTipo
                                 select t.Tipo).First();
                listA.Add(at);
            }
            return listA;
        }
    }
    
}
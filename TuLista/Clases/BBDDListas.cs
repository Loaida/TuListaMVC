using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuLista.Models;

namespace TuLista.Clases
{
    public class BBDDListas
    {
        tulistaEntities db = new tulistaEntities();
        //función para ver la lista de la compra activa
        public List<CompraActiva> verCompraActiva(int idUsuario)
        {
            List<CompraActiva> ListC = new List<CompraActiva>();
            CompraActiva ca;
            var comprasActivas = from c in db.tabCompra
                                 join u in db.tabUsuarioCompra
                                 on c.Id equals u.IdCompra
                                 where u.IdUsuario == idUsuario
                                 && c.Estado == true
                                 select c;
            foreach (var c in comprasActivas)
            {
                ca = new CompraActiva();
                ca.idLista = c.Id;
                ca.titulo = c.Titulo;
                ListC.Add(ca);
            }
            return ListC;
        }
        //función para ver los usuarios asociados a una lista
        public List<Usuarios> UsuariosDeLista(int idLista)
        {
            List<Usuarios> ListU = new List<Usuarios>();
            Usuarios usuarios;
            var usuario = from u in db.tabUsuarios
                          join uc in db.tabUsuarioCompra
                          on u.Id equals uc.IdUsuario
                          where uc.IdCompra == idLista
                          select u;
            foreach (var u in usuario)
            {
                usuarios = new Usuarios();
                usuarios.idUsuario = u.Id;
                usuarios.email = u.Email;
                ListU.Add(usuarios);
            }
            return ListU;
        }
        //función para guardar los usuarios y el titulo de la nueva lista
        public int guardarUsuariosYTituloNuevaLista(string titulo, int[] usuarios)
        {
            tabCompra tc = new tabCompra();
            tc.Titulo = titulo;
            tc.Estado = true;
            db.tabCompra.Add(tc);
            db.SaveChanges();
            int idCompra = (from c in db.tabCompra where c.Titulo == titulo select c.Id).First();
            for (int i = 0; i < usuarios.Length; i++)
            {
                tabUsuarioCompra tuc = new tabUsuarioCompra();
                tuc.IdCompra = idCompra;
                tuc.IdUsuario = usuarios[i];
                db.tabUsuarioCompra.Add(tuc);
            }
            db.SaveChanges();
            return idCompra;
        }
        public void guardarArticulosEnLista(int?[] articulo, int idLista)
        {
            for (int i = 0; i < articulo.Length; i++)
            {
                tabCompraArticulos tac = new tabCompraArticulos();
                tac.idAritulo = articulo[i];
                tac.idCompra = idLista;
                tac.Cantidad = "-";
                db.tabCompraArticulos.Add(tac);
            }
            db.SaveChanges();
        }
        public string getTitulo(int idCompra)
        {
            string titulo = (from c in db.tabCompra where c.Id == idCompra select c.Titulo).First();
            return titulo;
        }
        public List<ArticulosConTipo> GetAllArticulosInLista(int idCompra)
        {
            List<ArticulosConTipo> listA = new List<ArticulosConTipo>();
            var articulos = (from a in db.tabArticulo
                             join c in db.tabCompraArticulos
                             on a.Id equals c.idAritulo
                             where c.idCompra == idCompra
                             orderby a.IdTipo ascending
                             select a);
            foreach(var a in articulos)
            {
                ArticulosConTipo at = new ArticulosConTipo();
                at.IdArticulo = a.Id;
                at.nombreArticulo = a.Articulo;
                at.nombreTipo = (from t in db.tabTipoArticulos where t.Id == a.IdTipo select t.Tipo).First();
                listA.Add(at);
            }
            return listA;
        }
        //función para cambiar el estado de una lista
        public void finalizarLista(int idLista)
        {
            var lista = from l in db.tabCompra where l.Id == idLista select l;
            foreach(tabCompra l in lista)
            {
                l.Estado = false;
            }
            db.SaveChanges();
        }
    }
}
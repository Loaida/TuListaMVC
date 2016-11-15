using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuLista.Models;

namespace TuLista.Clases
{
    public class BBDDTiposDeArticulos
    {
        tulistaEntities db = new tulistaEntities();
        //función para listar los tipos de articulos que existen
        public List<tabTipoArticulos> TiposDeArticulosList()
        {
            List<tabTipoArticulos> listTA = new List<tabTipoArticulos>();
            var tipos = from t in db.tabTipoArticulos select t;
            foreach (var t in tipos)
            {
                listTA.Add(t);
            }
            return listTA;
        }
        //función para guardar un nuevo tipo de articulos en la base de datos
        public void guardarNuevoTipoDeArticulo(string nombre)
        {
            tabTipoArticulos tta = new tabTipoArticulos();
            tta.Tipo = nombre;
            db.tabTipoArticulos.Add(tta);
            db.SaveChanges();
        }
    }
}
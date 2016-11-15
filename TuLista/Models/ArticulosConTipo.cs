using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuLista.Models
{
    public class ArticulosConTipo
    {
        public int? IdArticulo { get; set; }
        public string nombreArticulo { get; set; }
        public string nombreTipo { get; set; }
        public bool comprado { get; set; }
    }
}
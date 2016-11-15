using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuLista.Models;

namespace TuLista.Clases
{
    
    public class BBDDUsuarios
    {
        tulistaEntities db = new tulistaEntities();
        //función para iniciar sesión con un usuario
        public int login(string email, string pass)
        {
            bool correcto = false;
            int idUsuario = 0;
            correcto = db.tabUsuarios.Count(c => c.Email == email && c.Pass == pass) > 0;
            if (correcto)
            {
                idUsuario = (from u in db.tabUsuarios where (u.Email == email && u.Pass == pass) select u.Id).First();
            }
            return idUsuario;
        }
        //función para dar de alta un usuario
        public void registro(string email, string pass)
        {
            tabUsuarios tu = new tabUsuarios();
            tu.Email = email;
            tu.Pass = pass;
            db.tabUsuarios.Add(tu);
            db.SaveChanges();
        }
        public List<Usuarios> GetAllUsuarios()
        {
            List<Usuarios> listU = new List<Usuarios>();
            var usuarios = from u in db.tabUsuarios select u;
            foreach(var u in usuarios)
            {
                Usuarios usu = new Usuarios();
                usu.idUsuario = u.Id;
                usu.email = u.Email;
                listU.Add(usu);
            }
            return listU;
        }
    }
}
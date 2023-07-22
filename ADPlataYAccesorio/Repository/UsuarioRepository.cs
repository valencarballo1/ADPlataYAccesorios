using ADPlataYAccesorio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ADPlataYAccesorio.Repository
{
    public class UsuarioRepository
    {
        public Usuario GetByEmailAndPass(string email, string pass)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                Usuario usuario = db.Usuario.Where(u => u.Email == email && u.Contraseña == pass).FirstOrDefault();
                return usuario;
            }
        }

        public Usuario GetByEmail(string email)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                Usuario usuario = db.Usuario.Where(u => u.Email == email).FirstOrDefault();
                return usuario;
            }
        }

        public void SaveUsuario(Usuario registrarUsuario)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                Usuario usu = this.GetByEmail(registrarUsuario.Email);
                if (usu == null)
                {
                    db.Usuario.AddOrUpdate(registrarUsuario);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("El usuario ya existe."); // Mensaje de error personalizado
                }
            }
        }

    }
}
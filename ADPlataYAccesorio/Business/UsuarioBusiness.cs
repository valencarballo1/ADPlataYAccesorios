using ADPlataYAccesorio.Models;
using ADPlataYAccesorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADPlataYAccesorio.Business
{
    public class UsuarioBusiness
    {
        private UsuarioRepository _usuarioRepository;

        public UsuarioBusiness()
        {
            this._usuarioRepository = new UsuarioRepository();
        }

        public Usuario GetByEmailAndPass(string email, string pass)
        {
            return _usuarioRepository.GetByEmailAndPass(email, pass);
        }

        public void CrearUsuario(Usuario registrarUsuario)
        {
            try
            {
                _usuarioRepository.SaveUsuario(registrarUsuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Usuario GetByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);

        }
    }
}
using ADPlataYAccesorio.Business;
using ADPlataYAccesorio.Models;
using ADPlataYAccesorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ADPlataYAccesorio.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;

        public UsuarioController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
        }

        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InicioSesion(string email, string pass, bool? recordarme)
        {
            if (recordarme == null)
            {
                recordarme = false;
            }

            Usuario usuario = _UsuarioBusiness.GetByEmailAndPass(email, pass);
            if (usuario != null)
            {
                // Crear una cookie de autenticación para el usuario
                var cookie = new HttpCookie("Usuario", email);
                if (recordarme.Value)
                {
                    // Establecer la fecha de expiración de la cookie en el futuro
                    cookie.Expires = DateTime.Now.AddDays(7);
                }
                // Agregar la cookie al contexto de respuesta
                Response.Cookies.Add(cookie);

                return Json(new { success = true, message = "Inicio de sesión exitoso" });
            }
            else
            {
                // Credenciales inválidas, devolver mensaje de error
                return Json(new { success = false, message = "Credenciales de inicio de sesión inválidas" });
            }
        }

        [HttpGet]
        public JsonResult CerrarSesion()
        {
            // Verificar si la cookie existe
            if (Request.Cookies["Usuario"] != null)
            {
                // Crear una nueva cookie con el mismo nombre y configuración de expiración en el pasado
                var cookie = new HttpCookie("Usuario")
                {
                    Expires = DateTime.Now.AddDays(-1) // Establecer la expiración en el pasado
                };

                // Reemplazar la cookie existente con la nueva cookie expirada
                Response.Cookies.Add(cookie);
            }

            // Otros pasos para cerrar la sesión si es necesario

            // Retornar una respuesta JSON u otra indicación de que se ha cerrado la sesión
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registrarme()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Registrarme(Usuario registrarUsuario)
        {
            try
            {
                _UsuarioBusiness.CrearUsuario(registrarUsuario);
                return Json(new { success = true, message = "Registro exitoso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult VerPerfil()
        {
            // Obtener el correo electrónico del usuario desde la cookie
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);

            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                return RedirectToAction("InicioSesion", "Usuario");
            }
        }
    }
}
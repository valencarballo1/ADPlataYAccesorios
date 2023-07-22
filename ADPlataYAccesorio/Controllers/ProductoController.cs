using ADPlataYAccesorio.Business;
using ADPlataYAccesorio.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ADPlataYAccesorio.Repository.CarritoRepository;
using static ADPlataYAccesorio.Repository.ProductoRepository;

namespace ADPlataYAccesorio.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoBusiness _ProductoBusiness;
        private CarritoBusiness _CarritoBusiness;
        private UsuarioBusiness _UsuarioBusiness;

        public ProductoController()
        {
            this._ProductoBusiness = new ProductoBusiness();
            this._CarritoBusiness = new CarritoBusiness();
            this._UsuarioBusiness = new UsuarioBusiness();
        }

        public ActionResult VerTodos()
        {
            return View();
        }

        public JsonResult VerTodosLosProductos()
        {
            List<ProductoView> productos = _ProductoBusiness.GetAll();
            return Json(productos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerCarrito()
        {
            return View();
        }

        public JsonResult Carrito()
        {

            // Obtener el correo electrónico del usuario desde la cookie
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);

            if (usuario != null)
            {
                List<CarritoView> verTodos = _CarritoBusiness.VerTodos(usuario.ID);
                return Json(verTodos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // El usuario no está autenticado, devolver un error o redireccionar a la página de inicio de sesión
                return Json(new { success = false, message = "Usuario no autenticado" });
            }
        }

        public ActionResult Agregar()
        {
            var categorias = _ProductoBusiness.GetAllCategorias();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View();
        }

        public JsonResult AgregarOModificar(Producto producto)
        {
            // Obtener el correo electrónico del usuario desde la cookie
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);

            if (usuario.ID == 1)
            {
                _ProductoBusiness.AgregarOModificar(producto);
                return Json("OK");
            }
            else
            {
                return Json("Error");

            }
        }

        public ActionResult AgregarCategoria()
        {
            return View();
        }

        public JsonResult AddCagetoria(Categoria categoria)
        {
            // Obtener el correo electrónico del usuario desde la cookie
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);
            if (usuario.ID == 1)
            {
                _ProductoBusiness.AgregarOModificarCategoria(categoria);
                return Json("Ok");
            }
            else
            {
                return Json("Error");
            }
        }

        public JsonResult AgregarProductoAlCarrito(Producto producto)
        {
            // Obtener el correo electrónico del usuario desde la cookie
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);

            if (usuario != null)
            {
                Carrito productoEnCarrito = _CarritoBusiness.GetProductoById(producto.Id, usuario.ID);
                if (productoEnCarrito != null)
                {
                    // Si el producto ya existe en el carrito, incrementa la cantidad en 1
                    productoEnCarrito.Cantidad++;
                    _CarritoBusiness.Save(productoEnCarrito);
                }
                else
                {
                    // Si el producto no existe en el carrito, agrega un nuevo item con cantidad 1
                    _CarritoBusiness.AgregarProductoAlCarrito(producto, usuario.ID);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Si el usuario no ha iniciado sesión, envía un mensaje de error
                return Json(new { success = false, message = "Debes iniciar sesión para acceder al carrito" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AumentarCantidadCarrito(int productId)
        {
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);
            Carrito productoEnCarrito = _CarritoBusiness.GetProductoById(productId, usuario.ID);
            if (productoEnCarrito != null)
            {
                // Si el producto ya existe en el carrito, incrementa la cantidad en 1
                productoEnCarrito.Cantidad++;
                _CarritoBusiness.Save(productoEnCarrito);
            }
            return Json(new { success = true });
        }

        public JsonResult DisminuirCantidad(int productId)
        {
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);
            Carrito productoEnCarrito = _CarritoBusiness.GetProductoById(productId, usuario.ID);

            if (usuario != null)
            {
                if (productoEnCarrito != null && productoEnCarrito.Cantidad > 1)
                {
                    // Si el producto ya existe en el carrito, incrementa la cantidad en 1
                    productoEnCarrito.Cantidad--;
                    _CarritoBusiness.Save(productoEnCarrito);
                }
                else if (productoEnCarrito != null && productoEnCarrito.Cantidad == 1)
                {
                    _CarritoBusiness.RemoverProducto(productoEnCarrito);
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public JsonResult EliminarProductoDelCarrito(int productId)
        {
            string userEmail = Request.Cookies["Usuario"]?.Value;

            // Obtener el usuario asociado al correo electrónico
            Usuario usuario = _UsuarioBusiness.GetByEmail(userEmail);
            Carrito productoEnCarrito = _CarritoBusiness.GetProductoById(productId, usuario.ID);
            if (productoEnCarrito != null)
            {
                _CarritoBusiness.RemoverProducto(productoEnCarrito);
            }
            return Json(new { success = true });
        }

        public ActionResult VerProductosPorCategoria(int categoriaId)
        {
            List<ProductoView> lista = _ProductoBusiness.GetProductoPorCategoria(categoriaId);
            ViewBag.Productos = lista;
            return View();
        }

        public ActionResult Editar() { return View(); }


    }
}
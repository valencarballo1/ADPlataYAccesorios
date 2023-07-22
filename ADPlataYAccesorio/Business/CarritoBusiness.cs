using ADPlataYAccesorio.Models;
using ADPlataYAccesorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ADPlataYAccesorio.Repository.CarritoRepository;

namespace ADPlataYAccesorio.Business
{
    public class CarritoBusiness
    {
        private CarritoRepository _CarritoRepository;
        private ProductoRepository _ProductoRepository;

        public CarritoBusiness()
        {
            this._CarritoRepository = new CarritoRepository();
            this._ProductoRepository = new ProductoRepository();
        }

        public Carrito  GetProductoById(int id, int productoId)
        {
            try
            {
                return _CarritoRepository.GetProductoById(id, productoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void AgregarProductoAlCarrito()
        {
            throw new NotImplementedException();
        }

        public void AgregarProductoAlCarrito(Producto producto, int usuarioId)
        {
            try
            {
                Carrito ProductoACarrito = new Carrito();
                ProductoACarrito.Cantidad = 1;
                ProductoACarrito.PrecioUnitario = producto.Precio;
                ProductoACarrito.ProductoId = producto.Id;
                ProductoACarrito.UsuarioId = usuarioId;
                _CarritoRepository.Save(ProductoACarrito);
                producto.Carrito.Add(ProductoACarrito);
                _ProductoRepository.Save(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CarritoView> VerTodos(int usuarioId)
        {
            try
            {
                return _CarritoRepository.VerTodos(usuarioId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Save(Carrito productoEnCarrito)
        {
            try
            {
                _CarritoRepository.Save(productoEnCarrito);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void RemoverProducto(Carrito productoEnCarrito)
        {
            try
            {
                _CarritoRepository.RemoverProducto(productoEnCarrito);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
using ADPlataYAccesorio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using static ADPlataYAccesorio.Repository.ProductoRepository;

namespace ADPlataYAccesorio.Repository
{
    public class CarritoRepository
    {
        public Carrito GetProductoById(int id, int usuarioId)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                return db.Carrito.Where(c => c.ProductoId == id && c.UsuarioId == usuarioId).FirstOrDefault();
            }
        }

        public void RemoverProducto(Carrito productoEnCarrito)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                if (db.Entry(productoEnCarrito).State == EntityState.Detached)
                {
                    db.Carrito.Attach(productoEnCarrito);
                }

                db.Carrito.Remove(productoEnCarrito);
                db.SaveChanges(); // Guardar los cambios en la base de datos
            }
        }

        public void Save(Carrito productoACarrito)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                db.Carrito.AddOrUpdate(productoACarrito);
                db.SaveChanges();
            }
        }

        internal List<CarritoView> VerTodos(int usuarioId)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                List<Carrito> carrito = db.Carrito.Include("Producto").Where(c => c.UsuarioId == usuarioId).ToList();
                List<CarritoView> CarritoView = carrito.Select(c => new CarritoView(c)).ToList();
                return CarritoView;
            }
        }

        public class CarritoView
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public string ImgUrl { get; set; }

            public int ProductoId { get; set; }

            public CarritoView(Carrito carrito)
            {
                Producto = carrito.Producto.Nombre;
                Cantidad = carrito.Cantidad.Value;
                PrecioUnitario = carrito.PrecioUnitario.Value;
                ImgUrl = carrito.Producto.ImgUrl;
                ProductoId = carrito.Producto.Id;
            }
        }
    }
}
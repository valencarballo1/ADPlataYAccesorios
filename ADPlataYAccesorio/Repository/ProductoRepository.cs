using ADPlataYAccesorio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ADPlataYAccesorio.Repository
{
    public class ProductoRepository
    {
        public void Save(Producto producto)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                db.Producto.AddOrUpdate(producto);
                db.SaveChanges();
            }
        }

        public List<ProductoView> GetAll()
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                List<Producto> productos = db.Producto.Include("Categoria").ToList();
                List<ProductoView> productosView = productos.Select(p => new ProductoView(p)).ToList();
                return productosView;
            }
        }

        public List<Categoria> GetAllCategorias()
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                List<Categoria> categorias = db.Categoria.Include("Producto").ToList();
                return categorias;
            }
        }

        public void AgregarOModificarCategoria(Categoria categoria)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                db.Categoria.AddOrUpdate(categoria);
                db.SaveChanges();
            }
        }

        internal Producto GetProducto(int id)
        {
            using (ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                return db.Producto.Include("Categoria").Where(p => p.Id == id).FirstOrDefault();
            }
        }

        public List<ProductoView> GetProductoPorCategoria(int categoriaId)
        {
            using(ADPLATAYACCESORIOEntities db = new ADPLATAYACCESORIOEntities())
            {
                List<Producto> productos = db.Producto.Include("Categoria").Where(p => p.CategoriaId == categoriaId).ToList();
                List<ProductoView> productosView = productos.Select(p => new ProductoView(p)).ToList();
                return productosView;
            }
        }

        public class ProductoView
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public string Detalle { get; set; }
            public string CategoriaNombre { get; set; }
            public string Coleccion { get; set; }
            public string ImgUrl { get; set; }

            public int CategoriaId { get; set; }

            // Otros campos que desees incluir

            public ProductoView(Producto producto)
            {
                Id = producto.Id;
                Nombre = producto.Nombre;
                Precio = producto.Precio;
                Detalle = producto.Detalle;
                CategoriaNombre = producto.Categoria.Nombre;
                Coleccion = producto.Coleccion;
                ImgUrl = producto.ImgUrl;
                CategoriaId = producto.CategoriaId;
                // Asignar otros campos
            }
        }
    }
}
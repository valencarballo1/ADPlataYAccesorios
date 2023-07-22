using ADPlataYAccesorio.Models;
using ADPlataYAccesorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ADPlataYAccesorio.Repository.ProductoRepository;

namespace ADPlataYAccesorio.Business
{
    public class ProductoBusiness
    {
        private ProductoRepository _ProductoRepository;

        public ProductoBusiness()
        {
            this._ProductoRepository = new ProductoRepository();
        }

        public void AgregarOModificar(Producto producto)
        {
            _ProductoRepository.Save(producto);
        }

        public List<ProductoView> GetAll() { 
           return  _ProductoRepository.GetAll();
        }

        public List<Categoria> GetAllCategorias()
        {
            return _ProductoRepository.GetAllCategorias();

        }

        public void AgregarOModificarCategoria(Categoria categoria)
        {
            _ProductoRepository.AgregarOModificarCategoria(categoria);
        }

        public Producto GetProducto(int id)
        {
            return _ProductoRepository.GetProducto(id);
        }

        public List<ProductoView> GetProductoPorCategoria(int categoriaId)
        {
            try
            {
                return _ProductoRepository.GetProductoPorCategoria(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
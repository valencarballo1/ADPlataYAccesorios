//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADPlataYAccesorio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carrito
    {
        public int Id { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public Nullable<int> ProductoId { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<decimal> PrecioUnitario { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

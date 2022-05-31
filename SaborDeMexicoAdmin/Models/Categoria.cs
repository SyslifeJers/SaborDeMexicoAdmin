using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SaborDeMexicoAdmin.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Activo { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}

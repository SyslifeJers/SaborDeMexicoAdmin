using System.Collections.Generic;

namespace SaborDeMexicoAdmin.Models.ModelViews
{
    public class ModelMenu
    {
        public List<Orden> GetListOrden { get; set; }
        public List<Producto> GetListProduc { get; set; }
        public List<Repartidor> GetListRepart { get; set; }
    }
}

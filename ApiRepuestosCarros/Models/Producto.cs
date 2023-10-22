namespace ApiRepuestosCarros.Models
{
    public class Producto
    {
        public int id_producto { get; set; }
        public string cod_producto { get; set; } = string.Empty;
        public string cod_barra { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public decimal precio { get; set; }
        //public byte imagen { get; set; }
        public int id_categoria { get; set; }
        public int id_sucursal { get; set; }

        public byte[] image { get; set; }


        //public List<Categoria> categoria { get; set; }
        //public List<Sucursal> sucursal { get; set; }

    }
}

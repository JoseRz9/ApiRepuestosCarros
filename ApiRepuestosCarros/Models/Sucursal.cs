namespace ApiRepuestosCarros.Models
{
    public class Sucursal
    {
        public int id_sucursal { get; set; }
        public string cod_sucursal { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public int id_ubicacion { get; set; }

        //public List<Ubicacion> ubicacion {  get; set; }
        public Ubicacion Ubicacion { get; set; }    
    }
}

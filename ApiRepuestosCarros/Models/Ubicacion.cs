namespace ApiRepuestosCarros.Models
{
    public class Ubicacion
    {
        public int id_ubicacion { get; set; }
        public string cod_ubicacion { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public decimal longitud { get; set; }
        public decimal latitud {  get; set; }   

    }
}

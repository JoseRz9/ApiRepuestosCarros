using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Contracts
{
    public interface IProductoRepository
    {
        public Task<IEnumerable<Producto>> GetProductos();
        public Task<IEnumerable<Producto>> GetProductosSucursal(int id_sucursal);
        public Task<Producto> GetProducto(int id_producto);
        public Task CreateProducto(Producto producto);
        public Task UpdateProducto(int id_producto, Producto producto);
        public Task DeleteProducto(int id_producto);

        public Task CreateProductoImagen(IFormFile file, string cod_prod, string cod_barr, string nom, string desc, decimal prec, int id_cat, int id_suc);

        public Task<Producto> GetImagenProducto(int id_producto);


    }
}

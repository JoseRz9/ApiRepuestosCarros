using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Contracts
{
    public interface ICategoriaRepository
    {
        public Task<IEnumerable<Categoria>> GetCategorias();
        public Task<Categoria> GetCategoria(int id_categoria);
        public Task CreateCategoria(Categoria categoria);
        public Task UpdateCategoria(int id_categoria,Categoria categoria);
        public Task DeleteCategoria(int id_categoria);
        //public Task<Categoria> GetCategoriaProducto(int id_categoria);
        //public Task<List<Categoria>> GetAllCategoriasProductos();
         
    }
}

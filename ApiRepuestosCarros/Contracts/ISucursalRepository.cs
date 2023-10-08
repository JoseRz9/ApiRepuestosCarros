using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Contracts
{
    public interface ISucursalRepository
    {
        public Task<IEnumerable<Sucursal>> GetSucursales();
        public Task<Sucursal> GetSucursal(int id_sucursal);
        public Task CreateSucursal(Sucursal sucursal);
        public Task UpdateSucursal(int id_sucursal, Sucursal sucursal);
        public Task DeleteSucursal(int id_sucursal);
    }
}

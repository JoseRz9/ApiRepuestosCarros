using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Contracts
{
    public interface IUbicacionRepository
    {
        public Task<IEnumerable<Ubicacion>> GetUbicaciones();
        public Task<Ubicacion> GetUbicacion(int id_ubicacion);
        public Task CreateUbicacion(Ubicacion ubicacion);
        public Task UpdateUbicacion(int id_ubicacion, Ubicacion ubicacion);
        public Task DeleteUbicacion(int id_ubicacion);
    }
}

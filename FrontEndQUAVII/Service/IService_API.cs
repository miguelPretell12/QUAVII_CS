using FrontEndQUAVII.Models;

namespace FrontEndQUAVII.Service
{
    public interface IService_API
    {
        Task<Usuarios> Ingresar(Usuarios usu);
        Task<List<OrdenTrabajo>> listarMedidorXusuario(Usuarios usu);
        Task<List<OrdenTrabajo>> listarMedidorCiclo(OrdenTrabajo ot);
        Task<List<OrdenTrabajo>> listarPendienteRegistrado(OrdenTrabajo ot);
        Task<OrdenTrabajo> obtenerOrdenTrabajo(OrdenTrabajo ot);
        Task<OrdenTrabajo> registrarLectura(OrdenTrabajo ot);
        Task<List<TiposObservaciones>> listarTipoObservaciones();

    }
}

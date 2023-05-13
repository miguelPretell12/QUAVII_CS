using Backend_Quavii.Datos;
using Backend_Quavii.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Quavii.Controllers
{

    [ApiController]
    [Route("api/ordentrabajo")]
    public class OrdenTrabajoController : Controller
    {
        [HttpGet]
        public dynamic  Get()
        {
            var fn = new DOrdenTrabajo();
            var result =  fn.listarTableDBO();
            var result_error = DOrdenTrabajo.GlobalData.detail;
            if (result_error is not null)
            {
                return new
                {
                    success = false,
                    detalle = result_error,
                    message = "error lectura"
                };
            }
            else
                return new
                {
                    success = true,
                    message = "ok",
                    result = result,
                };
        }

        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<RespuestaAPI>> Get(int id)
        {
            var fn = new DOrdenTrabajo();
            var usu = new Usuarios();
            usu.IdUsuario = id;
            var lista = await fn.mostrarMedidor(usu);
            RespuestaAPI obj = new RespuestaAPI
            {
                listas = lista,
            };
            return obj;
        }

        [HttpPost("usuarios/medidorCiclo")]
        public async Task<ActionResult<RespuestaAPI>> medidorCiclo([FromBody] OrdenTrabajo ot){
            var fn = new DOrdenTrabajo();

            var list = await fn.mostrarMedidorCiclo(ot);

            RespuestaAPI obj = new RespuestaAPI
            {
                listas=list
            };
            return obj;
        }

        [HttpPost("usuarios/medidorPendienteRegistro")]
        public async Task<ActionResult<RespuestaAPI>> medidorPendienteRegistro([FromBody] OrdenTrabajo ot)
        {
            var fn = new DOrdenTrabajo();

            var list = await fn.mostrarMedidorPendienteRegistrado(ot);

            RespuestaAPI obj = new RespuestaAPI
            {
                listas = list
            };
            return obj;
        }

        [HttpPost("{id}")] // 
        public async Task<ActionResult<OrdenTrabajo>> registrarLectura(int id, [FromBody] OrdenTrabajo ot)
        {
            var fn = new DOrdenTrabajo();
            ot.IdOrdenTrabajo = id;
            var resp = await fn.registrarLectura(ot);

            return resp;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenTrabajo>> obtenerOrdenTrabajo(int id)
        {
            var fn=new DOrdenTrabajo();
            OrdenTrabajo ot = new OrdenTrabajo();
            ot.IdOrdenTrabajo = id;
            var objecto =  await fn.obtenerOrdenTrabajo(ot);

            return objecto;
        }
    }
}

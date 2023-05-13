using Backend_Quavii.Datos;
using Backend_Quavii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Quavii.Controllers
{
    [ApiController]
    [Route("api/tipoobservacion")]
    public class TipoObservacionController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<RespuestaAPI>> Get()
        {
            var fn = new DTipoObservacion();
            var listato = await fn.ListarTipoObservacion();

            RespuestaAPI obj = new RespuestaAPI
            {
                listaTO=listato
            };

            return obj;
        }
    }
}

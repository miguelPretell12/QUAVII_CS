using Backend_Quavii.Datos;
using Backend_Quavii.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Quavii.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> Get()
        {
            var funcion = new DUsuarios();

            var lista = await funcion.mostrarUsuarios();

            return lista;
        }

        // Esta API, funciona para ingresar a la aplicacion
        [HttpPost("ingresar")]
        public async Task<Usuarios> Post([FromBody] Usuarios usus)
        {
            var funcion =new DUsuarios();
            var usuarioExiste =  await funcion.IngresarAplicacion(usus);
            return usuarioExiste;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]  Usuarios usu)
        {
            var fn = new DUsuarios();
            usu.IdUsuario = id;
            await fn.EditarUsuario(usu);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var fn = new DUsuarios();
            var usu = new Usuarios();
            usu.IdUsuario = id;
            await fn.EliminarUsuario(usu);

            return NoContent();
        }
    }
}

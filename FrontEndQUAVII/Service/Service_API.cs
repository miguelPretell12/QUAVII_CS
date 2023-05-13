using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FrontEndQUAVII.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Text;

namespace FrontEndQUAVII.Service
{
    public class Service_API : IService_API
    {
        private readonly IConfiguration _configuration;
        public string url;

        public Service_API (IConfiguration configuration)
        {
            _configuration = configuration;
            url = configuration["url"];
        }
        //public string url = _configuration.GetValue("")
            //"https://localhost:5001/api/";

        public async Task<Usuarios> Ingresar(Usuarios usu)
        {
            Usuarios usuario = new Usuarios();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);

            var content = new StringContent(JsonConvert.SerializeObject(usu), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("usuarios/ingresar", content);

            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<Usuarios>(json_respuesta);
            usuario = resultado;

            return usuario;
        }

        public async Task<List<OrdenTrabajo>> listarMedidorCiclo(OrdenTrabajo ot)
        {
            List<OrdenTrabajo> lista = new List<OrdenTrabajo>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);
            var apis = "ordentrabajo/usuarios/medidorCiclo";
            var content = new StringContent(JsonConvert.SerializeObject(ot), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync(apis, content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lista = resultado.listas;
            }
            return lista;
        }

        public async Task<List<OrdenTrabajo>> listarMedidorXusuario(Usuarios usu)
        {
            List<OrdenTrabajo> lista = new List<OrdenTrabajo>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);
            var apis = string.Format("ordentrabajo/usuarios/{0}", usu.IdUsuario);
            var response = await cliente.GetAsync(apis);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lista = resultado.listas;
            }

            return lista;
        }

        public async Task<List<OrdenTrabajo>> listarPendienteRegistrado(OrdenTrabajo ot)
        {
            List<OrdenTrabajo> lista = new List<OrdenTrabajo>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);
            var apis = "ordentrabajo/usuarios/medidorPendienteRegistro";
            var content = new StringContent(JsonConvert.SerializeObject(ot), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync(apis, content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lista = resultado.listas;
            }
            return lista;
        }

        public async Task<OrdenTrabajo> obtenerOrdenTrabajo(OrdenTrabajo ot)
        {
            OrdenTrabajo ordenTrabajo = new OrdenTrabajo();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);
            var urls = string.Format("ordentrabajo/{0}",ot.IdOrdenTrabajo);
            var response = await cliente.GetAsync(urls);

            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<OrdenTrabajo>(json_respuesta);
            ordenTrabajo = resultado;

            return ordenTrabajo;
        }

        public async Task<OrdenTrabajo> registrarLectura(OrdenTrabajo ot)
        {
            OrdenTrabajo ordentrabajo = new OrdenTrabajo();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);

            var content = new StringContent(JsonConvert.SerializeObject(ot), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("ordentrabajo/"+ot.IdOrdenTrabajo, content);

            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<OrdenTrabajo>(json_respuesta);
            ordentrabajo = resultado;

            return ordentrabajo;
        }

        public async Task<List<TiposObservaciones>> listarTipoObservaciones()
        {
            List<TiposObservaciones> listaTO = new List<TiposObservaciones>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(url);
            var apis = "tipoobservacion";
            var response = await cliente.GetAsync(apis);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                listaTO = resultado.listaTO;
            }
            return listaTO;
        }
    }
}

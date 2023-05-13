using ClosedXML.Excel;
using FrontEndQUAVII.Helpers;
using FrontEndQUAVII.Models;
using FrontEndQUAVII.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FrontEndQUAVII.Controllers
{
    public class ListadoController : Controller
    {
        private readonly IService_API _servicioAPI;
        private readonly IHttpContextAccessor _contextAccessor;
        private IWebHostEnvironment webHostEnvironment;
        [TempData]
        public string Message { get; set; }

        public ListadoController(IService_API servicioapi, IHttpContextAccessor contextAccessor, IWebHostEnvironment environment)
        {
            _servicioAPI = servicioapi;
            _contextAccessor = contextAccessor;
            webHostEnvironment = environment;
        }

        // GET: ListadoController
        public ActionResult Index()
        {
            
            if (_contextAccessor.HttpContext.Session.GetInt32("IdUsuario") is null || _contextAccessor.HttpContext.Session.GetInt32("IdUsuario") == 0)
            {
                return Redirect("/");
            }
            else
            {
                TempData["Idusuario"] = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");
                return View();
            }
        }

        //[HttpGet]
        public async Task<ActionResult> ListadoMedidor()
        {
            TempData["Idusuario"] = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");
            Usuarios usu = new Usuarios();

            Usuarios usus = new Usuarios();
            usus.IdUsuario = -1;
            List<Usuarios> usua = new List<Usuarios>();
            usua.Add(usus);
            
            usu.IdUsuario = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario") ;
            
            var lista = await _servicioAPI.listarMedidorXusuario(usu);
            if (Convert.ToInt32(TempData["Idusuario"].ToString()) != 0)
            {
                return Json(lista);
            }
                return Json(usua);

            //return Redirect("/");
            //return RedirectToAction("Index","Listado");
        }

        [HttpGet]
        public async Task<List<TiposObservaciones>> ListarTipoObservaciones()
        {
            var listaTO = await _servicioAPI.listarTipoObservaciones();
            return listaTO;
        }

        [HttpPost]
        public async Task<List<OrdenTrabajo>> ListadoMedidorCiclo(OrdenTrabajo ot)
        {
            ot.IdUsuario = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");

            var lista = await _servicioAPI.listarMedidorCiclo(ot);
            return lista;
        }

        [HttpPost]
        public async Task<List<OrdenTrabajo>> ListadoRegistroPendiente(OrdenTrabajo ot )
        {
            ot.IdUsuario = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");

            var lista = await _servicioAPI.listarPendienteRegistrado(ot);
            return lista;
        }

        [HttpPost]
        public async Task<OrdenTrabajo> obtenerOT(OrdenTrabajo ot)
        {
            var lista = await _servicioAPI.obtenerOrdenTrabajo(ot);

            return lista;
        }

        [HttpPost]
        public async Task<OrdenTrabajo> guardarLectura(OrdenTrabajo ot)
        {
            string result = String.Empty;
            string imgs = "";
            var files = Request.Form.Files;
            var ruta = webHostEnvironment.WebRootPath + "\\Uploads\\OrdenTrabajo";
            if (files.Count()>0)
            {                
                foreach (IFormFile source in files)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".jpeg";
                    // System.IO.Path.GetExtension(source.FileName)
                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }
                    string imagepath = getActualPath(FileName);

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                    }
                    //ot.imagenes = "";
                     imgs += FileName+";";
                }
            }
            ot.imagenes = imgs;
            ot.ruta = ruta;
            var guardar = await _servicioAPI.registrarLectura(ot);
            return guardar;
        }

        public string getActualPath(string Filename)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\OrdenTrabajo\\", Filename);
        }

        [HttpPost]
        public async Task<Usuarios> cerrar()
        {
            Usuarios usu = new Usuarios();
            usu.IdUsuario = 0;
            _contextAccessor.HttpContext.Session.Remove("IdUsuario");
            _contextAccessor.HttpContext.Session.Clear();
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return usu;
        }

        [HttpGet]
        public Session sesion()
        {
            Session os = new Session();

            os.IdUsuario = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");

            return os;
        }

        [HttpPost]
        public async Task<IActionResult> exportData(string pendiente,string registrado)
        {
            OrdenTrabajo ot = new OrdenTrabajo();
            ot.opcion1 = Convert.ToInt32(registrado);
            ot.opcion2 = Convert.ToInt32(pendiente);
            ot.IdUsuario = (int)_contextAccessor.HttpContext.Session.GetInt32("IdUsuario");
            var lista = await _servicioAPI.listarPendienteRegistrado(ot);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("lista de medidores");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "TipoTrabajo";
                worksheet.Cell(currentRow, 2).Value = "SectorOperativo";
                worksheet.Cell(currentRow, 3).Value = "Producto";
                worksheet.Cell(currentRow, 4).Value = "Contrato";
                worksheet.Cell(currentRow, 5).Value = "Ciclo";
                worksheet.Cell(currentRow, 6).Value = "Medidor";
                worksheet.Cell(currentRow, 7).Value = "CoordenadaX";
                worksheet.Cell(currentRow, 8).Value = "CoordenadaY";
                worksheet.Cell(currentRow, 9).Value = "LEC";
                worksheet.Cell(currentRow, 10).Value = "DistritoSuministro";
                worksheet.Cell(currentRow, 11).Value = "ProvinciaSuministro";
                worksheet.Cell(currentRow, 12).Value = "DepartamentoSuministro";
                worksheet.Cell(currentRow, 13).Value = "Barrio";
                worksheet.Cell(currentRow, 14).Value = "UbicacionGeografica";
                worksheet.Cell(currentRow, 15).Value = "DetalleDireccion";
                worksheet.Cell(currentRow, 16).Value = "ProvinciaSuministro";
                worksheet.Cell(currentRow, 17).Value = "NuevaLectura";
                worksheet.Cell(currentRow, 18).Value = "OrdenReparto";
                worksheet.Cell(currentRow, 19).Value = "UsuarioReparto";
                worksheet.Cell(currentRow, 20).Value = "Observacion";
                worksheet.Cell(currentRow, 21).Value = "FechaHoraLectura";
                worksheet.Cell(currentRow, 22).Value = "ProvinciaSuministro";
                worksheet.Cell(currentRow, 23).Value = "Tipo de Observacion";
                worksheet.Cell(currentRow, 24).Value = "UsuarioReparto";
            
                foreach(var ordentrabajo in lista) {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = ordentrabajo.TipoTrabajo;
                    worksheet.Cell(currentRow, 2).Value = ordentrabajo.SectorOperativo;
                    worksheet.Cell(currentRow, 3).Value = ordentrabajo.Producto;
                    worksheet.Cell(currentRow, 4).Value = ordentrabajo.Contrato;
                    worksheet.Cell(currentRow, 5).Value = ordentrabajo.Ciclo;
                    worksheet.Cell(currentRow, 6).Value = ordentrabajo.Medidor;
                    worksheet.Cell(currentRow, 7).Value = ordentrabajo.CoordenadaX;
                    worksheet.Cell(currentRow, 8).Value = ordentrabajo.CoordenadaY;
                    worksheet.Cell(currentRow, 9).Value = ordentrabajo.LEC;
                    worksheet.Cell(currentRow, 10).Value = ordentrabajo.DistritoSuministro;
                    worksheet.Cell(currentRow, 11).Value = ordentrabajo.ProvinciaSuministro;
                    worksheet.Cell(currentRow, 12).Value = ordentrabajo.DepartamentoSuministro;
                    worksheet.Cell(currentRow, 13).Value = ordentrabajo.Barrio;
                    worksheet.Cell(currentRow, 14).Value = ordentrabajo.UbicacionGeografica;
                    worksheet.Cell(currentRow, 15).Value = ordentrabajo.DetalleDireccion;
                    worksheet.Cell(currentRow, 16).Value = ordentrabajo.ProvinciaSuministro;
                    worksheet.Cell(currentRow, 17).Value = ordentrabajo.NuevaLectura;
                    worksheet.Cell(currentRow, 18).Value = ordentrabajo.ordenReparto;
                    worksheet.Cell(currentRow, 19).Value = ordentrabajo.UsuarioReparto;
                    worksheet.Cell(currentRow, 20).Value = ordentrabajo.observacion;
                    worksheet.Cell(currentRow, 21).Value = ordentrabajo.FechaHoraLectura;
                    worksheet.Cell(currentRow, 22).Value = ordentrabajo.ProvinciaSuministro;
                    worksheet.Cell(currentRow, 23).Value = ordentrabajo.descripcion;
                    worksheet.Cell(currentRow, 24).Value = ordentrabajo.UsuarioReparto;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
                }
            }
        }
    }
}

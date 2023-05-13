using FrontEndQUAVII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using FrontEndQUAVII.Service;

namespace FrontEndQUAVII.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService_API _servicioAPI;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IService_API servicioapi, IHttpContextAccessor contextAccessor)
        {
            _servicioAPI = servicioapi;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            if (_contextAccessor.HttpContext.Session.GetInt32("IdUsuario") is null || _contextAccessor.HttpContext.Session.GetInt32("IdUsuario") ==0)
            {
                return View();
            }else
            {
                return Redirect("/Listado");
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<Usuarios> Ingresar(Usuarios usu)
        {
            var us = await _servicioAPI.Ingresar(usu);
            _contextAccessor.HttpContext.Session.SetInt32("IdUsuario", us.IdUsuario);
            _contextAccessor.HttpContext.Session.SetString("Nombres", us.Nombres is null?"": us.Nombres);
            _contextAccessor.HttpContext.Session.SetString("AppPaterno", us.ApPaterno is null ? "" : us.ApPaterno);
            _contextAccessor.HttpContext.Session.SetString("AppMaterno", us.ApMaterno is null ? "" : us.ApMaterno );
            _contextAccessor.HttpContext.Session.SetInt32("abierto", 1);
                        
            return us;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
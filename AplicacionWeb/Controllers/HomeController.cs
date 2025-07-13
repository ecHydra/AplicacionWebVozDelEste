using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Linq;
using AplicacionWeb.Models;

public class HomeController : Controller
{
    [HttpGet]
    public async Task<JsonResult> ObtenerClima()
    {
        string apiKey = "c38acbda7c2172cd90e3748c1d65c893";
        string ciudad = "Maldonado,UY";
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={apiKey}&units=metric&lang=es";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var respuesta = await client.GetStringAsync(url);
                var datos = JObject.Parse(respuesta);

                var clima = new
                {
                    temp = Math.Round((double)datos["main"]["temp"]),
                    descripcion = (string)datos["weather"][0]["description"],
                    icono = (string)datos["weather"][0]["icon"]
                };

                return Json(clima, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "No se pudo obtener el clima." }, JsonRequestBehavior.AllowGet);
            }
        }
    }

    public dynamic GetViewBag()
    {
        return ViewBag;
    }

    public ActionResult Index()
    {
        using (var db = new Prueba2Entities())
        {
            ViewBag.Noticias = db.Noticias
                                 .OrderByDescending(n => n.Fecha_Publicacion)
                                 .Take(4)
                                 .ToList();

            ViewBag.Patrocinadores = db.Patrocinadores
                                       .Where(p => p.Imagen_Ruta != null)
                                       .ToList();
        }

        return View();
    }
    public ActionResult Clima()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> ObtenerPronostico()
    {
        string apiKey = "c38acbda7c2172cd90e3748c1d65c893";
        string ciudad = "Maldonado,UY";
        string url = $"https://api.openweathermap.org/data/2.5/forecast?q={ciudad}&appid={apiKey}&units=metric&lang=es";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var respuesta = await client.GetStringAsync(url);
                var datos = JObject.Parse(respuesta);

                var lista = datos["list"].ToList(); // ✅ Convertir a lista
                var pronostico = lista
                    .Where((item, index) => index % 8 == 0)
                    .Take(4)
                    .Select(item => new
                    {
                        fecha = DateTime.Parse(item["dt_txt"].ToString()).ToString("dddd dd", new System.Globalization.CultureInfo("es-ES")),
                        temp_min = Math.Round((double)item["main"]["temp_min"]),
                        temp_max = Math.Round((double)item["main"]["temp_max"]),
                        descripcion = (string)item["weather"][0]["description"],
                        icono = (string)item["weather"][0]["icon"]
                    });

                return Json(new { pronostico }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "No se pudo obtener el pronóstico." }, JsonRequestBehavior.AllowGet);
            }
        }
    }

    public ActionResult NoticiaDestacadas()
    {
        using (var db = new Prueba2Entities())
        {
            var noticias = db.Noticias
                             .OrderByDescending(n => n.Fecha_Publicacion)
                             .Take(4)
                             .ToList();

            return PartialView("~/Views/Shared/_NoticiaDestacadas.cshtml", noticias);
        }
    }

    public ActionResult PublicidadRotativa()
    {
        using (var db = new Prueba2Entities())
        {
            var patrocinadores = db.Patrocinadores
                                   .Where(p => p.Imagen_Ruta != null)
                                   .ToList();

            return PartialView("~/Views/Home/_Publicidad.cshtml", patrocinadores); // <- Este nombre es el que definiste tú
        }
    }


}


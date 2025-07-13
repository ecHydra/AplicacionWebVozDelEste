using AplicacionWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

public class LoginController : Controller
{
    private Prueba2Entities db = new Prueba2Entities();

    // GET: Login
    public ActionResult Index()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public ActionResult Index(string email, string contraseña)
    {
        var usuario = db.Usuarios.Include(u => u.Roles1) // <- cambio acá
                         .FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

        if (usuario != null)
        {
            Session["UsuarioId"] = usuario.Id_Usuario;
            Session["UsuarioNombre"] = usuario.Nombre;
            Session["Roles"] = new List<string> { usuario.Roles1?.NombreRol ?? "" };

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Mensaje = "Credenciales inválidas.";
        return View();
    }
    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}

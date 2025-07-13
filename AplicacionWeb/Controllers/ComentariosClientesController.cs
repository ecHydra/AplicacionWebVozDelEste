using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    public class ComentariosClientesController : Controller
    {
        private Prueba2Entities db = new Prueba2Entities();

        // GET: ComentariosClientes
        [AuthorizeByRole("Usuario", "Editor", "Admin")]
        public ActionResult Index()
        {
            var comentarios = db.ComentariosCliente
                                .Include(c => c.Programas)
                                .Include(c => c.Usuarios)
                                .ToList();
            return View(comentarios);
        }

        // GET: ComentariosClientes/Details/5
        [AuthorizeByRole("Usuario", "Editor", "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comentario = db.ComentariosCliente.Find(id);
            if (comentario == null)
                return HttpNotFound();

            return View(comentario);
        }

        // GET: ComentariosClientes/Create
        [AuthorizeByRole("Usuario")]
        public ActionResult Create()
        {
            ViewBag.Id_Programa = new SelectList(db.Programas, "Id_Programa", "Nombre_Programa");
            return View();
        }

        // POST: ComentariosClientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Usuario")]
        public ActionResult Create([Bind(Include = "Id_Programa,Comentarios")] ComentariosCliente comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.Id_Usuario = (int)Session["UsuarioId"];
                comentario.Fecha_Comentario = DateTime.Now;

                db.ComentariosCliente.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Programa = new SelectList(db.Programas, "Id_Programa", "Nombre_Programa", comentario.Id_Programa);
            return View(comentario);
        }

        // GET: ComentariosClientes/Edit/5
        [AuthorizeByRole("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comentario = db.ComentariosCliente.Find(id);
            if (comentario == null)
                return HttpNotFound();

            ViewBag.Id_Programa = new SelectList(db.Programas, "Id_Programa", "Nombre_Programa", comentario.Id_Programa);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", comentario.Id_Usuario);
            return View(comentario);
        }

        // POST: ComentariosClientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Admin")]
        public ActionResult Edit([Bind(Include = "Id_Comentario,Id_Usuario,Id_Programa,Comentarios,Fecha_Comentario")] ComentariosCliente comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Programa = new SelectList(db.Programas, "Id_Programa", "Nombre_Programa", comentario.Id_Programa);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombre", comentario.Id_Usuario);
            return View(comentario);
        }

        // GET: ComentariosClientes/Delete/5
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comentario = db.ComentariosCliente.Find(id);
            if (comentario == null)
                return HttpNotFound();

            return View(comentario);
        }

        // POST: ComentariosClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var comentario = db.ComentariosCliente.Find(id);
            db.ComentariosCliente.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}


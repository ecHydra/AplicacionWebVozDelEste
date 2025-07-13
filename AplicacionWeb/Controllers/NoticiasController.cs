using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    
    public class NoticiasController : Controller
    {
        private Prueba2Entities db = new Prueba2Entities();

        // GET: Noticias
        
        public ActionResult Index(string categoria)
        {
            var noticias = db.Noticias.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
            {
                noticias = noticias.Where(n => n.Categoria == categoria);
                ViewBag.Categoria = categoria;
            }

            return View(noticias.ToList());
        }

        // GET: Noticias/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
                return HttpNotFound();

            var relacionadas = db.Noticias
                .Where(n => n.Categoria == noticias.Categoria && n.Id_Noticia != noticias.Id_Noticia)
                .OrderByDescending(n => n.Fecha_Publicacion)
                .Take(4)
                .ToList();

            ViewBag.Relacionadas = relacionadas;
            return View(noticias);
        }

        // GET: Noticias/Create
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Create(Noticias noticia, HttpPostedFileBase imagenFile)
        {
            if (ModelState.IsValid)
            {
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imagenFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Imagenes"), fileName);
                    imagenFile.SaveAs(path);
                    noticia.Imagen_Ruta = fileName;
                }

                db.Noticias.Add(noticia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticia);
        }

        // GET: Noticias/Edit/5
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
                return HttpNotFound();

            return View(noticias);
        }

        // POST: Noticias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Edit([Bind(Include = "Id_Noticia,Titulo,Contenido,Fecha_Publicacion,Imagen_Ruta")] Noticias noticias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticias);
        }

        // GET: Noticias/Delete/5
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
                return HttpNotFound();

            return View(noticias);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticias noticias = db.Noticias.Find(id);
            db.Noticias.Remove(noticias);
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


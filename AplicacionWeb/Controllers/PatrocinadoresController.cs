using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    [AuthorizeByRole("Admin")]
    public class PatrocinadoresController : Controller
    {
        private Prueba2Entities db = new Prueba2Entities();

        // GET: Patrocinadores
        public ActionResult Index()
        {
            return View(db.Patrocinadores.ToList());
        }

        // GET: Patrocinadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Patrocinadores patrocinadores = db.Patrocinadores.Find(id);
            if (patrocinadores == null)
                return HttpNotFound();

            return View(patrocinadores);
        }

        // GET: Patrocinadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patrocinadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patrocinadores patrocinadores, HttpPostedFileBase ImagenFile)
        {
            if (ModelState.IsValid)
            {
                if (ImagenFile != null && ImagenFile.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(ImagenFile.FileName);
                    var path = Server.MapPath("~/Content/Imagenes/" + fileName);
                    ImagenFile.SaveAs(path);
                    patrocinadores.Imagen_Ruta = fileName;
                }

                db.Patrocinadores.Add(patrocinadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patrocinadores);
        }

        // GET: Patrocinadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Patrocinadores patrocinadores = db.Patrocinadores.Find(id);
            if (patrocinadores == null)
                return HttpNotFound();

            return View(patrocinadores);
        }

        // POST: Patrocinadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patrocinadores patrocinadores, HttpPostedFileBase ImagenFile)
        {
            if (ModelState.IsValid)
            {
                var existente = db.Patrocinadores.Find(patrocinadores.Id_Patrocinador);
                if (existente == null)
                    return HttpNotFound();

                existente.Nombre_Patrocinador = patrocinadores.Nombre_Patrocinador;
                existente.Veces_Por_Dia = patrocinadores.Veces_Por_Dia;
                existente.Url = patrocinadores.Url;

                if (ImagenFile != null && ImagenFile.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(ImagenFile.FileName);
                    var path = Server.MapPath("~/Content/Imagenes/" + fileName);
                    ImagenFile.SaveAs(path);
                    existente.Imagen_Ruta = fileName;
                }

                db.Entry(existente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patrocinadores);
        }

        // GET: Patrocinadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Patrocinadores patrocinadores = db.Patrocinadores.Find(id);
            if (patrocinadores == null)
                return HttpNotFound();

            return View(patrocinadores);
        }

        // POST: Patrocinadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patrocinadores patrocinadores = db.Patrocinadores.Find(id);
            db.Patrocinadores.Remove(patrocinadores);
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
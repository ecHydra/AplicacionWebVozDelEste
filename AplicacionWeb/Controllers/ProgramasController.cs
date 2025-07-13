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
    public class ProgramasController : Controller
    {
        private Prueba2Entities db = new Prueba2Entities();

        // GET: Programas (acceso público)
        public ActionResult Index()
        {
            return View(db.Programas.ToList());
        }

        // GET: Programas/Details/5 (acceso público)
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Programas programas = db.Programas.Find(id);
            if (programas == null)
                return HttpNotFound();

            return View(programas);
        }

        // GET: Programas/Create
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Programas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Create([Bind(Include = "Id_Programa,Nombre_Programa,Imagen_Ruta,Descripcion,Horario")] Programas programas)
        {
            if (ModelState.IsValid)
            {
                db.Programas.Add(programas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programas);
        }

        // GET: Programas/Edit/5
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Programas programas = db.Programas.Find(id);
            if (programas == null)
                return HttpNotFound();

            return View(programas);
        }

        // POST: Programas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Editor", "Admin")]
        public ActionResult Edit([Bind(Include = "Id_Programa,Nombre_Programa,Imagen_Ruta,Descripcion,Horario")] Programas programas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programas);
        }

        // GET: Programas/Delete/5
        [AuthorizeByRole("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Programas programas = db.Programas.Find(id);
            if (programas == null)
                return HttpNotFound();

            return View(programas);
        }

        // POST: Programas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeByRole("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Programas programas = db.Programas.Find(id);
            db.Programas.Remove(programas);
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

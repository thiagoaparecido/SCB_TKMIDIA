using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCB_TKMIDIA.Models;

namespace SCB_TKMIDIA.Controllers
{
    public class VENDEDOR_REMOTOController : Controller
    {
        private SCB_SPCINEEntities db = new SCB_SPCINEEntities();

        // GET: VENDEDOR_REMOTO
        public ActionResult Index()
        {
            var tB_VENDEDOR_REMOTO = db.TB_VENDEDOR_REMOTO.Include(t => t.TB_EMPRESA_COMPLEXO);
            return View(tB_VENDEDOR_REMOTO.ToList());
        }

        // GET: VENDEDOR_REMOTO/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO = db.TB_VENDEDOR_REMOTO.Find(id);
            if (tB_VENDEDOR_REMOTO == null)
            {
                return HttpNotFound();
            }
            return View(tB_VENDEDOR_REMOTO);
        }

        // GET: VENDEDOR_REMOTO/Create
        public ActionResult Create()
        {
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL");
            return View();
        }

        // POST: VENDEDOR_REMOTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VRE_CNPJ,EMP_CD_ANCINE,VRE_RZ_SOCIAL")] TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO)
        {
            if (ModelState.IsValid)
            {
                db.TB_VENDEDOR_REMOTO.Add(tB_VENDEDOR_REMOTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_VENDEDOR_REMOTO.EMP_CD_ANCINE);
            return View(tB_VENDEDOR_REMOTO);
        }

        // GET: VENDEDOR_REMOTO/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO = db.TB_VENDEDOR_REMOTO.Find(id);
            if (tB_VENDEDOR_REMOTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_VENDEDOR_REMOTO.EMP_CD_ANCINE);
            return View(tB_VENDEDOR_REMOTO);
        }

        // POST: VENDEDOR_REMOTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VRE_CNPJ,EMP_CD_ANCINE,VRE_RZ_SOCIAL")] TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_VENDEDOR_REMOTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_VENDEDOR_REMOTO.EMP_CD_ANCINE);
            return View(tB_VENDEDOR_REMOTO);
        }

        // GET: VENDEDOR_REMOTO/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO = db.TB_VENDEDOR_REMOTO.Find(id);
            if (tB_VENDEDOR_REMOTO == null)
            {
                return HttpNotFound();
            }
            return View(tB_VENDEDOR_REMOTO);
        }

        // POST: VENDEDOR_REMOTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TB_VENDEDOR_REMOTO tB_VENDEDOR_REMOTO = db.TB_VENDEDOR_REMOTO.Find(id);
            db.TB_VENDEDOR_REMOTO.Remove(tB_VENDEDOR_REMOTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

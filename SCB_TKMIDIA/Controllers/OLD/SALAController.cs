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
    public class SALAController : Controller
    {
        private SCB_SPCINEEntities db = new SCB_SPCINEEntities();

        // GET: SALA
        public ActionResult Index()
        {
            var tB_SALA = db.TB_SALA.Include(t => t.TB_EMPRESA_COMPLEXO);
            return View(tB_SALA.ToList());
        }

        // GET: SALA/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SALA tB_SALA = db.TB_SALA.Find(id);
            if (tB_SALA == null)
            {
                return HttpNotFound();
            }
            return View(tB_SALA);
        }

        // GET: SALA/Create
        public ActionResult Create()
        {
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL");
            return View();
        }

        // POST: SALA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SAL_CD_ANCINE,EMP_CD_ANCINE,SAL_NUM,SAL_DESC,SAL_QTD_LUG_PDR,SAL_QTD_LUG_ESP,SAL_SERIAL_PROJETOR,SAL_CD_AUX,SAL_DT_INC,SAL_DT_ALT,SAL_DT_DES,SAL_MOT_DES,SAL_SOM")] TB_SALA tB_SALA)
        {
            if (ModelState.IsValid)
            {
                db.TB_SALA.Add(tB_SALA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_SALA.EMP_CD_ANCINE);
            return View(tB_SALA);
        }

        // GET: SALA/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SALA tB_SALA = db.TB_SALA.Find(id);
            if (tB_SALA == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_SALA.EMP_CD_ANCINE);
            return View(tB_SALA);
        }

        // POST: SALA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SAL_CD_ANCINE,EMP_CD_ANCINE,SAL_NUM,SAL_DESC,SAL_QTD_LUG_PDR,SAL_QTD_LUG_ESP,SAL_SERIAL_PROJETOR,SAL_CD_AUX,SAL_DT_INC,SAL_DT_ALT,SAL_DT_DES,SAL_MOT_DES,SAL_SOM")] TB_SALA tB_SALA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_SALA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_SALA.EMP_CD_ANCINE);
            return View(tB_SALA);
        }

        // GET: SALA/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SALA tB_SALA = db.TB_SALA.Find(id);
            if (tB_SALA == null)
            {
                return HttpNotFound();
            }
            return View(tB_SALA);
        }

        // POST: SALA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TB_SALA tB_SALA = db.TB_SALA.Find(id);
            db.TB_SALA.Remove(tB_SALA);
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

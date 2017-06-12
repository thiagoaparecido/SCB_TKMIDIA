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
    public class BILHETERIAController : Controller
    {
        private SCB_SPCINEEntities db = new SCB_SPCINEEntities();

        // GET: BILHETERIA
        public ActionResult Index()
        {
            var tB_BILHETERIA = db.TB_BILHETERIA.Include(t => t.TB_EMPRESA_COMPLEXO).Include(t => t.TB_SALA);
            return View(tB_BILHETERIA.ToList());
        }

        // GET: BILHETERIA/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_BILHETERIA tB_BILHETERIA = db.TB_BILHETERIA.Find(id);
            if (tB_BILHETERIA == null)
            {
                return HttpNotFound();
            }
            return View(tB_BILHETERIA);
        }

        // GET: BILHETERIA/Create
        public ActionResult Create()
        {
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL");
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE");
            return View();
        }

        // POST: BILHETERIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BIL_ID,EMP_CD_ANCINE,SAL_CD_ANCINE,BIL_DIA_CIN,BIL_HOUVE_SES,BIL_RETIF,BIL_PROT,BIL_ADIMP_SALA,BIL_STATUS_PROT,BIL_DT_ALT_ADIMP,BIL_DT_ALT_STAT_PROT")] TB_BILHETERIA tB_BILHETERIA)
        {
            if (ModelState.IsValid)
            {
                db.TB_BILHETERIA.Add(tB_BILHETERIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_BILHETERIA.EMP_CD_ANCINE);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_BILHETERIA.SAL_CD_ANCINE);
            return View(tB_BILHETERIA);
        }

        // GET: BILHETERIA/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_BILHETERIA tB_BILHETERIA = db.TB_BILHETERIA.Find(id);
            if (tB_BILHETERIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_BILHETERIA.EMP_CD_ANCINE);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_BILHETERIA.SAL_CD_ANCINE);
            return View(tB_BILHETERIA);
        }

        // POST: BILHETERIA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BIL_ID,EMP_CD_ANCINE,SAL_CD_ANCINE,BIL_DIA_CIN,BIL_HOUVE_SES,BIL_RETIF,BIL_PROT,BIL_ADIMP_SALA,BIL_STATUS_PROT,BIL_DT_ALT_ADIMP,BIL_DT_ALT_STAT_PROT")] TB_BILHETERIA tB_BILHETERIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_BILHETERIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL", tB_BILHETERIA.EMP_CD_ANCINE);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_BILHETERIA.SAL_CD_ANCINE);
            return View(tB_BILHETERIA);
        }

        // GET: BILHETERIA/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_BILHETERIA tB_BILHETERIA = db.TB_BILHETERIA.Find(id);
            if (tB_BILHETERIA == null)
            {
                return HttpNotFound();
            }
            return View(tB_BILHETERIA);
        }

        // POST: BILHETERIA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TB_BILHETERIA tB_BILHETERIA = db.TB_BILHETERIA.Find(id);
            db.TB_BILHETERIA.Remove(tB_BILHETERIA);
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

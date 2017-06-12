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
    public class FILMEController : Controller
    {
        private SCB_SPCINEEntities db = new SCB_SPCINEEntities();

        // GET: FILME
        public ActionResult Index()
        {
            var tB_FILME = db.TB_FILME.Include(t => t.TB_DISTRIBUIDORA);
            return View(tB_FILME.ToList());
        }

        // GET: FILME/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FILME tB_FILME = db.TB_FILME.Find(id);
            if (tB_FILME == null)
            {
                return HttpNotFound();
            }
            return View(tB_FILME);
        }

        // GET: FILME/Create
        public ActionResult Create()
        {
            ViewBag.DIS_CNPJ = new SelectList(db.TB_DISTRIBUIDORA, "DIS_CNPJ", "DIS_NM");
            return View();
        }

        // POST: FILME/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FIL_CD_ANCINE,DIS_CNPJ,FIL_NM,FIL_ID_NACIO,FIL_DIGITAL,FIL_GENERO,FIL_CLASS_ETA,FIL_DIRETOR,FIL_SINOPSE,FIL_DT_INICIO,FIL_DT_FIM,FIL_ANO_LANC,FIL_PAIS,FIL_CD_AUX,FIL_DT_INC,FIL_DT_ALT,FIL_DT_DES,FIL_MOT_DES")] TB_FILME tB_FILME)
        {
            if (ModelState.IsValid)
            {
                db.TB_FILME.Add(tB_FILME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DIS_CNPJ = new SelectList(db.TB_DISTRIBUIDORA, "DIS_CNPJ", "DIS_NM", tB_FILME.DIS_CNPJ);
            return View(tB_FILME);
        }

        // GET: FILME/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FILME tB_FILME = db.TB_FILME.Find(id);
            if (tB_FILME == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIS_CNPJ = new SelectList(db.TB_DISTRIBUIDORA, "DIS_CNPJ", "DIS_NM", tB_FILME.DIS_CNPJ);
            return View(tB_FILME);
        }

        // POST: FILME/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FIL_CD_ANCINE,DIS_CNPJ,FIL_NM,FIL_ID_NACIO,FIL_DIGITAL,FIL_GENERO,FIL_CLASS_ETA,FIL_DIRETOR,FIL_SINOPSE,FIL_DT_INICIO,FIL_DT_FIM,FIL_ANO_LANC,FIL_PAIS,FIL_CD_AUX,FIL_DT_INC,FIL_DT_ALT,FIL_DT_DES,FIL_MOT_DES")] TB_FILME tB_FILME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_FILME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIS_CNPJ = new SelectList(db.TB_DISTRIBUIDORA, "DIS_CNPJ", "DIS_NM", tB_FILME.DIS_CNPJ);
            return View(tB_FILME);
        }

        // GET: FILME/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FILME tB_FILME = db.TB_FILME.Find(id);
            if (tB_FILME == null)
            {
                return HttpNotFound();
            }
            return View(tB_FILME);
        }

        // POST: FILME/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TB_FILME tB_FILME = db.TB_FILME.Find(id);
            db.TB_FILME.Remove(tB_FILME);
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

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
    [Authorize]
    public class MENSAGEM_ANCINEController : Controller
    {
        private SCBEntities db = new SCBEntities();

        // GET: MENSAGEM_ANCINE
        public ActionResult Index(long BIL_ID)
        {
            //var tB_MENSAGEM_ANCINE = db.TB_MENSAGEM_ANCINE.Include(t => t.TB_BILHETERIA).Include(t => t.TB_SALA).Include(t => t.TB_SESSAO_ANCINE).Where(T => T.BIL_ID == BIL_ID).OrderByDescending(t => t.MSA_DT_HORA_MSG);

            var tB_MENSAGEM_ANCINE = from m in db.TB_MENSAGEM_ANCINE
                                     where m.BIL_ID == BIL_ID
                                     orderby m.MSA_DT_HORA_MSG descending
                                     select m;

            return View(tB_MENSAGEM_ANCINE);
        }

        // GET: MENSAGEM_ANCINE/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE = db.TB_MENSAGEM_ANCINE.Find(id);
            if (tB_MENSAGEM_ANCINE == null)
            {
                return HttpNotFound();
            }
            return View(tB_MENSAGEM_ANCINE);
        }

        // GET: MENSAGEM_ANCINE/Create
        public ActionResult Create()
        {
            ViewBag.BIL_ID = new SelectList(db.TB_BILHETERIA, "BIL_ID", "EMP_CD_ANCINE");
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE");
            ViewBag.SEA_ID = new SelectList(db.TB_SESSAO_ANCINE, "SEA_ID", "SAL_CD_ANCINE");
            return View();
        }

        // POST: MENSAGEM_ANCINE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MSA_DT_MSG,MSA_DT_HORA_MSG,BIL_ID,SAL_CD_ANCINE,SEA_ID,SEA_DT_HR_INICIO,MSA_TP_MSG,MSA_CD_MSG,MSA_TXT_MSG")] TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE)
        {
            if (ModelState.IsValid)
            {
                db.TB_MENSAGEM_ANCINE.Add(tB_MENSAGEM_ANCINE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BIL_ID = new SelectList(db.TB_BILHETERIA, "BIL_ID", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.BIL_ID);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.SAL_CD_ANCINE);
            ViewBag.SEA_ID = new SelectList(db.TB_SESSAO_ANCINE, "SEA_ID", "SAL_CD_ANCINE", tB_MENSAGEM_ANCINE.SEA_ID);
            return View(tB_MENSAGEM_ANCINE);
        }

        // GET: MENSAGEM_ANCINE/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE = db.TB_MENSAGEM_ANCINE.Find(id);
            if (tB_MENSAGEM_ANCINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.BIL_ID = new SelectList(db.TB_BILHETERIA, "BIL_ID", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.BIL_ID);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.SAL_CD_ANCINE);
            ViewBag.SEA_ID = new SelectList(db.TB_SESSAO_ANCINE, "SEA_ID", "SAL_CD_ANCINE", tB_MENSAGEM_ANCINE.SEA_ID);
            return View(tB_MENSAGEM_ANCINE);
        }

        // POST: MENSAGEM_ANCINE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MSA_DT_MSG,MSA_DT_HORA_MSG,BIL_ID,SAL_CD_ANCINE,SEA_ID,SEA_DT_HR_INICIO,MSA_TP_MSG,MSA_CD_MSG,MSA_TXT_MSG")] TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_MENSAGEM_ANCINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BIL_ID = new SelectList(db.TB_BILHETERIA, "BIL_ID", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.BIL_ID);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA, "SAL_CD_ANCINE", "EMP_CD_ANCINE", tB_MENSAGEM_ANCINE.SAL_CD_ANCINE);
            ViewBag.SEA_ID = new SelectList(db.TB_SESSAO_ANCINE, "SEA_ID", "SAL_CD_ANCINE", tB_MENSAGEM_ANCINE.SEA_ID);
            return View(tB_MENSAGEM_ANCINE);
        }

        // GET: MENSAGEM_ANCINE/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE = db.TB_MENSAGEM_ANCINE.Find(id);
            if (tB_MENSAGEM_ANCINE == null)
            {
                return HttpNotFound();
            }
            return View(tB_MENSAGEM_ANCINE);
        }

        // POST: MENSAGEM_ANCINE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            TB_MENSAGEM_ANCINE tB_MENSAGEM_ANCINE = db.TB_MENSAGEM_ANCINE.Find(id);
            db.TB_MENSAGEM_ANCINE.Remove(tB_MENSAGEM_ANCINE);
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

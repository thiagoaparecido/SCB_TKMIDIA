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
    public class EMPRESA_COMPLEXOController : Controller
    {
        private SCB_SPCINEEntities db = new SCB_SPCINEEntities();

        // GET: EMPRESA_COMPLEXO
        public ActionResult Index()
        {
            return View(db.TB_EMPRESA_COMPLEXO.ToList());
        }

        // GET: EMPRESA_COMPLEXO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO = db.TB_EMPRESA_COMPLEXO.Find(id);
            if (tB_EMPRESA_COMPLEXO == null)
            {
                return HttpNotFound();
            }
            return View(tB_EMPRESA_COMPLEXO);
        }

        // GET: EMPRESA_COMPLEXO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EMPRESA_COMPLEXO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMP_CD_ANCINE,EMP_RZ_SOCIAL,EMP_NM_FANT,EMP_CNPJ,EMP_INSCRICAO,EMP_END,EMP_NUM_END,EMP_CMP_END,EMP_BRR_END,EMP_CID_END,EMP_TEL_DDD,EMP_TEL,EMP_UF_END,EMP_CEP_END,EMP_CD_AUX,EMP_DT_INC,EMP_DT_ALT,EMP_DT_DES,EMP_MOT_DES")] TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO)
        {
            if (ModelState.IsValid)
            {
                db.TB_EMPRESA_COMPLEXO.Add(tB_EMPRESA_COMPLEXO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_EMPRESA_COMPLEXO);
        }

        // GET: EMPRESA_COMPLEXO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO = db.TB_EMPRESA_COMPLEXO.Find(id);
            if (tB_EMPRESA_COMPLEXO == null)
            {
                return HttpNotFound();
            }
            return View(tB_EMPRESA_COMPLEXO);
        }

        // POST: EMPRESA_COMPLEXO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMP_CD_ANCINE,EMP_RZ_SOCIAL,EMP_NM_FANT,EMP_CNPJ,EMP_INSCRICAO,EMP_END,EMP_NUM_END,EMP_CMP_END,EMP_BRR_END,EMP_CID_END,EMP_TEL_DDD,EMP_TEL,EMP_UF_END,EMP_CEP_END,EMP_CD_AUX,EMP_DT_INC,EMP_DT_ALT,EMP_DT_DES,EMP_MOT_DES")] TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_EMPRESA_COMPLEXO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_EMPRESA_COMPLEXO);
        }

        // GET: EMPRESA_COMPLEXO/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO = db.TB_EMPRESA_COMPLEXO.Find(id);
            if (tB_EMPRESA_COMPLEXO == null)
            {
                return HttpNotFound();
            }
            return View(tB_EMPRESA_COMPLEXO);
        }

        // POST: EMPRESA_COMPLEXO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TB_EMPRESA_COMPLEXO tB_EMPRESA_COMPLEXO = db.TB_EMPRESA_COMPLEXO.Find(id);
            db.TB_EMPRESA_COMPLEXO.Remove(tB_EMPRESA_COMPLEXO);
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

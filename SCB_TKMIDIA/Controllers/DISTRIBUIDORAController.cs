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
    public class DISTRIBUIDORAController : Controller
    {
        private SCBEntities db = new SCBEntities();

        // GET: DISTRIBUIDORA
        public ActionResult Index()
        {
            return View(db.TB_DISTRIBUIDORA.ToList());
        }

        // GET: DISTRIBUIDORA/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_DISTRIBUIDORA tB_DISTRIBUIDORA = db.TB_DISTRIBUIDORA.Find(id);
            if (tB_DISTRIBUIDORA == null)
            {
                return HttpNotFound();
            }
            return View(tB_DISTRIBUIDORA);
        }

        // GET: DISTRIBUIDORA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DISTRIBUIDORA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DIS_CNPJ,DIS_NM,DIS_CD_AUX,DIS_DT_INC,DIS_DT_ALT,DIS_DT_DES,DIS_MOT_DES")] TB_DISTRIBUIDORA tB_DISTRIBUIDORA)
        {
            if (ModelState.IsValid)
            {
                db.TB_DISTRIBUIDORA.Add(tB_DISTRIBUIDORA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_DISTRIBUIDORA);
        }

        // GET: DISTRIBUIDORA/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_DISTRIBUIDORA tB_DISTRIBUIDORA = db.TB_DISTRIBUIDORA.Find(id);
            if (tB_DISTRIBUIDORA == null)
            {
                return HttpNotFound();
            }
            return View(tB_DISTRIBUIDORA);
        }

        // POST: DISTRIBUIDORA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DIS_CNPJ,DIS_NM,DIS_CD_AUX,DIS_DT_INC,DIS_DT_ALT,DIS_DT_DES,DIS_MOT_DES")] TB_DISTRIBUIDORA tB_DISTRIBUIDORA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_DISTRIBUIDORA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_DISTRIBUIDORA);
        }

        // GET: DISTRIBUIDORA/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_DISTRIBUIDORA tB_DISTRIBUIDORA = db.TB_DISTRIBUIDORA.Find(id);
            if (tB_DISTRIBUIDORA == null)
            {
                return HttpNotFound();
            }
            return View(tB_DISTRIBUIDORA);
        }

        // POST: DISTRIBUIDORA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TB_DISTRIBUIDORA tB_DISTRIBUIDORA = db.TB_DISTRIBUIDORA.Find(id);
            db.TB_DISTRIBUIDORA.Remove(tB_DISTRIBUIDORA);
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

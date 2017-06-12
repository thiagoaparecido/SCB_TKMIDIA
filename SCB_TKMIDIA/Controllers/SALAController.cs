using SCBIntegration;
using SCBIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class SALAController : Controller
    {
        private SCBEntities db = new SCBEntities();

        // GET: TB_SALA
        public ActionResult Index(DateTime? bil_dia_cin)
        {
            DateTime dtAux;
            List<Adimplencia> modelSala = new List<Adimplencia>();

            if (bil_dia_cin == null)
            {
                if (ViewBag.bil_dia_cin == null)
                {
                    var bil_exists = from bil in db.TB_BILHETERIA
                                     where bil.BIL_ID != 0
                                     select bil;

                    if (bil_exists.Any())
                    {
                        bil_dia_cin = db.TB_BILHETERIA.Max(max => max.BIL_DIA_CIN);
                    }
                    else
                    {
                        bil_dia_cin = DateTime.Today;
                    }
                }
                else
                {
                    bil_dia_cin = ViewBag.bil_dia_cin;
                }

            }

            dtAux = Convert.ToDateTime(bil_dia_cin);
            ViewBag.bil_dia_cin = dtAux.ToShortDateString();

            string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
            string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

            SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);
            AdimplenciaExibidor objReturn = objSCBIntegrationManager.ConsultaSituacaoAdimplencia(dtAux);


            if (objReturn != null)
            {

                if (objReturn.adimplenciaSalas != null && objReturn.adimplenciaSalas.Count() > 0)
                {

                    var listaSalas = (from sl in objReturn.adimplenciaSalas
                                      orderby sl.situacaoSala
                                      select new Adimplencia()
                                      {
                                          SAL_CD_ANCINE = sl.registroANCINESala.ToString(),
                                          BIL_ADIMP_SALA = sl.situacaoSala,
                                          SAL_DESC = GetSalaDesc(sl.registroANCINESala.ToString()),
                                      }).ToList();

                    return View(listaSalas);
                }

            }
            
            return View();
        }

        public string GetSalaDesc(string sal_cd_ancine)
        {
            TB_SALA sal_desc = db.TB_SALA.Find(sal_cd_ancine);

            if(sal_desc != null )
            {
                return sal_desc.SAL_DESC;
            }

            return "";
        }

        // GET: TB_SALA/Details/5
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

        // GET: TB_SALA/Create
        public ActionResult Create()
        {
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_RZ_SOCIAL");
            return View();
        }

        // POST: TB_SALA/Create
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

        // GET: TB_SALA/Edit/5
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

        // POST: TB_SALA/Edit/5
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

        // GET: TB_SALA/Delete/5
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

        // POST: TB_SALA/Delete/5
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

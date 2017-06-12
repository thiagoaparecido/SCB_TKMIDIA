using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCB_TKMIDIA.Models;
using PagedList;

namespace SCB_TKMIDIA.Controllers
{
    [Authorize]
    public class FILMEController : Controller
    {
        private SCBEntities db = new SCBEntities();

        public ActionResult Index(string sortOrder ,string currentFilter ,string searchString ,int? page ,DateTime? CurrentDtSearch ,DateTime? DtSearch)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.InclusaoSortParm = sortOrder == "inclusao" ? "inclusao_desc" : "inclusao";
            ViewBag.FilmeSortParm = string.IsNullOrEmpty(sortOrder) ? "filme" : "";
            ViewBag.DistrSortParm = string.IsNullOrEmpty(sortOrder) ? "distr" : "";
            ViewBag.NacSortParm = string.IsNullOrEmpty(sortOrder) ? "nac" : "";

            if (searchString == null)
            {
                searchString = currentFilter;
            }

            if (!DtSearch.HasValue)
            {
                DtSearch = CurrentDtSearch;
            }

            //Atualiza a busca atual para a próxima página (se houver)
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentDtSearch = DtSearch;

            var listaFilme = from s in db.TB_FILME
                             where s.FIL_DT_DES == null
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                listaFilme = listaFilme.Where(s => s.FIL_NM.ToUpper().Contains(searchString.ToUpper()) || s.TB_DISTRIBUIDORA.DIS_NM.ToUpper().Contains(searchString.ToUpper()) || s.FIL_ID_NACIO.ToUpper().Contains(searchString.ToUpper())).Include(t => t.TB_DISTRIBUIDORA).OrderBy(t => t.FIL_NM);
            }

            if (DtSearch.HasValue)
            {
                listaFilme = listaFilme.Where(s => s.FIL_DT_INC >= DtSearch).Include(t => t.TB_DISTRIBUIDORA).OrderBy(t => t.FIL_NM);
            }

            switch (sortOrder)
            {
                case "inclusao":
                listaFilme = listaFilme.OrderBy(s => s.FIL_DT_INC);
                break;
                case "inclusao_desc":
                listaFilme = listaFilme.OrderByDescending(s => s.FIL_DT_INC);
                break;
                case "distr":
                listaFilme = listaFilme.OrderBy(s => s.TB_DISTRIBUIDORA.DIS_NM);
                break;
                case "nac":
                listaFilme = listaFilme.OrderBy(s => s.FIL_ID_NACIO);
                break;
                case "filme":
                listaFilme = listaFilme.OrderBy(s => s.FIL_NM);
                break;
                default:
                listaFilme = listaFilme.OrderBy(s => s.FIL_NM);
                break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(listaFilme.ToPagedList(pageNumber ,pageSize));
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
        public ActionResult Create(FormCollection collection)
        {
            string inlineRadioOptions = "";
            string FIL_COD_GEN = "";
            Random rdmCodGen = new Random();
            int iCodGen=0;
            string strAux="";
            int itamanho = 0;

            TB_FILME TB_FIL = new TB_FILME();

            foreach (var key in collection.AllKeys)
            {
                var value = collection[key];

                if (key == "FIL_CD_ANCINE") { TB_FIL.FIL_CD_ANCINE = value; }
                if (key == "FIL_COD_GEN") { FIL_COD_GEN = value; }
                if (key == "inlineRadioOptions") { inlineRadioOptions = value; }
                if (key == "DIS_CNPJ") { TB_FIL.DIS_CNPJ = Convert.ToInt64(value); }
                if (key == "FIL_NM") { TB_FIL.FIL_NM = value; }
                if (key == "FIL_ID_NACIO") { TB_FIL.FIL_ID_NACIO = value; }
                if (key == "FIL_DT_INC") { TB_FIL.FIL_DT_INC = Convert.ToDateTime(value); }
            }

            if (inlineRadioOptions == "2")
            {
                iCodGen = rdmCodGen.Next();
                strAux = iCodGen.ToString();
                itamanho = strAux.Length;

                if (itamanho > 11)
                {
                    itamanho = 11;
                }

                TB_FIL.FIL_CD_ANCINE = FIL_COD_GEN + "|" + strAux.Substring(0 ,itamanho);
            }


            db.TB_FILME.Add(TB_FIL);
            db.SaveChanges();

            return RedirectToAction("Index");
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

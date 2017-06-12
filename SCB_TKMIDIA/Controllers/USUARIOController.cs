using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCB_TKMIDIA.Models;
using System.Web.Security;

namespace SCB_TKMIDIA.Controllers
{
    public class USUARIOController : Controller
    {
        private SCBEntities db = new SCBEntities();

        [AllowAnonymous]
        public ActionResult Logoff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new TB_USUARIO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(TB_USUARIO login ,string returnUrl ,string USU_LOGIN ,string USU_SENHA)
        {
            var vLogin = db.TB_USUARIO.Where(p => p.USU_LOGIN.Equals(login.USU_LOGIN)).FirstOrDefault();

            if (vLogin != null)
            {

                if (Equals(vLogin.USU_SENHA.Trim() ,login.USU_SENHA.Trim()))
                {
                    FormsAuthentication.SetAuthCookie(vLogin.USU_LOGIN ,false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    Session["Nome"] = vLogin.USU_NM;
                    Console.Write(Session.SessionID);

                    return RedirectToAction("Index" ,"Home");

                }
                else
                {
                    ModelState.AddModelError("" ,"Usuário sem acesso ao sistema!");
                    return View(new TB_USUARIO());
                }
            }
            else
            {
                ModelState.AddModelError("" ,"Usuário informado inválido!");
                return View(new TB_USUARIO());
            }

            return View(login);
        }
        // GET: TB_USUARIO
        public ActionResult Index()
        {
            return View(db.TB_USUARIO.ToList());
        }

        // GET: TB_USUARIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USUARIO USUARIO = db.TB_USUARIO.Find(id);
            if (USUARIO == null)
            {
                return HttpNotFound();
            }
            return View(USUARIO);
        }

        // GET: TB_USUARIO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_USUARIO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USU_CD,USU_NM,USU_LOGIN,USU_SENHA,USU_DT_INC,USU_DT_ALT,USU_DT_DES,USU_MOT_DES")] TB_USUARIO USUARIO)
        {
            if (ModelState.IsValid)
            {
                db.TB_USUARIO.Add(USUARIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(USUARIO);
        }

        // GET: TB_USUARIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USUARIO USUARIO = db.TB_USUARIO.Find(id);
            if (USUARIO == null)
            {
                return HttpNotFound();
            }
            return View(USUARIO);
        }

        // POST: TB_USUARIO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USU_CD,USU_NM,USU_LOGIN,USU_SENHA,USU_DT_INC,USU_DT_ALT,USU_DT_DES,USU_MOT_DES")] TB_USUARIO USUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(USUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(USUARIO);
        }

        // GET: TB_USUARIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USUARIO USUARIO = db.TB_USUARIO.Find(id);
            if (USUARIO == null)
            {
                return HttpNotFound();
            }
            return View(USUARIO);
        }

        // POST: TB_USUARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_USUARIO USUARIO = db.TB_USUARIO.Find(id);
            db.TB_USUARIO.Remove(USUARIO);
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

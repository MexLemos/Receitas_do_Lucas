using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using receitando.Models;
using System.Web.Security;

namespace receitando.Controllers
{
    public class UsuarioController : Controller
    {
        private ReceitasDBEntities db = new ReceitasDBEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.TipoUsuario);
            return View(usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            
            ViewBag.Tipo = new SelectList(db.TipoUsuario, "Id", "Nome");

            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (db.Usuario.Any(x => x.email == usuario.email))
                {
                    ModelState.AddModelError("", "Este Email já se encontra Cadastrado!!");
                }
                else
                {
                usuario.Status = "Activo";
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                
            }

            ViewBag.Tipo = new SelectList(db.TipoUsuario, "Id", "Nome", usuario.Tipo);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            var status = new List<string>() { "Activo", "Inactivo" };
            ViewBag.Estado = status;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo = new SelectList(db.TipoUsuario, "Id", "Nome", usuario.Tipo);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Senha,Tipo,Status,email,Contacto")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo = new SelectList(db.TipoUsuario, "Id", "Nome", usuario.Tipo);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if ((db.Usuario.Any(x => x.email == user.email)) && (db.Usuario.Any(x => x.Senha == user.Senha)))
                    {
                        if((db.Usuario.Any(x => x.TipoUsuario.Nome == "Admin")))
                        return RedirectToRoute(new { controller = "Home", action = "Menu" });
                        else
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro ao fazer Login! Verifique seu email e senha!");
                       
                    }
                    

                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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

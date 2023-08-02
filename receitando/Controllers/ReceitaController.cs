using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using receitando.Models;

namespace receitando.Controllers
{
    public class ReceitaController : Controller
    {
        private ReceitasDBEntities db = new ReceitasDBEntities();

        // GET: Receita
        public ActionResult Index()
        {
            var receita = db.Receita.Include(r => r.Categoria1);
            return View(receita.ToList());
        }

        // GET: Receita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita rec = new Receita();
            rec = db.Receita.Where(c => c.Id == id).SingleOrDefault();
            List<Comentarios> list_comentarios = new List<Comentarios>();
            list_comentarios = db.Comentarios.Where(a => a.ReceitaId == id).ToList();
            ReceitaDetalhes model = new ReceitaDetalhes();
            model.receita = rec;
            model.comentario = list_comentarios;
            if (rec == null)
            {
                return HttpNotFound();
            }
            
           return View(model);

        }

        // GET: Receita/Create
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.Categoria, "Id", "Descricao");
            return View();
        }

        // POST: Receita/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Categoria,Ingredientes,Descricao,Imagem,Video,Status,Sugestoes,dataPublicacao")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                db.Receita.Add(receita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.Categoria, "Id", "Descricao", receita.Categoria);
            return View(receita);
        }

        // GET: Receita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receita.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.Categoria, "Id", "Descricao", receita.Categoria);
            return View(receita);
        }

        // POST: Receita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Categoria,Ingredientes,Descricao,Imagem,Video,Status,Sugestoes,dataPublicacao")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.Categoria, "Id", "Descricao", receita.Categoria);
            return View(receita);
        }

        // GET: Receita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receita.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // POST: Receita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receita receita = db.Receita.Find(id);
            db.Receita.Remove(receita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comentario(Comentarios comment, int id)
        {
            if (ModelState.IsValid)
            {
                Comentarios cmt = new Comentarios();
                cmt = comment;
                
                db.Comentarios.Add(cmt);
                db.SaveChanges();                                                              
             }

          
            else
            {
               ModelState.AddModelError("", "Nenhum ficheiro foi selecionado!");
             }
            return RedirectToRoute(new { controller = "Receita", action = "Details", id = id });

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

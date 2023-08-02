using receitando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace receitando.Controllers
{
    public class HomeController : Controller
    {
        private ReceitasDBEntities db = new ReceitasDBEntities();

        // GET: Categoria
        public ActionResult Index()
        {
            return View(db.Categoria.ToList());
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        { 

            return View();
        }
        public ActionResult Menu()
        {

            return View();
        }
    }
}
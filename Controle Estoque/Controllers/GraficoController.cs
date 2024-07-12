using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_Estoque.Controllers
{
    public class GraficoController : Controller
    {
        // GET: Relatorio
        public ActionResult PerdaMes()
        {
            return View();
        }
        public ActionResult EntradaSaidaMesa()
        {
            return View();
        }
    }
}
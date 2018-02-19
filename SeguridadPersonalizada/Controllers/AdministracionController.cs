using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeguridadPersonalizada.Atributos;

namespace SeguridadPersonalizada.Controllers
{
    [AutorizacionAdmin]
    public class AdministracionController : Controller
    {
        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }
    }
}
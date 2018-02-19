using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeguridadPersonalizada.Controllers
{
    public class ValidacionController : Controller
    {
        // GET: Validacion
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String usuario,String password)
        {
            return View();
        }

    }
}
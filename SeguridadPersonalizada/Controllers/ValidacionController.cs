using SeguridadPersonalizada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            ValidateUsers validacion = new ValidateUsers();
            if (validacion.ExisteUsuario(usuario, password))
            {
                //si existe, debemos crearnos un ticket
                //en el incluiremos el rol del usuario(opcional) ticket = cookie encriptada
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                    (1 //version del ticket
                    , usuario //nombre del ticket
                    , DateTime.Now //momento de creacion del ticket
                    , DateTime.Now.AddSeconds(4)//tiempo de expiracion
                    , true //persistencia del ticket poner a true
                    , validacion.Role //informacion extra del usuario, puedo guardar lo que quiera(String)
                    , FormsAuthentication.FormsCookiePath //ruta del ticket/cookie
                    );
                //todo ticket debe estar cifrado obligatoriamente.
                String ticketcifrado = FormsAuthentication.Encrypt(ticket);
                //los tickets/cookies debemos almacenarlos en el conjunto de cookies del cliente
                //creamos cookie con nombre y valor
                HttpCookie cookie = new HttpCookie("cookieusuario", ticketcifrado);
                Response.Cookies.Add(cookie);//almacenada
                //llevamos al usuario a la zona segura
                return RedirectToAction("Index", "Administracion");
            }
            else
            {
                ViewBag.Mensaje = "Usuario/pw incorrectos";
            }
            return View();
        }
        public ActionResult ErrorAcceso()
        {
            return View();
        }

    }
}
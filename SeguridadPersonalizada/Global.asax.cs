using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;

namespace SeguridadPersonalizada
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //Metodo que se ejecuta en el servidor con cada intento de validacion de usuario
        public void Application_PostAuthenticateRequest(Object sender,EventArgs e)
        {
            //en este metodo debemos crear un usuario en la sesion. 
            //solamente crearemos usuarios si tenemos ticket. 
            //recuperamos las cookies de la seguridad
            HttpCookie cookie = Request.Cookies["cookieusuario"];
            if(cookie!= null)
            {
                //tenemos ticket , podemos crear el usuario
                //recuperamos los datos encriptados de la cookie
                String datoscookie = cookie.Value;
                //con ello ahora recuperamos el ticket interno de dicha cookie
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(datoscookie);
                //a partir de este ticket podemos hacer lo que necesitemos para la seguridad
                //por ej; crear un nuevo usuario(clase)
                //lo que deseemos almacenar nosotros en nuestra session de usuario- 
                //como no estamos personalizando usuarios vamos a coger a un usuario principal generico
                //cualquier usuario esta compuesto por dos caracteristicas:
                //1. una identidad(nombre del usuario)
                //2. grupos ROLES[] (Array con los grupos del usuario)
                String nombreusuario = ticket.Name;
                String grupousuario = ticket.UserData;
                //con este grupo creamos la identidad del principal
                GenericIdentity identidad = new GenericIdentity(nombreusuario);
                //creamos el usuario principal 
                GenericPrincipal usuario = new GenericPrincipal(identidad, new string[] { grupousuario });
                //almacenamos el usuario principal en la sesion
                HttpContext.Current.User = usuario;

            }
        }
    }
}

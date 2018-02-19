using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;

namespace SeguridadPersonalizada.Atributos
{
    public class AutorizacionAdminAttribute: AuthorizeAttribute //: AuthorizeAttribute hago que herede de aqui
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //por defecto este metodo deja acceso a la peticion que se haya realizado
            //nosotros debemos indicar que lo enviaremos a otro action y controller
            //si no nos gusta el acceso(Routing)
            //debemos preguntar si existe un usuario ya validado
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                //Si el usuario se ha validado, es que ha pasado por el ticket y por la sesion- 
                //si queremos validar usuarios por roles debemos extraer de la sesion al usuario principal 
                GenericPrincipal usuario =
                    HttpContext.Current.User as GenericPrincipal;
                //ya podemos validar por usuario
                //podriamos hacerlo por nombre
                //ej 1. if()usuario.Identity.Name == "quien sea..")
                //2. por su role(grupo) 
                //if (usuario.IsInRole("GRUPO"))..
                //QUEREMOS QUE ENTRE SOLO EL ADMIN
                if (usuario.IsInRole("ADMINISTRADOR") == false)
                {
                    //SI NO LO ES LE ENVIAMOS A OTRO SITIO(ErrorAcceso)
                    RouteValueDictionary rutaacceso =
                        new RouteValueDictionary(new
                        {
                            controller = "Validacion",
                            action = "ErrorAcceso"
                        });
                    RedirectToRouteResult direccionacceso =
                        new RedirectToRouteResult(rutaacceso);
                    filterContext.Result = direccionacceso;

                }

            }
            else
            {
                //el usuario no se ha validado todavía y hacemos un routing hacia el login. 
                //para hacer un routing(interceptar la peticion ) necesitamos crear la CLASE: 
                // RouteValueDiccionary con Controller y Action donde deseamos enviar la nueva peticion
                RouteValueDictionary ruta =
                    new RouteValueDictionary(new
                    {
                        controller = "Validacion",
                        action = "Login"
                    }
                    );
                //mediante la clase RedirectToRouteResult indicamos la propia redireccion con la ruta. 
                RedirectToRouteResult direccionlogin =
                    new RedirectToRouteResult(ruta);
                //mediante el filtercontext, tenemos una propiedad result que nos permite redirigir 
                //a otras rutas. 
                filterContext.Result = direccionlogin;
            }
        }

    }
}
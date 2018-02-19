using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeguridadPersonalizada.Atributos
{
    public class AutorizacionAdminAttribute: AuthorizeAttribute //: AuthorizeAttribute hago que herede de aqui
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

    }
}
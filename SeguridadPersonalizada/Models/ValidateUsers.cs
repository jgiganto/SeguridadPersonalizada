using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadPersonalizada.Models
{
    public class ValidateUsers
    {
        //propiedad para devolver el grupo del usuario que se esta dando de alta
        public String Role { get; set; }
        //metodo para validar si existen los usuarios por su usuario y password 
        public bool ExisteUsuario(String usuario, String password)
        {
            if(usuario.ToUpper()=="ADMIN" && password.ToUpper() == "ADMIN")
            {
                //LE ASIGNAMOS EL ROLE DE ADMINISTRADOR PARA LAS POSTERIORES VALIDACIONES
                this.Role = "ADMINISTRADOR";
                return true;
            }
            else if(usuario.ToUpper()=="USER" && password.ToUpper() == "USER")
            {
                this.Role = "USUARIO"; //OTRO ROLE PARA ESTE USUARIO
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
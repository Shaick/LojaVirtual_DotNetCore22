using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Library.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Library.Filtro
{
    public class ClienteAuthorization : Attribute, IAuthorizationFilter
    {
        LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _loginCliente = (LoginCliente) context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();

            if(cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso Negado!" };
            }
            
        }
    }
}

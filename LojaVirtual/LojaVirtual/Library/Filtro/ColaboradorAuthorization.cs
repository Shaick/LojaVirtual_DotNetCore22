using LojaVirtual.Library.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LojaVirtual.Models;

namespace LojaVirtual.Library.Filtro
{
    
    public class ColaboradorAuthorization : Attribute, IAuthorizationFilter
    {
        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Colaborador colaborador = _loginColaborador.GetColaborador();

            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }


    }
}
}

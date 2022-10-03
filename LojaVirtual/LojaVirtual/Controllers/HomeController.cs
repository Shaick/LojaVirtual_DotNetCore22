using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Library.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            Contato contato = new Contato();
            try
            {
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if(isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_S"] = "Mensagem de contato enviada com sucesso!";
                } else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage);
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["Contato"] = contato;
                }

                // return new ContentResult() { Content = string.Format("Dados Recebidos Com Sucesso <br> Nome: {0} <br> Email: {1} ,<br> Texto: {2}", contato.Nome, contato.Email, contato.Texto), ContentType="text/html" };
                
            } catch (Exception e)
            {
                ViewData["MSG_S"] = "Ops houve um erro! " + e.Message;

                //TODO - Implementar Log
            }
            

            

            return View("Contato");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Library.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LojaVirtual.Repositories;
using System.Text;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Library.Login;
using LojaVirtual.Library.Filtro;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsletterRepository _repositoryNewsletter;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository repositoryCliente, INewsletterRepository repositoryNewsletter, LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsletter = repositoryNewsletter;
            _loginCliente = loginCliente;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if(ModelState.IsValid)
            {
                _repositoryNewsletter.Cadastrar(newsletter);

                TempData["MSG_S"] = "E-mail cadastradi! Agora você recebe promoções especiais!";
                
                return RedirectToAction(nameof(Index));              

            }
            else
            {
                return View();
            }
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);

            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique E-mail/Senha";
               return View();
            }
        }

        [HttpGet]
        [ClinteAthorization]
        public IActionResult Painel()
        {
            Cliente cliente = _loginCliente.GetCliente();
            if( cliente != null)
            {
                return new ContentResult() { Content = "Usuário " + cliente.Id + " E-mail " + cliente.Email + " Logado! " };
            }
            else
            {
                return new ContentResult() { Content = "Acesso Negado!" };
            }            
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if(ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(cliente);          

                TempData["MSG_S"] = "Cadastro Realizado com Sucesso!";

                //TODO - Implementar redirecionamento diferentes (Painel, Carrinho de compras e etc).
                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
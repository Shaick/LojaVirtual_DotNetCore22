using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Database;
using LojaVirtual.Library.Filtro;
using LojaVirtual.Library.Login;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository _repositoryColaborador;
        private LoginColaborador _loginColaborador;
        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorDB != null)
            {
                _loginColaborador.Login(colaboradorDB);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique E-mail/Senha";
                return View();
            }
        }

        [ColaboradorAuthorization]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult CadastrarNovaSenha()
        {
            return View();
        }

        [ColaboradorAuthorization]
        public IActionResult Painel()
        {
            return View();
        }
    }
}
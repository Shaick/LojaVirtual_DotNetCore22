using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Visualizar()
        {
            Produto produto = GetProduto();

            return View(produto);
        }

        private Produto GetProduto()
        {
            return new Produto()
            {
                Id = 1,
                Nome = "Raspberry",
                Descricao = "Mini Pc",
                valor = 1500.00M
            };
        }
    }
}

using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var news = new NewsletterEmail() { Email = "ribeiro@gmail.com" };
            return View(news);
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            //TODO - Adição no banco de dados

            //TODO - Validações
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            Contato con = new Contato();

            con.Nome = HttpContext.Request.Form["nome"];
            con.Email = HttpContext.Request.Form["email"];
            con.Texto = HttpContext.Request.Form["texto"];

            var listaMensagens = new List<ValidationResult>();
            var contexto = new ValidationContext(con);
            var isvalid = Validator.TryValidateObject(con, contexto, listaMensagens, true);

            if (isvalid)
            {
                try
                {
                    ContatoEmail.EnviarContatoPorEmail(con);

                    ViewData["MSG_S"] = "Mensagem enviada com sucesso!";
                }
                catch (Exception e)
                {
                    ViewData["MSG_E"] = "Ocorreu um erro! Tente mais tarde.";

                    //TODO - Implementar mensagem de LOG.
                }
            } 
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var texto in listaMensagens)
                {
                    sb.Append(texto.ErrorMessage + "<br />");
                }

                ViewData["MSG_E"] = sb.ToString();

                ViewData["CONTATO"] = con;
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

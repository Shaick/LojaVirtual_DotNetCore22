using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace LojaVirtual.Library.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            //configurar smtp
            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("shaick@msn.com", "Novodenovo30"); /*Novodenovo30*/
            smtp.EnableSsl = true;

            //construir mensagem 
            string corpoMsg = string.Format("<h2>Contato - Loja Virtual</h2>" + 
                "<b>Nome: </b> {0} <br>" + 
                "<b>E-mail: </b> {1} <br>" + 
                "<b>Texto: </b> {2}" + 
                "<br>E-mail Enviador com Sucesso!", contato.Nome, contato.Email, contato.Texto);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("shaick@msn.com");
            mensagem.To.Add("wilsinho323@gmail.com");
            mensagem.Subject = "Contato Loja Virtual - Email: " +  contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //enviar por smtp
            smtp.Send(mensagem);
        }
    }
}
/* 
 * string corpoMsg = string.Format("<h2>Contato - Loja Virtual</h2>" + 
                "<b>Nome: </b> {0} <br>" + 
                "<b>E-mail: </b> {1} <br>" + 
                "<b>Texto: </b> {2}" + 
                "<br>E-mail Enviador com Sucesso!", contato.Nome, contato.Email, contato.Texto);

*/

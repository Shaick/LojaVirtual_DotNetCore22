using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Library.Lang;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Models
{
    public class NewsletterEmail
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }
    }
}

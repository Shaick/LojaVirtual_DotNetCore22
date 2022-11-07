using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaVirtual.Library.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Session.Session _session;

        public LoginCliente(Session.Session session)
        {
            _session = session;
        }

        public void Login(Cliente cliente)
        {
            //Serializar
            string clienteJs = JsonConvert.SerializeObject(cliente);
            _session.Cadastrar(Key, clienteJs);
        }

        public Cliente GetCliente()
        {
            //Deserializar
            if (_session.Existe(Key))
            {
                string clinteJS = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clinteJS);
            } else
            {
                return null;
            }

        }

        public void Logout()
        {
            _session.RemoverTodos();
        }
    }
}

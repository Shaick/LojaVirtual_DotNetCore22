using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Library.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Session.Session _session;

        public LoginColaborador(Session.Session session)
        {
            _session = session;
        }

        public void Login(Colaborador colaborador)
        {
            //Serializar
            string colaboradorJs = JsonConvert.SerializeObject(colaborador);
            _session.Cadastrar(Key, colaboradorJs);
        }

        public Colaborador GetColaborador()
        {
            //Deserializar
            if (_session.Existe(Key))
            {
                string colaboradorJS = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJS);
            }
            else
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

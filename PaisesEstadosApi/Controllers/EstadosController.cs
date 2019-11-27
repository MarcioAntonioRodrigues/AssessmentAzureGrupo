using Negocio;
using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaisesEstadosApi.Controllers
{
    public class EstadosController : ApiController
    {
        private DataContext _dataContext;

        public EstadosController()
        {
            _dataContext = new DataContext();
        }

        // GET: api/Estados
        public List<Estado> Get()
        {
            List<Estado> estados = new List<Estado>();
            foreach (var e in _dataContext.Estados)
            {
                estados.Add(e);
            }
            return estados;
        }

        // GET: api/Estados/5
        public Estado Get(int id)
        {
            Estado estadoSelecionado = _dataContext.Estados.Where(p => p.Id == id).FirstOrDefault();
            return estadoSelecionado;
        }

        // POST: api/Estados
        public void Post(Estado estado)
        {
            var state = new Estado()
            {
                Nome = estado.Nome,
                BandeiraUrl = estado.BandeiraUrl,
                PaisId = estado.PaisId
            };

            _dataContext.Estados.Add(state);
            _dataContext.SaveChanges();
        }

        // PUT: api/Estados/5
        public void Put(int id, Estado estado)
        {
            var estadoAlterado = _dataContext.Estados.Where(e => e.Id == id).FirstOrDefault();
            if(estadoAlterado != null)
            {
                estadoAlterado.Nome = estado.Nome;
                estadoAlterado.BandeiraUrl = estado.BandeiraUrl;
                estadoAlterado.PaisId = estado.PaisId;
            }
            _dataContext.SaveChanges();
        }

        // DELETE: api/Estados/5
        public void Delete(int id)
        {
            var estadoDeletado = _dataContext.Estados.Where(e => e.Id == id).FirstOrDefault();
            if (estadoDeletado != null)
            {
                _dataContext.Estados.Remove(estadoDeletado);
            }
            _dataContext.SaveChanges();
        }

    }
}

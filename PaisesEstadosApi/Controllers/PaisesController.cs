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
    public class PaisesController : ApiController
    {
        private DataContext _dataContext;

        public PaisesController()
        {
            _dataContext = new DataContext();
        }

        // GET: api/Paises
        public List<Pais> Get()
        {
            List<Pais> Paises = new List<Pais>();
            foreach (var p in _dataContext.Paises)
            {
                var pais = new Pais()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    BandeiraUrl = p.BandeiraUrl

                };
                Paises.Add(pais);
            }

            return Paises;
        }

        // GET: api/Paises/5
        public Pais Get(int id)
        {
            Pais paisSelecionado = _dataContext.Paises.Where(p => p.Id == id).FirstOrDefault();
            return paisSelecionado;
        }

        // POST: api/Paises
        public void Post(Pais pais)
        {
            var paisSalvo = new Pais()
            {
                Nome = pais.Nome,
                BandeiraUrl = pais.BandeiraUrl
            };

            _dataContext.Paises.Add(paisSalvo);
            _dataContext.SaveChanges();
        }

        // PUT: api/Paises/5
        public void Put(int id, Pais pais)
        {
            var PaisToUpdate = _dataContext.Paises.Where(a => a.Id == id).FirstOrDefault();

            if (PaisToUpdate != null)
            {
                PaisToUpdate.Nome = pais.Nome;
                PaisToUpdate.BandeiraUrl = pais.BandeiraUrl;
            }

            _dataContext.SaveChanges();
        }

        // DELETE: api/Paises/5
        public void Delete(int id)
        {
            var PaisToDelete = _dataContext.Paises.Where(a => a.Id == id).FirstOrDefault();

            if (PaisToDelete != null)
            {
                _dataContext.Paises.Remove(PaisToDelete);
            }

            _dataContext.SaveChanges();
        }
    }
}

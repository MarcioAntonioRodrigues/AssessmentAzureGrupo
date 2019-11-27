using Negocio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tp3AzureMarcio.Models;

namespace Tp3AzureMarcio.Controllers
{
    public class AmigosController : ApiController
    {
        private DataContext _dataContext;

        public AmigosController()
        {
            _dataContext = new DataContext();
        }

        // GET: api/Amigos
        public List<Amigo> Get()
        {
            List<Amigo> amigos = new List<Amigo>();

            foreach(var a in _dataContext.Amigos)
            {
                amigos.Add(a);
            }

            return amigos;

        }

        // GET: api/Amigos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Amigos
        public void Post(Amigo json)
        {
            var AmigoCriado = new Amigo()
            {
                Id = json.Id,
                Nome = json.Nome,
                SobreNome = json.SobreNome,
                Email = json.Email,
                Telefone = json.Telefone,
                Aniversario = json.Aniversario,
                NomePais = json.NomePais,
                NomeEstado = json.NomeEstado
            };

            _dataContext.Amigos.Add(AmigoCriado);
            _dataContext.SaveChanges();

        }

        // PUT: api/Amigos/5
        public void Put(int id, Amigo amigo)
        {
            var amigoToUpdate = _dataContext.Amigos.Where(a => a.Id == id).FirstOrDefault();

            if(amigoToUpdate != null)
            {
                amigoToUpdate.Nome = amigo.Nome;
                amigoToUpdate.SobreNome = amigo.SobreNome;
                amigoToUpdate.Telefone = amigo.Telefone;
                amigoToUpdate.NomePais = amigo.NomePais;
                amigoToUpdate.NomeEstado = amigo.NomeEstado;
                amigoToUpdate.Email = amigo.Email;
            }

            _dataContext.SaveChanges();

        }

        // DELETE: api/Amigos/5
        public void Delete(int id)
        {
            var amigoRemovido = _dataContext.Amigos.Where(a => a.Id == id).FirstOrDefault();

            if(amigoRemovido != null)
            {
                _dataContext.Amigos.Remove(amigoRemovido);
                _dataContext.SaveChanges();
            }

        }
    }
}

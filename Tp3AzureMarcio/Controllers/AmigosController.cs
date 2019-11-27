using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Negocio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var result = await Request.Content.ReadAsMultipartAsync();

            var requestJson = await result.Contents[0].ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<AmigoBindModel>(requestJson);

            if (result.Contents.Count > 1)
            {
                model.PictureUrl = await CreateBlob(result.Contents[1], model.Nome);
            }

            var AmigoCriado = new Amigo()
            {
                Id = model.Id,
                PictureUrl = model.PictureUrl,
                Nome = model.Nome,
                SobreNome = model.SobreNome,
                Email = model.Email,
                Telefone = model.Telefone,
                Aniversario = model.Aniversario,
                NomePais = model.NomePais,
                NomeEstado = model.NomeEstado
            };

            _dataContext.Amigos.Add(AmigoCriado);
            _dataContext.SaveChanges();

            return Ok();
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

        private async Task<string> CreateBlob(HttpContent httpContent, string userName)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobContainerName = userName.ToLower();
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);

            await blobContainer.CreateIfNotExistsAsync();

            await blobContainer.SetPermissionsAsync(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            var fileName = httpContent.Headers.ContentDisposition.FileName;
            if (fileName == null)
            {
                return null;
            }
            var byteArray = await httpContent.ReadAsByteArrayAsync();

            var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(fileName));
            await blob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);

            return blob.Uri.AbsoluteUri;

        }

        private string GetRandomBlobName(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}

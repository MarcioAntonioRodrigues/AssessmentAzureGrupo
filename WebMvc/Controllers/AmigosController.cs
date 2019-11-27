using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class AmigosController : Controller
    {
        // GET: Amigos
        public async Task<ActionResult> Index()
        {
            ActionResult x = await GetAmigos();
            IEnumerable<AmigoViewModel> amigos = (IEnumerable<AmigoViewModel>)Session["Amigos"];
            return View(amigos);
        }

        [HttpGet]
        public async Task<ActionResult> GetAmigos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49572/");
                var response = await client.GetAsync("/api/amigos");
                if (response.IsSuccessStatusCode)
                {
                    var amigos = await response.Content.ReadAsAsync<IEnumerable<AmigoViewModel>>();
                    Session["Amigos"] = amigos;
                    return RedirectToAction("Index", "Amigos");
                }
                return View("Error");
            }
        }

        // GET: Amigos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Amigos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigos/Create
        [HttpPost]
        public async Task<ActionResult> Create(AmigoViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri("http://localhost:49572/");
                        client.DefaultRequestHeaders.Accept.Clear();

                        content.Add(new StringContent(JsonConvert.SerializeObject(model)));

                        if (Request.Files.Count > 0)
                        {
                            byte[] fileBytes;
                            using (var inputStream = Request.Files[0].InputStream)
                            {
                                var memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                fileBytes = memoryStream.ToArray();
                            }
                            var fileContent = new ByteArrayContent(fileBytes);
                            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            fileContent.Headers.ContentDisposition.FileName = Request.Files[0].FileName.Split('\\').Last();

                            content.Add(fileContent);
                        }

                        var response = await client.PostAsync("/api/amigos", content);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
            }
            return View();
        }

        // GET: Amigos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Amigos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Amigos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Amigos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

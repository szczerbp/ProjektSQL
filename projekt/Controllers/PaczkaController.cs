using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class PaczkaController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public PaczkaController(DatabaseDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<IActionResult> Create()
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        return View();
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paczka p)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        Firma? firma = _db.Firma.Single(f => f.Id.Equals(p.FirmaId));
                        p.Wlasciciel = firma.Nazwa;
                        _db.Paczka.Add(p);
                        _db.SaveChanges();
                        return RedirectToAction("GetList", "Paczka");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        /** MIEJSCE NA KOD **/
                        _db.Paczka.Where(p => p.Id == Id).ExecuteDelete();
                        return RedirectToAction("GetList", "Paczka");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        public async Task<IActionResult> Edit(long Id)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        /** MIEJSCE NA KOD **/
                        Paczka p = _db.Paczka.Single(pp => pp.Id.Equals(Id));
                        return View(p);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Paczka p)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        /** MIEJSCE NA KOD **/
                        var pac = _db.Paczka.SingleOrDefault(pp => pp.Id.Equals(p.Id));
                        Firma firma = _db.Firma.Single(f => f.Id.Equals(p.FirmaId));
                        pac.Wlasciciel = firma.Nazwa;
                        pac.FirmaId = p.FirmaId;
                        pac.MagazynId = p.MagazynId;
                        pac.Stan = p.Stan;

                        _db.SaveChanges();
                        return RedirectToAction("GetList", "Paczka");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
            
        }
        public async Task<IActionResult> GetList()
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konto.Single(k => k.Login.Equals(login));

                        /** MIEJSCE NA KOD **/
                        var paczki = from s in _db.Paczka select s;
                        return View(paczki);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }
    }
}

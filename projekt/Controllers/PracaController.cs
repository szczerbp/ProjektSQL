using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class PracaController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public PracaController(DatabaseDbContext db, IConfiguration config)
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
        public async Task<IActionResult> Create(Praca p)
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

                        p.KontoId = k.Id;
                        p.CzasRozpoczecia = DateTime.Now;
                        p.CzasZakonczenia = DateTime.Now.AddHours(8);
                        _db.Praca.Add(p);
                        _db.SaveChanges();
                        return RedirectToAction("GetList", "Praca");
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
                        _db.Praca.Where(p => p.Id == Id).ExecuteDelete();
                        return RedirectToAction("GetList", "Praca");
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
                        Praca p = _db.Praca.Single(pp => pp.Id.Equals(Id));
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
        public async Task<IActionResult> Edit(Praca p)
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
                        var pac = _db.Praca.SingleOrDefault(pp => pp.Id.Equals(p.Id));
                        pac.CzasZakonczenia = p.CzasZakonczenia;

                        _db.SaveChanges();
                        return RedirectToAction("GetList", "Praca");
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
                        var prace = from s in _db.Praca select s;
                        return View(prace);
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

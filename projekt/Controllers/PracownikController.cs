using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class PracownikController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public PracownikController(DatabaseDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            /** MIEJSCE NA KOD **/
                            _db.Pracownik.Where(p => p.Id == Id).ExecuteDelete();
                            return RedirectToAction("GetList", "Pracownik");
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
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
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            Pracownik p = _db.Pracownik.SingleOrDefault(pp => pp.Id.Equals(Id));
                            return View(p);
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
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
        public async Task<IActionResult> Edit(Pracownik p)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            var prac = _db.Pracownik.SingleOrDefault(pr => pr.Id.Equals(p.Id));
                            prac.Imie = p.Imie;
                            prac.Nazwisko = p.Nazwisko;

                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Pracownik");
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
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
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            /** MIEJSCE NA KOD **/
                            var pracownicy = from s in _db.Pracownik select s;
                            return View(pracownicy);
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
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

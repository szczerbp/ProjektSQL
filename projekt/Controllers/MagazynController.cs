using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class MagazynController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public MagazynController(DatabaseDbContext db, IConfiguration config)
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
                            return View();
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
        public async Task<IActionResult> Create(Magazyn m)
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
                            _db.Magazyn.Add(m);
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Magazyn");
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
                            _db.Magazyn.Where(m => m.Id == Id).ExecuteDelete();
                            return RedirectToAction("GetList", "Magazyn");
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
                            Magazyn m = _db.Magazyn.SingleOrDefault(ff => ff.Id.Equals(Id));
                            return View(m);
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
        public async Task<IActionResult> Edit(Magazyn m)
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
                            var mag = _db.Magazyn.SingleOrDefault(mm => mm.Id.Equals(m.Id));
                            mag.Miasto = m.Miasto;
                            mag.Powierzchnia = m.Powierzchnia;
                            mag.PojemnoscPaczek = m.PojemnoscPaczek;

                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Magazyn");
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
                            var magazyny = from s in _db.Magazyn select s;
                            return View(magazyny);
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

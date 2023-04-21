using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace projekt.Controllers
{
    public class FirmaController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public FirmaController(DatabaseDbContext db, IConfiguration config)
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
        public async Task<IActionResult> Create(Firma f)
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
                            _db.Firma.Add(f);
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Firma");
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
                            _db.Firma.Where(f => f.Id == Id).ExecuteDelete();
                            return RedirectToAction("GetList", "Firma");
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
                            Firma f = _db.Firma.SingleOrDefault(ff => ff.Id.Equals(Id));
                            return View(f);
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
        public async Task<IActionResult> Edit(Firma f)
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
                            var firma = _db.Firma.SingleOrDefault(ff => ff.Id.Equals(f.Id));
                            firma.Nazwa = f.Nazwa;
                            firma.NIP = f.NIP;
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Firma");
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
                            var firmy = from s in _db.Firma select s;
                            return View(firmy);
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

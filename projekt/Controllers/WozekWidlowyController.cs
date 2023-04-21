using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class WozekWidlowyController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public WozekWidlowyController(DatabaseDbContext db, IConfiguration config)
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
        public async Task<IActionResult> Create(WozekWidlowy w)
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
                            _db.WozekWidlowy.Add(w);
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "WozekWidlowy");
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
                            _db.WozekWidlowy.Where(w => w.Id == Id).ExecuteDelete();
                            return RedirectToAction("GetList", "WozekWidlowy");
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
                            WozekWidlowy w = _db.WozekWidlowy.SingleOrDefault(ww => ww.Id.Equals(Id));
                            return View(w);
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
        public async Task<IActionResult> Edit(WozekWidlowy w)
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
                            var wz = _db.WozekWidlowy.SingleOrDefault(ww => ww.Id.Equals(w.Id));
                            wz.DataOstatniegoSerwisu = w.DataOstatniegoSerwisu;
                            wz.MagazynId = w.MagazynId;
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "WozekWidlowy");
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
                            var wozki = from s in _db.WozekWidlowy select s;
                            return View(wozki);
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

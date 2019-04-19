using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;

namespace MyApp.Web.Helper.webapi
{
    [Route("api/[controller]/[action]")]
    public abstract class MyWebApiBaseController : Controller
    {
        protected readonly MyContext _db;

        protected MyWebApiBaseController(MyContext db)
        {
            _db = db;
        }

        protected KorisnickiNalog AuthKorisnickiNalog => HttpContext.GetKorisnikOfAuthToken();
        protected Korisnik AuthKorisnik => _db.Korisnik.SingleOrDefault(s => s.KorisnickiNalogId == AuthKorisnickiNalog.Id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;

namespace MyApp.Web.Helper.webapi
{
    public static class MyAuthTokenExtension
    {

        public static KorisnickiNalog GetKorisnikOfAuthToken(this HttpContext httpContext)
        {
            MyContext db = httpContext.RequestServices.GetService<MyContext>();

            string token = httpContext.GetMyAuthToken();
            KorisnickiNalog korisnickiNalog = db.AutorizacijskiToken.Where(x => token != null && x.Vrijednost == token).Select(s => s.KorisnickiNalog).SingleOrDefault();
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["MyAuthToken"];
            return token;
        }
    }
}

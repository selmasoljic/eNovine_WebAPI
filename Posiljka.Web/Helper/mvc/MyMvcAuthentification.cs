using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;

namespace MyApp.Web.Helper.mvc
{
    public static class MyMvcAuthentification
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context,KorisnickiNalog korisnik, bool snimiUCookie=false)
        {

            MyContext db = context.RequestServices.GetService<MyContext>();

            string stariToken1 = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken1 != null)
            {
                AutorizacijskiToken obrisati = db.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == stariToken1);
                if (obrisati != null)
                {
                    db.AutorizacijskiToken.Remove(obrisati);
                    db.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                db.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnickiNalogId = korisnik.Id,
                    VrijemeEvidentiranja = DateTime.Now,
                    deviceInfo = "Web app - " + context.Request.Headers["User-Agent"],
                    IpAdresa = context.Connection.RemoteIpAddress + ":" + context.Connection.RemotePort

                });
                db.SaveChanges();
                context.Session.Set(LogiraniKorisnik, token);
                if (snimiUCookie)
                    context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            string token = context.Session.Get<string>(LogiraniKorisnik);
            if (token == null)
                token =  context.Request.GetCookieJson<string>(LogiraniKorisnik);

            return token;
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {
            string token = GetTrenutniToken(context);
            if (token == null)
                return null;

            MyContext db = context.RequestServices.GetService<MyContext>();

            return db.AutorizacijskiToken
                .Where(x => x.Vrijednost == token)
                .Select(s => s.KorisnickiNalog)
                .SingleOrDefault();

        }

    }
}
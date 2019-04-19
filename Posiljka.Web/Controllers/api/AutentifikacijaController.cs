using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;
using MyApp.Web.Helper.webapi;
using MyApp.Web.ViewModels.api;

namespace MyApp.Web.Controllers.api
{
    public class AutentifikacijaController : MyWebApiBaseController
    {
        public AutentifikacijaController(MyContext db) : base(db)
        {
        }

        [HttpPost]
        public ActionResult<AutentifikacijaResultVM> LoginCheck([FromBody] AutentifikacijaLoginPostVM input)
        {
            string token = Guid.NewGuid().ToString();

            AutentifikacijaResultVM model = _db.Korisnik
                .Where(w => w.KorisnickiNalog.KorisnickoIme == input.Username && w.KorisnickiNalog.Lozinka == input.Password)
                .Select(s => new AutentifikacijaResultVM
                {
                    ime = s.Ime,
                    korisnickiNalogId = s.KorisnickiNalogId,
                    prezime = s.Prezime,
                    username = s.KorisnickiNalog.KorisnickoIme,
                    password = s.KorisnickiNalog.Lozinka,
                    token = token,
                }).SingleOrDefault();


            if (model != null)
            {
                _db.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = model.token,
                    KorisnickiNalogId = model.korisnickiNalogId.Value,
                    VrijemeEvidentiranja = DateTime.Now,
                    deviceInfo = "Mobile app - " + input.deviceInfo,
                    IpAdresa = HttpContext.Connection.RemoteIpAddress + ":" + HttpContext.Connection.RemotePort
                });
                _db.SaveChanges();
            }

            return model;
        }


        [HttpDelete]
        public ActionResult Logout()
        {
            string tokenString = HttpContext.GetMyAuthToken();
            AutorizacijskiToken autorizacijskiToken = _db.AutorizacijskiToken.Find(tokenString);
            if (autorizacijskiToken != null)
            {
                _db.Remove(autorizacijskiToken);
                _db.SaveChanges();
            }
            return Ok();
        }
    }
}

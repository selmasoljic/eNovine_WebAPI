using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;
using MyApp.Web.Helper.webapi;
using MyApp.Web.ViewModels.api;

namespace MyApp.Web.Controllers.api
{
    [MyApiAuthorize]
    public class KorisnikController : MyWebApiBaseController
    {
        public KorisnikController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        public KorisnikPregledVM Find()
        {
            return Find("");
        }

        [HttpGet("{name}")]
        public KorisnikPregledVM Find(string name)
        {
            var result = new KorisnikPregledVM
            {
                rows = _db.Korisnik
                .Where(w => (w.Ime + " " + w.Prezime).StartsWith(name) || (w.Prezime + " " + w.Ime).StartsWith(name))
                .Select(s => new KorisnikPregledVM.Row
                {
                    id = s.Id,
                    ime = s.Ime,
                    prezime = s.Prezime
                }).ToList()
            };
            return result;
        }

        [HttpPost]
        public KorisnikPregledVM.Row Add([FromBody]KorisnikAddVM input)
        {
            Korisnik newKorisnik = new Korisnik
            {
                Ime = input.ime,
                Prezime = input.prezime,
                KorisnickiNalog = new KorisnickiNalog
                {
                    KorisnickoIme = input.ime + "." + input.prezime,
                    Lozinka = "test"
                }
            };
            _db.Korisnik.Add(newKorisnik);
            _db.SaveChanges();


            var result = _db.Korisnik
                    .Where(w => w.Id == newKorisnik.Id)
                    .Select(s => new KorisnikPregledVM.Row
                    {
                        id = s.Id,
                        ime = s.Ime,
                        prezime = s.Prezime
                    })
                    .Single();

            return result;
        }


    }
}

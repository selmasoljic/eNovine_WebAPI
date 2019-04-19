using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MyApp.Web.Helper.webapi;
using MyApp.Data.EF;
using MyApp.Web.ViewModels.api;
using MyApp.Data.EntityModels;

namespace MyApp.Web.Controllers.api
{
    [MyApiAuthorize]
    public class VijestiController : MyWebApiBaseController
    {
        public VijestiController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        public VijestPregledVM Find()
        {
            return Find2("");
        }

        [HttpGet]
        public VijestPregledVM Find2(string query)
        {
            if (query == null) query = "";
            var result = new VijestPregledVM
            {
                rows = _db.Vijest
                .Where(w => (w.KategorijaVijesti.Naziv).StartsWith(query))
                .Select(s => new VijestPregledVM.Row
                {
                    id = s.Id,
                    naslovVijesti = s.Naslov,
                    sadrzajVijesti = s.Sadrzaj,
                    nazivKategorije = s.KategorijaVijesti.Naziv
                    
                }).ToList()
            };
            return result;
        }


        [HttpGet]
        public ActionResult<VijestPregledVM> Index()
        {
            //if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
            //    return StatusCode(500, "Možete pristupiti samo petkom.");

            var model = new VijestPregledVM
            {
                rows = _db.Vijest
                    .OrderByDescending(s => s.Id)
                    .Select(s => new VijestPregledVM.Row
                    {
                        id = s.Id,
                        brojVijesti = s.BrojVijesti,
                        naslovVijesti = s.Naslov,
                        sadrzajVijesti = s.Sadrzaj,
                        nazivKategorije = s.KategorijaVijesti.Naziv

                    }).ToList()
            };


            return model;
        }

        [HttpDelete]
        public ActionResult Remove(int id)
        {
            VijestRecord x = _db.Vijest.Find(id);
            if (x != null)
            {
                _db.Vijest.Remove(x);
                _db.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public int Add([FromBody] VijestAddVM input)
        {
            VijestRecord newVijest = new VijestRecord()
            {
                Naslov = input.naslovVijesti,
                Sadrzaj = input.sadrzajVijesti,
                KategorijaVijestiId = input.kategorijaId,
                Datum = DateTime.Now,
                BrojVijesti = input.brojVijesti
            };
            _db.Add(newVijest);
            _db.SaveChanges();
            return 0;
        }
    }
}
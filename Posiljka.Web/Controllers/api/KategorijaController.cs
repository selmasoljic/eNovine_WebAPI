using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;
using MyApp.Web.Helper.webapi;
using MyApp.Web.ViewModels.api;

namespace MyApp.Web.Controllers.api
{
    [MyApiAuthorize]
    public class KategorijaController : MyWebApiBaseController
    {
        public KategorijaController(MyContext db) : base(db)
        {
        }

        //[HttpGet]
        //public ActionResult<KategorijaPregledVM> GetAll()
        //{
        //    var model = new KategorijaPregledVM()
        //    {
        //        rows = _db.Kategorija
        //            .Select(s => new KategorijaPregledVM.Row
        //            {
        //                id = s.Id,
        //                naziv = s.Naziv
        //            }).ToList()
        //    };
        //    return model;
        //}

        [HttpGet]
        public ActionResult<KategorijaPregledVM> Index()
        {
            //if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
            //    return StatusCode(500, "Možete pristupiti samo petkom.");

            var model = new KategorijaPregledVM
            {
                rows = _db.Kategorija
                    .Select(s => new KategorijaPregledVM.Row
                    {

                        id = s.Id, 
                        naziv = s.Naziv

                    }).ToList()
            };


            return model;
        }


    }
}
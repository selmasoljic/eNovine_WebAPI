using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;
using MyApp.Web.Helper.mvc;

namespace MyApp.Web.Controllers
{
    public class HomeController : MyMvcBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestDB()
        {
            MyDbInit.Run(_db);
            return View(_db);
        }

        public HomeController(MyContext db) : base(db)
        {
        }
    }
}

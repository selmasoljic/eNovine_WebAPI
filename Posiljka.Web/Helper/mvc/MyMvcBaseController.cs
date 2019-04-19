using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;

namespace MyApp.Web.Helper.mvc
{
    public abstract class MyMvcBaseController : Controller
    {
        protected readonly MyContext _db;

        protected MyMvcBaseController(MyContext db)
        {
            _db = db;
        }

       
    }
}

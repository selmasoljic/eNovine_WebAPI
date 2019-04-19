using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;
using MyApp.Web.Controllers;
using MyApp.Web.Controllers.api;
using MyApp.Web.Helper;

namespace MyApp.Web.Helper.mvc
{

    public class MyMvcAuthorizeAttribute : TypeFilterAttribute
    {
        public MyMvcAuthorizeAttribute()
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {

        public MyAuthorizeImpl()
        {
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = MyStringValues.NotLogged;
                }

                filterContext.Result = new RedirectToActionResult("Index", nameof(Controllers.AutentifikacijaController).Replace("Controller",""), new { @area = "" });
                return;
            }

            if (true)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //Preuzimamo DbContext preko app services
            MyContext db = filterContext.HttpContext.RequestServices.GetService<MyContext>();

            if (true)
            {
                await next();//ok - ima pravo pristupa
                return;
            }

          

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = MyStringValues.NotAllowed;
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}


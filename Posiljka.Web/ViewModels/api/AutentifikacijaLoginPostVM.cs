using System.ComponentModel.DataAnnotations;
using MyApp.Web.Helper;

namespace MyApp.Web.ViewModels.api
{
    public class AutentifikacijaLoginPostVM
    {
        public string Username { get; set; }
        public string Password{ get; set; }
        public string deviceInfo{ get; set; }
    }
}

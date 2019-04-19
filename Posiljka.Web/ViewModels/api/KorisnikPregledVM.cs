using System.Collections.Generic;

namespace MyApp.Web.ViewModels.api
{
    public class KorisnikPregledVM
    {
        public class Row
        {
            public int? id;
            public string ime;
            public string prezime;
        }

        public List<Row> rows;
    }
}

using MyApp.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.ViewModels.api
{
    public class VijestiPregledVM
    {
        public class Row
        {
            public int? id;
            public Kategorija nazivKategorije;
            public string naslovVijesti;
            public string sadrzajVijesti;
            public int brojVijesti;
        }

        public List<Row> rows;
    }
}

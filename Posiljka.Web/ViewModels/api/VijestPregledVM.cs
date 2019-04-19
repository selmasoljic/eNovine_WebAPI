using MyApp.Data.EntityModels;
using System;
using System.Collections.Generic;

namespace MyApp.Web.ViewModels.api
{
    public class VijestPregledVM
    {
       
        public class Row
        {
            public int id;
            public string nazivKategorije;
            public string naslovVijesti;
            public string sadrzajVijesti;
            public int brojVijesti;
        }

        public List<Row> rows;

    }
}

using System;
using System.Collections.Generic;

namespace MyApp.Web.ViewModels.api
{

    public class KategorijaPregledVM
    {
        public class Row
        {
            public int id;
            public string naziv;
        }

        public List<Row> rows;

    }
}

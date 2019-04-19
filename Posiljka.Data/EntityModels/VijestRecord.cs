using MyApp.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyApp.Data.EntityModels
{
    public class VijestRecord
    {
        public int Id { get; set; }

        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int BrojVijesti { get; set; }
        public DateTime? Datum { get; set; }


        [ForeignKey(nameof(KategorijaVijesti))]
        public int? KategorijaVijestiId { get; set; }
        public Kategorija KategorijaVijesti { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}

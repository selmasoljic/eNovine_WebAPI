using MyApp.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyApp.Data.EF
{
    public static class MyDbInit
    {
        public static void Run(MyContext _context)
        {
            // Look for any Vijests
        
            if (_context.Vijest.Any())
            {
                return; // DB has been seeded
            }


            List<Kategorija> kategorijas = new List<Kategorija>();
            List<Korisnik> korisniks = new List<Korisnik>();
            List<VijestRecord> vijestrecords = new List<VijestRecord>();


            kategorijas.Add(new Kategorija {Naziv = "Politika"});
            kategorijas.Add(new Kategorija {Naziv = "Sport"});
            kategorijas.Add(new Kategorija {Naziv = "Kultura"});
            kategorijas.Add(new Kategorija {Naziv = "Zabava"});
            kategorijas.Add(new Kategorija { Naziv = "Hronika" });
            kategorijas.Add(new Kategorija { Naziv = "Zdravlje" });
            kategorijas.Add(new Kategorija { Naziv = "Zanimljivosti" });

     
            for (int i = 0; i < 5; i++)
            {
                korisniks.Add(new Korisnik{Ime = MyRandomString(5), Prezime = MyRandomString(5),   KorisnickiNalog = new KorisnickiNalog { KorisnickoIme = "Korisnik" + i, Lozinka = "test"} });
            }

            for (int i = 0; i < 10; i++)
            {
                vijestrecords.Add(new VijestRecord { Naslov = MyRandomString(5), Sadrzaj = MyRandomString(25),  BrojVijesti = i, Datum = DateTime.Now, KategorijaVijesti=kategorijas.MyRandom() });
            }


            _context.Kategorija.AddRange(kategorijas);
            _context.Korisnik.AddRange(korisniks);
            _context.Vijest.AddRange(vijestrecords);

          
            _context.SaveChanges();
        }

        private static string MyRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        static readonly Random random = new Random();

        private static T MyRandom<T>(this List<T> lista)
        {
            int r = random.Next(0, lista.Count);
            return lista[r];
        }

    }
}
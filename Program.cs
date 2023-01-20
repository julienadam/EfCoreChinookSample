using ChinookExercice.Entities;

namespace ChinookExercice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new ChinookContext();

            //foreach(var genre in context.Genres)
            //{
            //    Console.WriteLine(genre.Name);
            //}

            var expensiveTracks = context.Tracks.Where(track => track.UnitPrice > 1.0m);
            foreach (var t in expensiveTracks)
            {
                Console.WriteLine($"{t.Name} {t.UnitPrice}");
            }

            Console.WriteLine("============================ Night albums ===============================");
            // Trouver les albums dont le nom contient "Night"
            foreach (var album in context.Albums.Where(a => a.Title.Contains("Night")))
            {
                Console.WriteLine($"{album.Title}");
            }

            Console.WriteLine();
            Console.WriteLine("============================ Commandes depuis Paris en 2009 ===============================");
            // Trouver les commandes placées depuis Paris en 2009
            foreach (var invoice in context.Invoices.Where(invoice => invoice.InvoiceDate.Year == 2009 && invoice.BillingCity == "Paris"))
            {
                Console.WriteLine($"{invoice.InvoiceId} {invoice.BillingCity} {invoice.InvoiceDate}");
            }

            // Trouver le montant total des commandes placées depuis Paris en 2009
            Console.WriteLine();
            Console.WriteLine("============================ Montant des commandes depuis Paris en 2009 ===============================");
            
            // Trouver le montant total des commandes placées depuis Paris en 2009
            var total = context.Invoices
                .Where(invoice => invoice.InvoiceDate.Year == 2009 && invoice.BillingCity == "Paris")
                .Sum(i => i.Total);
            
            Console.WriteLine($"Montant total : {total}");


            var bestCa = context.Invoices
                .Where(i => i.InvoiceDate.Year == 2009)
                .GroupBy(i => i.BillingCity)
                .Select(group => new { City = group.Key, CA = group.Sum(invoices => invoices.Total) })
                .ToList()
                .MaxBy(caByCity => caByCity.CA);

            Console.WriteLine($"{bestCa?.City} avec {bestCa?.CA}");

            // INSERT
            //var newGenre = new Genre { Name = "Foo" };
            //context.Genres.Add(newGenre);

            // UPDATE
            // var genre = context.Genres.Single(genre => genre.GenreId == 26);
            //genre.Name = "Bar";

            // DELETE
            var genre = context.Genres.Single(genre => genre.GenreId == 26);
            context.Genres.Remove(genre);

            context.SaveChanges();
            //foreach(var group in bestCa)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach(var invoice in group)
            //    {
            //        Console.WriteLine("\t" + invoice.Total);
            //    }
            //}

            //.Select(g => new { Ville = g.Key, CA = g.Sum(x => x.Total) })
            //.OrderByDescending(x => x.CA)
            //.FirstOrDefault();

            //Console.WriteLine($"{bestCa?.Ville} avec {bestCa?.CA}");
        }
    }
}
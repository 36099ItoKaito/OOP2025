namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            //var price = Library.Books
            //    .Where(b => b.CategoryId == 1)
            //    .Max(b => b.Price);
            //Console.WriteLine(price);

            //Console.WriteLine();//改行

            //var book = Library.Books
            //    .Where(b => b.PublishedYear >= 2021)
            //    .MinBy(b => b.Price);
            //Console.WriteLine(book);

            //var average = Library.Books.Average(x => x.Price);
            //var aboves = Library.Books
            //    .Where(b => b.Price > average);
            //foreach(var book1 in aboves) {
            //    Console.WriteLine(book1);
            //}


            //var selected = Library.Books
            //    .GroupBy(b => b.PublishedYear)
            //    .Select(group => group.MaxBy(b => b.Price))
            //    .OrderBy(b => b!.PublishedYear);

            //foreach (var book in selected) {
            //    Console.WriteLine($"{book!.PublishedYear}年 {book!.Title} ({book!.Price})");
            //}


            var groups = Library.Categories
                   .GroupJoin(Library.Books,
                         c => c.Id,
                         b => b.CategoryId,
                         (c, books) => new {
                             Category = c.Name,
                             Books = books
                         });

            foreach (var group in groups) {
                Console.WriteLine(group.Category);
                foreach (var book in group.Books) {
                    Console.WriteLine($"   {book.Title} ({book.PublishedYear})年");
                }
            }
        }
    }
}

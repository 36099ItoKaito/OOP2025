namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var books = Books.GetBooks();

            //本の平均金額を表示
            Console.WriteLine((int)books.Average(x => x.Price));

            //本のページ合計を表示
            Console.WriteLine((int)books.Sum(x => x.Pages));

            //金額の安い書籍名と金額を表示
            var Price = books.Where(x => x.Price == books.Min(b => b.Price));
            foreach(var item in Price) {
                Console.WriteLine(item.Title + ":" + item.Price + "円");
            }
            //var Price = books.MinBy(b => b.Price);
            //Console.WriteLine($"{Price.Title}:{Price.Price}円");

            //ページが多い書籍名とページ数を表示
            var Pages = books.Where(x => x.Pages == books.Max(b => b.Pages));
            foreach (var item in Pages) {
                Console.WriteLine(item.Title + ":" + item.Pages + "ページ");
            }
            //var Pages = books.MaxBy(b => b.Pages);
            //Console.WriteLine($"{Pages.Title}:{Pages.Pages}ページ");

            //タイトルに物語が含まれている書籍名をすべて表示
            var titles = books.Where(x => x.Title.Contains("物語"));
            foreach (var book in titles) {
                Console.WriteLine(book.Title);
            }
        }
    }
}

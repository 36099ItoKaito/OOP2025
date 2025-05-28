
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var books = new List<Book> {
                new Book { Title = "C#プログラミングの新常識", Price = 3800, Pages = 378 },
                new Book { Title = "ラムダ式とLINQの極意", Price = 2500, Pages = 312 },
                new Book { Title = "ワンダフル・C#ライフ", Price = 2900, Pages = 385 },
                new Book { Title = "一人で学ぶ並列処理プログラミング", Price = 4800, Pages = 464 },
                new Book { Title = "フレーズで覚えるC#入門", Price = 5300, Pages = 604 },
                new Book { Title = "私でも分かったASP.NET Core", Price = 3200, Pages = 453 },
                new Book { Title = "楽しいC#プログラミング教室", Price = 2540, Pages = 348 },
            };

            Console.WriteLine("7.2.1");
            Exercise1(books);

            Console.WriteLine("7.2.2");
            Exercise2(books);

            Console.WriteLine("7.2.3");
            Exercise3(books);

            Console.WriteLine("7.2.4");
            Exercise4(books);

            Console.WriteLine("7.2.5");
            Exercise5(books);

            Console.WriteLine("7.2.6");
            Exercise6(books);

            Console.WriteLine("7.2.7");
            Exercise7(books);
        }

        private static void Exercise1(List<Book> books) {
            var book = books.FirstOrDefault(b => b.Title == "ワンダフル・C#ライフ");
            Console.WriteLine($"価格: {book.Price}円, ページ数: {book.Pages}ページ");
        }

        private static void Exercise2(List<Book> books) {
            int count = books.Count(book => book.Title.Contains("C#"));
            Console.WriteLine($"タイトルに「C#」が含まれている書籍の数: {count}冊");
        }

        private static void Exercise3(List<Book> books) {
            double Pages = books.Where(book => book.Title.Contains("C#")).Average(book => book.Pages);
            Console.WriteLine($"タイトルに「C#」が含まれている書籍の平均ページ数: {Pages}");
        }

        private static void Exercise4(List<Book> books) {
            var book = books.FirstOrDefault(b => b.Price >= 4000);
            Console.WriteLine($"価格が4000円以上の最初の書籍のタイトル: {book.Title}");
        }

        private static void Exercise5(List<Book> books) {
            int Pages = books.Where(book => book.Price < 4000).Max(book => book.Pages);
            Console.WriteLine($"価格が4000円未満の本の最大ページ数は {Pages} ページ");
        }

        private static void Exercise6(List<Book> books) {
            var Pages = books.Where(book => book.Pages >= 400).OrderByDescending(book => book.Price);
            foreach (var book in Pages) {
                Console.WriteLine($"タイトル: {book.Title}, 価格: {book.Price}円");
            }
        }

        private static void Exercise7(List<Book> books) {
            var Pages = books.Where(book => book.Title.Contains("C#") && book.Pages <= 500);
            foreach (var book in Pages) {
                Console.WriteLine(book.Title);
            }
        }
    }
}

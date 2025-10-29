
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();
        }

        private static void Exercise1_2() {
            var book = Library.Books
                .MaxBy(g => g.Price);
            Console.WriteLine(book);
        }

        private static void Exercise1_3() {
            var book = Library.Books
                .GroupBy(book => book.PublishedYear)
                .Select(group => new {
                    Year = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.Year);

            foreach (var item in book) {
                Console.WriteLine($"{item.Year}年: {item.Count}冊");
            }
        }

        private static void Exercise1_4() {
            var books = Library.Books
                .OrderByDescending(book => book.PublishedYear)
                .ThenByDescending(book => book.Price)
                .ToList();

            foreach (var book in books) {
                Console.WriteLine(book);
            }
        }

        private static void Exercise1_5() {
            var books2022 = Library.Books.Where(book => book.PublishedYear == 2022).ToList();

            var categoryIds2022 = books2022
                .Select(book => book.CategoryId)
                .Distinct()
                .ToList();

            var categories2022 = Library.Categories
                .Where(category => categoryIds2022.Contains(category.Id))
                .ToList();

            Console.WriteLine("2022年に発行された書籍のカテゴリ一覧:");
            foreach (var category in categories2022) {
                Console.WriteLine(category.Name);
            }
        }

        private static void Exercise1_6() {
            var groupedBooks = Library.Books
                .GroupBy(book => book.CategoryId)
                .Select(group => new {
                    Category = Library.Categories.First(c => c.Id == group.Key),
                    Books = group.OrderBy(book => book.Title) 
                })
                .OrderBy(group => group.Category.Name)
                .ToList();

            foreach (var group in groupedBooks) {
                Console.WriteLine($"# {group.Category.Name}");
                foreach (var book in group.Books) {
                    Console.WriteLine($"   {book.Title}");
                }
                Console.WriteLine();
            }
        }

        private static void Exercise1_7() {
            var developmentBooks = Library.Books
                .Where(book => book.CategoryId == 1) 
                .GroupBy(book => book.PublishedYear) 
                .OrderBy(group => group.Key) 
                .ToList();

            foreach (var yearGroup in developmentBooks) {
                Console.WriteLine($"# {yearGroup.Key}");
                foreach (var book in yearGroup) {
                    Console.WriteLine($"   {book.Title}");
                }
            }
        }

        private static void Exercise1_8() {
            var categoriesWithMoreThanFourBooks = Library.Categories
                .GroupJoin(Library.Books,
                           category => category.Id,
                           book => book.CategoryId,
                           (category, books) => new {
                               Category = category,
                               BooksCount = books.Count()
                           })
                .Where(group => group.BooksCount >= 4)
                .OrderBy(group => group.Category.Name)
                .ToList();

            foreach (var categoryGroup in categoriesWithMoreThanFourBooks) {
                Console.WriteLine(categoryGroup.Category.Name);
            }
        }
    }
}

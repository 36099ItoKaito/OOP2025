
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
            
        }

        private static void Exercise1_5() {
            
        }

        private static void Exercise1_6() {
            
        }

        private static void Exercise1_7() {
            
        }

        private static void Exercise1_8() {
            
        }
    }
}

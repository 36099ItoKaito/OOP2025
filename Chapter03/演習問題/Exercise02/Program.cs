
using System.Net;

namespace Exercise02 {
    internal class Program {

        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London",
                "Paris", "Berlin", "Canberra", "Hong Kong",
            };

            Console.WriteLine("***** 3.2.1 *****");
            Exercise2_1(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.2 *****");
            Exercise2_2(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.3 *****");
            Exercise2_3(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.4 *****");
            Exercise2_4(cities);
            Console.WriteLine();

        }

        private static void Exercise2_1(List<string> names) {
            Console.WriteLine("都市名を入力。空行で終了");
            while (true) {
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) {
                    break;
                }
                // FindIndex を使って都市名のインデックスを検索
                int index = names.FindIndex(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));

                // 結果を出力
                Console.WriteLine(index);  // 見つからなければ -1 が出力される
            }

        }

        private static void Exercise2_2(List<string> names) {
            // 小文字の'o'を含む都市名をカウント
            var count = names.Count(s => s.Contains('o'));
            Console.WriteLine($"小文字の'o'を含む都市名の数: {count}");
        }

        private static void Exercise2_3(List<string> names) {
            var selected = names.Where(s => s.Contains('o')).ToArray();
            foreach (var name in selected) {
                Console.WriteLine(name);
                /* // 'o' を含む都市名を抽出して配列に格納
                 string[] citiesWithO = names
                  .Where(city => city.Contains('o'))  // 小文字 'o' を含むものに絞り込み
                  .ToArray();                         // 配列に変換
                                                      // 結果をコンソールに出力
                 foreach (string city in citiesWithO) {
                     Console.WriteLine(city);
                 }*/
            }
        }

        private static void Exercise2_4(List<string> names) {
            var obj = names.Where(s => s.StartsWith('B'))
                                .Select(s => new { s, s.Length });

            foreach (var data in obj) {
                Console.WriteLine(data.s + ":" + data.Length + "文字");
            }
        }
    }
}

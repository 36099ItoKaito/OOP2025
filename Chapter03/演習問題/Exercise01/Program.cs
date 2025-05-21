
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var numbers = new List<int> { 12, 87, 94, 14, 53, 20, 40, 35, 76, 91, 31, 17, 48 };


            // 3.1.1
            Exercise1(numbers);
            Console.WriteLine("-----");

            // 3.1.2
            Exercise2(numbers);
            Console.WriteLine("-----");

            // 3.1.3
            Exercise3(numbers);
            Console.WriteLine("-----");

            // 3.1.4
            Exercise4(numbers);
        }

        private static void Exercise1(List<int> numbers) {
            // 8または9で割り切れる数が存在するか調べる
            var exists = numbers.Exists(n => n % 8 == 0 || n % 9 == 0);

            // 結果を出力
            if (exists) {
                Console.WriteLine("存在しています");
            } else {
                Console.WriteLine("存在していません");
            }
        }

        private static void Exercise2(List<int> numbers) {
            // ForEachメソッドで各要素を2.0で割って出力
            numbers.ForEach(n => Console.WriteLine(n / 2.0));
        }

        private static void Exercise3(List<int> numbers) {
            /* // Whereメソッドで50以上の要素をフィルタリング
             var result = numbers.Where(n => n >= 50);
             // 結果をコンソールに出力
             foreach (var number in result) {
                 Console.WriteLine(number);
             }*/
            //foreach (var number in numbers.Where(n => n >= 50)) Console.WriteLine(number);
            numbers.Where(n => n > 50).ToList().ForEach(n => Console.WriteLine(n));
        }

        private static void Exercise4(List<int> numbers) {
            /*// Selectメソッドで各要素を2倍にし、List<int>に格納
            List<int> doubledNumbers = numbers.Select(n => n * 2).ToList();
            // doubledNumbersの各要素をコンソールに出力
            foreach (var number in doubledNumbers) {
                Console.WriteLine(number);
            }*/
            numbers.Select(n => n * 2).ToList().ForEach(n => Console.WriteLine(n));
        }
    }
}

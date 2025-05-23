using System;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1();
            Console.WriteLine("---");
            Exercise2();
            Console.WriteLine("---");
            Exercise3();
        }

        private static void Exercise1() {
            Console.Write("数値を入力してください: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number < 0) {
                    // 負の値はそのまま出力
                    Console.WriteLine(number);
                } else if (number >= 0 && number < 100) {
                    // 0以上100未満 → 2倍
                    Console.WriteLine(number * 2);
                } else if (number >= 100 && number < 500) {
                    // 100以上500未満 → 3倍
                    Console.WriteLine(number * 3);
                } else {
                    // 500以上 → そのまま
                    Console.WriteLine(number);
                }
            } else {
                Console.WriteLine("入力値に誤りがあります。");
            }
        }

        private static void Exercise2() {

        }

        private static void Exercise3() {

        }
    }
}

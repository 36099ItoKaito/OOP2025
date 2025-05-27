namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {

            var numbers = Enumerable.Range(1, 100);

            //合計値を出力
            Console.WriteLine(numbers.Where(n => n % 2 == 0).Min());
            //8の倍数の合計
            Console.WriteLine(numbers.Where(n => n % 8 == 0).Sum());

            //foreach(var num in numbers) {
            //    Console.WriteLine(num);
            //}

        }
    }
}

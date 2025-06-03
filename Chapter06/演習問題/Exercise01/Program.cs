using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var s1 = Console.ReadLine();
            var s2 = Console.ReadLine();

            var cultureinfo = new CultureInfo("ja-JP");
            if (string.Compare(s1, s2, cultureinfo, CompareOptions.None) == 0)
                Console.WriteLine("等しい");
            else
                Console.WriteLine("等しくない");

        }
    }
}

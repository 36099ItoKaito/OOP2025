using System.Diagnostics.Metrics;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            /*//表示
            Console.WriteLine("***変換アプリ***");
            Console.WriteLine("1:インチからメートル");
            Console.WriteLine("2:メートルからインチ");

            //入力
            Console.Write(">");
            int start = int.Parse(Console.ReadLine());
            Console.Write("はじめ：");
            int hajime = int.Parse(Console.ReadLine());
            Console.Write("おわり：");
            int owari = int.Parse(Console.ReadLine());

            //メートルかインチの判別
            if (start == 1) {
                for (int inch = hajime; inch <= owari; inch++) {
                    double meter = InchConverter.InchiToMeter(inch);
                    Console.WriteLine($"{inch}inch = {meter:0.0000}m");
                }
            } else if (start == 2) {
                for (int meter = hajime; meter <= owari; meter++) {
                    double inch = InchConverter.MeterToInchi(meter);
                    Console.WriteLine($"{meter}mt = {inch:0.0000}inch");
                }
            }*/

            Console.WriteLine("***変換アプリ***");
            Console.WriteLine("1:ヤードからメートル");
            Console.WriteLine("2:メートルからヤード");

            Console.Write(">");
            int start2 = int.Parse(Console.ReadLine());

            if(start2 == 1) {

                Console.Write("変換前(ヤード)：");
                int yard = int.Parse(Console.ReadLine());
                double aa = InchConverter.YardToMeter(yard);
                Console.Write("変換後(メートル)：" + $"{aa : 0.000}");

            } else if(start2 == 2) {
                Console.Write("変換前(メートル)");
                int meter = int.Parse(Console.ReadLine());
                double a = InchConverter.MeterToYard(meter);
                Console.Write("変換後(ヤード)："+ $"{a: 0.000}");
            }

            
            




        }
    }
}

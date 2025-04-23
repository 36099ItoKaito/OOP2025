namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            //表示
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
            }
        }
    }
}

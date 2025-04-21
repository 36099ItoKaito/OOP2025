namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィートとメートルの対応表を出力する
        static void Main(string[] args) {

            int i = int.Parse(args[1]);
            int j = int.Parse(args[2]);

            if (args.Length >= i && args[0] == "-tom") {
                PrintFeetToMeterList(i, j);
            } else {
                PrintMeterToFeetList(i,j);

            }
        }

        static void PrintFeetToMeterList(int i, int j) {
            for (int feet = i; feet <= j; feet++) {
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                }
        }

        static void PrintMeterToFeetList(int i, int j) {
            for (int meter = i; meter <= j; meter++) {
                double feet = FeetToMeter(meter);
                Console.WriteLine($"{meter}ft = {feet:0.0000}m");
            }
        }

        static double FeetToMeter(int feet) {
            return feet * 0.3048;
        }

        static double MeterToFeet(int meter) {
            return meter / 0.3048;
        }
    }
}

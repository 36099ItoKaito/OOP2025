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
                double meter = FeetConverter.ToMeter(feet);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                }
        }

        static void PrintMeterToFeetList(int i, int j) {
            for (int meter = i; meter <= j; meter++) {
                double feet = FeetConverter.FromMeter(meter);
                Console.WriteLine($"{meter}ft = {feet:0.0000}m");
            }
        }
    }
}

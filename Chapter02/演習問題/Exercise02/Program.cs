namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {


            for (int inch = 1; inch <= 10; inch++) {
                double meter = InchiToMeter(inch);
                Console.WriteLine($"{inch}inch = {meter:0.0000}m");
            }
        }

        //定数
        private const double ratio = 0.0254;

        public static double InchiToMeter(double meter) {
            return meter * ratio;
        }
    }
}

namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineCounterService();
            var prcessor = new TextFileProcessor(service);
            Console.WriteLine("パスの入力：");
            prcessor.Run(Console.ReadLine());
        }
    }
}

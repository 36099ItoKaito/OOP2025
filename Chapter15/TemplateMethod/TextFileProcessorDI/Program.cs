namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineToHalfNumberService();
            var processor = new TextFileProcessor(service);
            Console.WriteLine("パスの入力：");
            processor.Run(Console.ReadLine());
        }
    }
}

//１４６５２
//６２４３６２
//3215168
//６２３４６２
//６８４９６４
//13218568
//９４６７３
//3215846521
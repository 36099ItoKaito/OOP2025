using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            string filePath;

            if (args.Length > 0) {
                filePath = args[0];
            } else {
                Console.WriteLine("検索するファイルのパス：");
                filePath = Console.ReadLine() ?? @"""C:\Users\infosys\source\repos\OOP2025\Chapter14\Section03""";

                if (string.IsNullOrWhiteSpace(filePath)) {
                    Console.WriteLine("エラー: ファイルパスが入力されていません。");
                    return;
                }
            }

            TextProcessor.Run<LineCounterProcessor>(filePath);
        }
    }
}
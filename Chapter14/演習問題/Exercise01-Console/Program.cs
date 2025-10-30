using System;
using System.IO;
using System.Threading.Tasks;

class Program {
    static async Task Main(string[] args) {
        string filePath = "走れメロス.txt";
        await ReadFile(filePath);
    }

    static async Task ReadFile(string filePath) {
        using (StreamReader reader = new StreamReader(filePath)) {
            string line;
            while ((line = await reader.ReadLineAsync()) != null) {
                Console.WriteLine(line);
            }
        }
    }
}

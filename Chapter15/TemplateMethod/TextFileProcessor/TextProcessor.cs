using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessor {
    public abstract class TextProcessor {
        protected string SearchWord { get; private set; } = "";

        public static void Run<T>(string fileName) where T : TextProcessor, new() {
            var self = new T();
            self.Process(fileName);
        }

        private void Process(string path) {
            Initialize(path);

            Console.WriteLine("検索する文字：");
            var moji = Console.ReadLine();
            SearchWord = moji ?? "";


            if (!File.Exists(path)) {
                Console.WriteLine($"エラー: ファイル '{path}' が見つかりません。");
                return;
            }

            var lines = File.ReadLines(path);
            foreach (var line in lines) {
                if (!string.IsNullOrEmpty(moji) && line.Contains(moji)) {
                    Execute(line);
                }
            }
            Terminate();

        }

        protected virtual void Initialize(string fname) { }
        protected virtual void Execute(string line) { }
        protected virtual void Terminate() { }
    }
}
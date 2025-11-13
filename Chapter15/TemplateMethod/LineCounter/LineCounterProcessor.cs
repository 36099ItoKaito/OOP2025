using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor {
        private int _count = 0;

        protected override void Initialize(string fname) => _count = 0;

        protected override void Execute(string line) {
            if (!string.IsNullOrEmpty(SearchWord)) {
                int index = 0;
                while ((index = line.IndexOf(SearchWord, index)) != -1) {
                    _count++;
                    index += SearchWord.Length;
                }
            }
        }

        protected override void Terminate() => Console.WriteLine("「{0}」が見つかった回数: {1} 回", SearchWord, _count);
    }
}
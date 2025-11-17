using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    internal class LineCounterService : ITextFileService {
        private int _count;

        public void Initialize(string fname) {
            _count = 0;
        }

        public void Excute(string line) {
            _count++;
        }

        public void Terminate() {
            Console.WriteLine($"{_count} 行");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //問題15.3
    internal class LineOutputService : ITextFileService {
        private int _count;
        public virtual void Initialize(string fname) {
            _count = 0;
        }
        public virtual void Excute(string line) {
            if (_count < 20) {
                Console.WriteLine($"{_count} 行");
                _count++;
            }
        }
        public virtual void Terminate() {

        }
    }
}

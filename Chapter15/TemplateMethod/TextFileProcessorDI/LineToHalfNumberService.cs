using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //P362　問題15.1
    public class LineToHalfNumberService : ITextFileService {
        public void Initialize(string fname) {
        }

        public void Excute(string line) {
            var result = ConvertFullWidthToHalfWidth(line);
            Console.WriteLine(result);
        }

        public void Terminate() {
        }

        private string ConvertFullWidthToHalfWidth(string input) {
            var sb = new StringBuilder();
            foreach (var c in input) {
                if (c >= '０' && c <= '９') {
                    sb.Append((char)(c - '０' + '0'));
                } else {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
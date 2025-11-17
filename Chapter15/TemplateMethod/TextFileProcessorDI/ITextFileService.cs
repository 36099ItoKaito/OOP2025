using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public interface ITextFileService {
        void Initialize(string fname);
        void Excute(string line);
        void Terminate();
    }
}

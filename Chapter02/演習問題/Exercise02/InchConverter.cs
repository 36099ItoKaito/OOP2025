using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    class InchConverter {

        //定数
        private const double ratio = 0.0254;

        private const double ratio2 = 0.9144;

        public static double InchiToMeter(double inch) {
            return inch * ratio;
        }

        public static double MeterToInchi(double meter) {
            return meter / ratio;
        }

        public static double YardToMeter(double yard) {
            return yard * ratio2;
        }

        public static double MeterToYard(double meter) {
            return meter / ratio2;
        }
    }
}

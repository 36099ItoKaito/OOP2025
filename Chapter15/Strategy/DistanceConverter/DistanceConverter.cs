using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    public class DistanceConverter {
        public ConverterBase From { get; init; }
        public ConverterBase To { get; init; }

        //コンストラクタ
        public DistanceConverter(ConverterBase from, ConverterBase to) {
            From = from;
            To = to;
        }

        public double Convert(double value) {
            var meter = From.ToMeter(value);   //メーターへ変換
            return To.FromMeter(meter);        //メーターからそれぞれの単位へ変換
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorChecker {
    public struct MyColor {
        public Color Color { get; set; }
        public string Name { get; set; }

        // ToStringメソッドを色名を表示する形に変更
        public override string ToString() {
            return $"{Name} (R:{Color.R}, G:{Color.G}, B:{Color.B})";
        }
    }
}

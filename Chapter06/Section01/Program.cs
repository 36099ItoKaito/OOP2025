using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var target = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";
            var value = "BestWork=";
            var startIndex = target.IndexOf("BestWork=") + value.Length;
            var endIndex = target.IndexOf(";", startIndex);
            var bestWork = target.Substring(startIndex, endIndex - startIndex);
        }
    }
}

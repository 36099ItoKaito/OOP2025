using System.Text.RegularExpressions;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            string[] texts = [
                "Time is money.",
                "What time is it?",
                "It will take time.",
                "We reorganized the timetable.",
            ];

            Regex regex = new Regex(@"\btime\b", RegexOptions.IgnoreCase);

            foreach (string line in texts) {
                MatchCollection matches = regex.Matches(line);

                foreach (Match match in matches) {
                    Console.WriteLine($"{line}: {match.Index}");
                }
            }

            //foreach(var line in texts) {
            //    var matches = Regex.Matches(line, @"\btime\b", RegexOptions.IgnoreCase);
            //
            //    foreach(Match match2 in matches) {
            //        Console.WriteLine($"{line}, {match2.Index}");
            //    }
            //
            //}
        }
    }
}


namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            Console.WriteLine("=== foreach文 ===");
            foreach (string item in langs) {
                if (item.Contains("S")) {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("=== for文 ===");
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains("S")) {
                    Console.WriteLine(langs[i]);
                }
            }

            Console.WriteLine("=== while文 ===");
            int j = 0;
            while (j < langs.Count) {
                if (langs[j].Contains("S")) {
                    Console.WriteLine(langs[j]);
                }
                j++;
            }
        }

        private static void Exercise2(List<string> langs) {
            var filtered = langs.Where(s => s.Contains("S"));

            Console.WriteLine("=== LINQで抽出 ===");
            foreach (var item in filtered) {
                Console.WriteLine(item);
            }
        }

        private static void Exercise3(List<string> langs) {
            string? lang = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(lang);
        }
    }
}

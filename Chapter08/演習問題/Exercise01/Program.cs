
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);
        }

        private static void Exercise1(string text) {
            //1ディクショナリインスタンスの生成
            //2一文字目取り出す
            //3大文字に変換
            //4アルファベットならディクショナリに登録
            //登録済み：valueをインクリメント
            //未登録：valueに１を設定
            //6すべての文字が読み終わったら、アルファベット順に並び替えて出力
            Dictionary<char, int> moji = new Dictionary<char, int>();
            foreach(char c in text) {
                if (char.IsLetter(c)) {
                    char upperChar = char.ToUpper(c);
                    if (moji.ContainsKey(upperChar)){
                        moji[upperChar]++;
                    } else {
                        moji[upperChar] = 1;
                    }
                }
            }
            foreach(var pair in moji.OrderBy(p => p.Key)) {
                Console.WriteLine($"'{pair.Key}':{pair.Value}");
            }
        }

        private static void Exercise2(string text) {
            SortedDictionary<char, int> moji = new SortedDictionary<char, int>();
            foreach (char c in text) {
                if (char.IsLetter(c)) {
                    char upperChar = char.ToUpper(c);
                    if (moji.ContainsKey(upperChar)) {
                        moji[upperChar]++;
                    } else {
                        moji[upperChar] = 1;
                    }
                }
            }
            foreach (var pair in moji) {
                Console.WriteLine($"'{pair.Key}':{pair.Value}");
            }
        }
    }
}

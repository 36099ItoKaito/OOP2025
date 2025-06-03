namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";
            // セミコロンで区切って各項目を分ける
            string[] items = line.Split(';');

            string novelist = "";
            string bestWork = "";
            string born = "";

            // 各項目をさらに = で分けて値を取り出す
            foreach (var item in items) {
                string[] pair = item.Split('=');
                if (pair.Length == 2) {
                    string key = pair[0];
                    string value = pair[1];

                    switch (key) {
                        case "Novelist":
                            novelist = value;
                            break;
                        case "BestWork":
                            bestWork = value;
                            break;
                        case "Born":
                            born = value;
                            break;
                    }
                }
            }

            // 出力
            Console.WriteLine("作家：" + novelist);
            Console.WriteLine("代表作：" + bestWork);
            Console.WriteLine("誕生年：" + born);



        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {

            return ""; //エラーをなくすためのダミー
        }
    }
}

//出力例
//作家：谷崎潤一郎
//代表作：春琴抄
//誕生年：1886
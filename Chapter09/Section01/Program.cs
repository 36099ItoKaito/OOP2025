using System.Globalization;
using System.Net.Http.Headers;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            //var today = new DateTime(2025,7,12);//日付
            //var now = DateTime.Now;//日付と時刻

            //Console.WriteLine($"ToDay:{today.Month}");
            //Console.WriteLine($"ToDay:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            //　日付を入力
            //  西暦:
            //  月:
            //  年:
            //  日:
            //  ○○○○年〇月〇日は火曜日です ←曜日は漢字で表示
            // 日付の入力
            Console.Write("西暦: ");
            var year = int.Parse(Console.ReadLine());

            Console.Write("月: ");
            var month = int.Parse(Console.ReadLine());

            Console.Write("日: ");
            var day = int.Parse(Console.ReadLine());

            var birth = new DateTime(year, month, day);

            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = birth.ToString("ggyy年M月d日", culture);

            var shortDayOfWeek = culture.DateTimeFormat.GetShortestDayName(birth.DayOfWeek);

            Console.WriteLine(str + shortDayOfWeek + "曜日");

            //②うるう年の判定プログラムを作成する
            //西暦を入力
            //→〇〇年はうるう年です
            //→〇〇年は平年です
            var isLeapYear = DateTime.IsLeapYear(2025);
            if (isLeapYear) {
                Console.WriteLine("うるう年です");
            } else {
                Console.WriteLine("うるう年ではありません");
            }

            //③生まれてから〇〇〇〇日目です
            DateTime today = DateTime.Today;
            TimeSpan elapsed = today - birth;
            long daysPassed = (long)elapsed.TotalDays;
            Console.WriteLine($"生まれてから{daysPassed}日目です");

            //④あなたは〇〇歳です！
            var age = today.Year - birth.Year;
            if(today < birth.AddYears(age)) {
                age--;
            }
            Console.WriteLine($"あなたは{age}歳です");
            //⑤1月１日から何日目か？

        }
    }
}

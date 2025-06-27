
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_1();  //テストケース１
            Console.WriteLine();
            Exercise1_2();//テストケース２
            Console.WriteLine();
            Exercise2();
        }
        // 9.2.1を呼び出すテスト用メソッド
        private static void Exercise1_1() {
            var dt = DateTime.Today;
            foreach (var dayofweek in Enum.GetValues(typeof(DayOfWeek))) {
                Console.Write("{0:yyyy/MM/dd}の次週の{1}: ", dt, (DayOfWeek)dayofweek);
                Console.WriteLine("{0:yyyy/MM/dd(ddd)}", NextWeek(dt, (DayOfWeek)dayofweek));
            }
        }

        // 9.2.1を呼び出すテスト用メソッド
        private static void Exercise1_2() {
            var dt = new DateTime(2024, 12, 29);
            foreach (var dayofweek in Enum.GetValues(typeof(DayOfWeek))) {
                Console.Write("{0:yyyy/MM/dd}の次週の{1}: ", dt, (DayOfWeek)dayofweek);
                Console.WriteLine("{0:yyyy/MM/dd(ddd)}", NextWeek(dt, (DayOfWeek)dayofweek));
            }
        }

        // 9.2.1【ここにプログラムを作成する】
        static DateTime NextWeek(DateTime date, DayOfWeek dayOfWeek) {
            //var nextweek = date.AddDays(7);                     //一週間後の日付を求める(AddDays(7))
            //var days = (int)dayOfWeek - (int)(date.DayOfWeek);  //一週間後の日曜の日付を求める(日曜(0)-金曜(5) = -5)
            //return nextweek.AddDays(days);                      //一週間後の日付から5日戻す(AddDays(-5))

            //var days = (int)dayOfWeek - (int)(date.DayOfWeek);
            //if(days <= 0) {
            //    days += 7;
            //}
            //return date.AddDays(days);

            var days = 7 - (int)(date.DayOfWeek) + (int)dayOfWeek;
            return date.AddDays(days);
        }

        private static void Exercise2() {
            var birthday = new DateOnly(2001, 4, 19);
            var targetDay = new DateOnly(2030, 4, 18);
            var age = GetAge(birthday, targetDay);
            Console.WriteLine(age);
        }

        // 9.2.2【ここにプログラムを作成する】
        static int GetAge(DateOnly birthday, DateOnly targetDay) {
            var age = targetDay.Year - birthday.Year;
            if(targetDay < birthday.AddYears(age)) {
                age--;
            }
            return age;

        }
    }
}

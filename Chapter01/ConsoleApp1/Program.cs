using System.Diagnostics;

namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {

            Product karinto = new Product(123, "かりんとう", 180);
            Product Glock = new Product(124,"G17 gen5 MOS",19800);

            //税抜きの価格を表示(かりんとうの税抜き価格は〇〇円です)
            Console.WriteLine(karinto.Name + "の税抜き価格は" + karinto.Price + "円です");
            //消費税額の表示(かりんとうの消費税額は〇〇円です)
            Console.WriteLine(karinto.Name + "の消費税額は" + karinto.GetTax() + "円です");
            //税込み価格の表示(かりんとうの税込み価格は〇〇円です)
            Console.WriteLine(karinto.Name + "の税込み価格は" + karinto.GetPriceIncludingTax() + "円です");

            //税抜きの価格を表示(G17の税抜き価格は〇〇円です)
            Console.WriteLine(Glock.Name + "の税抜き価格は" + Glock.Price + "円です");
            //消費税額の表示(G17の消費税額は〇〇円です)
            Console.WriteLine(Glock.Name + "の消費税額は" + Glock.GetTax() + "円です");
            //税込み価格の表示(G17の税込み価格は〇〇円です)
            Console.WriteLine(Glock.Name + "の税込み価格は" + Glock.GetPriceIncludingTax() + "円です");
        }
    }
}

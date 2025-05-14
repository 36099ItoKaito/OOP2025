using System.Reflection;

namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            //2.1.3
            /* var songs = new Song[] {
                  new Song("Let it be", "The Beatles", 243),
                  new Song("Bridge Over Troubled Water", "Simon & Garfunkel", 293),
                  new Song("Close To You", "Carpenters", 276),
                  new Song("Honesty", "Billy Joel", 231),
                  new Song("I Will Always Love You", "Whitney Houston", 273),
              };
              printSongs(songs);
          */

            var songs = new List<Song>();

            Console.Write("*****曲の追加*****");
            Console.WriteLine();


            while (true) {
                Console.Write("曲名：");//曲名を出力
                string? title = Console.ReadLine();      //入力された曲名を取得
                if (title.Equals("end", StringComparison.OrdinalIgnoreCase))
                    return;        //endが入力されたら登録終了
                Console.Write("アーティスト名：");//アーティスト名を出力
                string? artistname = Console.ReadLine();//入力されたアーティスト名を取得
                Console.Write("演奏時間(秒)：");//演奏時間を出力
                int length = int.Parse(Console.ReadLine());//入力された演奏時間を取得

                //Song song = new Song(title, artistname, length);
                Song song = new Song() {
                    Title = title,
                    ArtistName = artistname,
                    Length = length
                };
                songs.Add(song);
                Console.WriteLine();        //改行
            }

            printSongs(sing);
        }



        //2.1.4
        private static void printSongs(Song[] songs) {
            foreach (Song song in sing) {
                var minutes = song.Length / 60;
                var seconds = song.Length % 60;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {minutes}:{seconds:00}");
            }
            Console.WriteLine();
        }

    }
}

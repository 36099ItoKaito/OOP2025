using System.Diagnostics;
using System.Threading.Tasks;

namespace Section03 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            toolStripStatusLabel1.Text = "";
            await DoLongTimeWork();
            toolStripStatusLabel1.Text = "終了";
        }

        //非同期メソッド
        private async Task DoLongTimeWork() {
            await Task.Run(() => {
                System.Threading.Thread.Sleep(5000);
            });
        }
    }
}

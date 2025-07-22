using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {
                string xml = await hc.GetStringAsync(tbUrl.Text);
                XDocument xdoc = XDocument.Parse(xml);
                //var url = hc.OpenRead(tbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);   //RSSの取得

                //RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string)x.Element("title"),
                            Link = (string)x.Element("link"),
                        }).ToList();

                //リストボックスへタイトルを表示
                lbTitles.Items.Clear();
                foreach (var item in items) {
                    lbTitles.Items.Add(item.Title);
                }

                //lbTitles.Items.Clear();
                //items.ForEach(item => lbTitles.Items.Add(item.Title));

            }


        }

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            //int index = lbTitles.SelectedIndex;
            //if (index >= 0 && index < items.Count) {
            //    string link = items[index].Link                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 ;
            //    if (!string.IsNullOrEmpty(link)) {
            //        webView21.Source = new Uri(link);
            //    }
            //}

            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);


        }

        private void btGoBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
            btGoBack.Enabled = wvRssLink.CanGoBack;
        }

        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }

        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {

        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }
    }
}

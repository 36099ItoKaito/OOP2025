using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string> {
            {"��v", "https://news.yahoo.co.jp/rss/topics/top-picks.xml"},
            {"����", "https://news.yahoo.co.jp/rss/topics/domestic.xml"},
            {"����", "https://news.yahoo.co.jp/rss/topics/world.xml"},
            {"�o��", "https://news.yahoo.co.jp/rss/topics/business.xml"},
            {"�G���^��", "https://news.yahoo.co.jp/rss/topics/entertainment.xml"},
            {"�X�|�[�c", "https://news.yahoo.co.jp/rss/topics/sports.xml"},
            {"IT", "https://news.yahoo.co.jp/rss/topics/it.xml"},
            {"�Ȋw", "https://news.yahoo.co.jp/rss/topics/science.xml"},
            {"�n��", "https://news.yahoo.co.jp/rss/topics/local.xml"},
        };


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            tbUrl.DataSource = rssUrlDict.Select(k => k.Key).ToList();
            tbUrl.SelectedIndex = -1;
            GoForwardBtEnableSet();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(getRssUrl(tbUrl.Text));
                XDocument xdoc = XDocument.Parse(xml);
                //var url = hc.OpenRead(tbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);   //RSS�̎擾

                //RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                //���X�g�{�b�N�X�փ^�C�g����\��
                //lbTitles.Items.Clear();
                //foreach (var item in items) {
                //    lbTitles.Items.Add(item.Title);
                //}

                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title));

            }
        }

        private string getRssUrl(string str) {
            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];
            }
            return str;
        }

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��
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

        private void tbUrl_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void tbName_TextChanged(object sender, EventArgs e) {

        }

        private void btEntry_Click(object sender, EventArgs e) {
            string name = tbName.Text.Trim();
            string url = tbUrl.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(url)) {
                MessageBox.Show("���O�܂���URL����ł��B��������͂��Ă��������B");
                return;
            }

            if (rssUrlDict.ContainsKey(name)) {
                MessageBox.Show("���̖��O�͂��łɓo�^����Ă��܂��B");
            } else {
                rssUrlDict[name] = url;
                tbUrl.DataSource = null;
                tbUrl.DataSource = rssUrlDict.Keys.ToList();
                tbName.Clear();
                MessageBox.Show("�o�^�ł��܂���");
            }
        }

        private void btDelete_Click(object sender, EventArgs e) {
            string selectedName = tbUrl.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedName)) {
                MessageBox.Show("�폜����RSS��I�����Ă��������B");
                return;
            }

            if (rssUrlDict.ContainsKey(selectedName)) {
                rssUrlDict.Remove(selectedName);
                tbUrl.DataSource = null;
                tbUrl.DataSource = rssUrlDict.Keys.ToList();
                MessageBox.Show($"{selectedName} ��RSS���폜���܂����B");
            } else {
                MessageBox.Show("�I�����ꂽRSS��������܂���B");
            }
        }
    }
}

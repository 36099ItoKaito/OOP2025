using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Exercise01_WPF {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void ReadFileButton_Click(object sender, RoutedEventArgs e) {
            string filePath = "走れメロス.txt";

            ReadFileButton.IsEnabled = false;

            OutputTextBlock.Text = "読み込み中...\n\n";

            await ReadFileAsync(filePath);

            ReadFileButton.IsEnabled = true;
        }

        private async Task ReadFileAsync(string filePath) {
            try {
                if (!File.Exists(filePath)) {
                    MessageBox.Show($"ファイルが見つかりません: {filePath}\n\n実行ファイルと同じフォルダに配置してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    OutputTextBlock.Text = "";
                    return;
                }

                OutputTextBlock.Text = "";

                using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8)) {
                    string? line;
                    int lineCount = 0;

                    while ((line = await reader.ReadLineAsync()) != null) {
                        lineCount++;

                        OutputTextBlock.Text += line + Environment.NewLine;

                        if (lineCount % 10 == 0) {
                            await Task.Delay(1);
                        }
                    }
                }

            }
            catch (Exception ex) {
                OutputTextBlock.Text = "";
                MessageBox.Show($"エラー: {ex.Message}\n\nファイルパス: {filePath}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
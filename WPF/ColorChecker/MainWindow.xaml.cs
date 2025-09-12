using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace ColorChecker {
    public partial class MainWindow : Window {
        private List<MyColor> stockColors = new List<MyColor>();
        private List<MyColor> presetColors = new List<MyColor>();

        public MainWindow() {
            InitializeComponent();
            InitColorComboBox();
        }

        // スライダーの値変更時に呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            byte r = (byte)rSlider.Value;
            byte g = (byte)gSlider.Value;
            byte b = (byte)bSlider.Value;

            colorArea.Background = new SolidColorBrush(Color.FromRgb(r, g, b));

            // コンボボックスとリストボックスの選択を更新
            var currentColor = Color.FromRgb(r, g, b);
            UpdateComboBoxSelection(currentColor);
            UpdateListBoxSelection(currentColor);
        }

        // プリセット色をComboBoxに設定
        private void InitColorComboBox() {
            var colors = new List<MyColor>();

            // System.Windows.Media.Colorsクラスから全ての色を取得
            var colorProperties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Where(prop => prop.PropertyType == typeof(Color))
                .OrderBy(prop => prop.Name);

            foreach (var prop in colorProperties) {
                var color = (Color)prop.GetValue(null);
                var myColor = new MyColor {
                    Name = prop.Name,
                    Color = color
                };
                colors.Add(myColor);
            }

            // プリセット色をフィールドに保存
            presetColors = colors;

            // ComboBoxにすべての色を設定
            foreach (var color in colors) {
                colorSelectComboBox.Items.Add(color);
            }

            // ComboBoxの選択変更時に呼ばれるイベント
            colorSelectComboBox.SelectionChanged += ColorSelectComboBox_SelectionChanged;
        }

        // ComboBoxで色が選択されたとき
        private void ColorSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorSelectComboBox.SelectedItem != null) {
                var selectedColor = (MyColor)colorSelectComboBox.SelectedItem;

                rSlider.Value = selectedColor.Color.R;
                gSlider.Value = selectedColor.Color.G;
                bSlider.Value = selectedColor.Color.B;
            }
        }

        // STOCKボタンがクリックされたとき
        private void stockButton_Click(object sender, RoutedEventArgs e) {
            byte r = (byte)rSlider.Value;
            byte g = (byte)gSlider.Value;
            byte b = (byte)bSlider.Value;
            var color = Color.FromRgb(r, g, b);

            // 同じ色が既に存在するかチェック
            bool isDuplicate = stockColors.Any(existingColor =>
                existingColor.Color.R == color.R &&
                existingColor.Color.G == color.G &&
                existingColor.Color.B == color.B);

            if (isDuplicate) {
                MessageBox.Show("重複しています", "重複");
                return;
            }

            // コンボボックスの色と一致するかチック
            var matchingPresetColor = presetColors.FirstOrDefault(presetColor =>
                presetColor.Color.R == color.R &&
                presetColor.Color.G == color.G &&
                presetColor.Color.B == color.B);

            MyColor myColor;
            if (matchingPresetColor.Name != null) {
                // コンボボックス内の色の場合、その名前を使用
                myColor = new MyColor { Name = matchingPresetColor.Name, Color = color };
            } else {
                // カスタム色の場合、RGB値で表示
                myColor = new MyColor { Name = $"R:{r} G:{g} B:{b}", Color = color };
            }

            stockColors.Add(myColor);

            // 保存された色をListBoxに表示
            ListBoxItem item = new ListBoxItem {
                Content = myColor.Name,
            };
            stockList.Items.Add(item);
        }

        // ListBoxで色が選択されたとき
        private void stockList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (stockList.SelectedIndex >= 0) {
                var selectedColor = stockColors[stockList.SelectedIndex];

                rSlider.Value = selectedColor.Color.R;
                gSlider.Value = selectedColor.Color.G;
                bSlider.Value = selectedColor.Color.B;
            }
        }

        // コンボボックスの選択を更新するメソッド
        private void UpdateComboBoxSelection(Color currentColor) {
            colorSelectComboBox.SelectionChanged -= ColorSelectComboBox_SelectionChanged;

            // ー致する色を検索
            var matchingColor = presetColors.FirstOrDefault(presetColor =>
                presetColor.Color.R == currentColor.R &&
                presetColor.Color.G == currentColor.G &&
                presetColor.Color.B == currentColor.B);

            if (matchingColor.Name != null) {
                // ー致する色がある場合、コンボボックスで選択
                colorSelectComboBox.SelectedItem = matchingColor;
            } else {
                // 一致する色がない場合、選択を解除
                colorSelectComboBox.SelectedIndex = -1;
            }

            // イベントハンドラを再設定
            colorSelectComboBox.SelectionChanged += ColorSelectComboBox_SelectionChanged;
        }

        // リストボックスの選択を更新するメソッド
        private void UpdateListBoxSelection(Color currentColor) {
            stockList.SelectionChanged -= stockList_SelectionChanged;

            // 一致する色を検索
            int matchingIndex = -1;
            for (int i = 0; i < stockColors.Count; i++) {
                if (stockColors[i].Color.R == currentColor.R &&
                    stockColors[i].Color.G == currentColor.G &&
                    stockColors[i].Color.B == currentColor.B) {
                    matchingIndex = i;
                    break;
                }
            }

            // 一致する色がある場合は選択、ない場合は選択解除
            stockList.SelectedIndex = matchingIndex;

            // イベントハンドラを再設定
            stockList.SelectionChanged += stockList_SelectionChanged;
        }
    }
}
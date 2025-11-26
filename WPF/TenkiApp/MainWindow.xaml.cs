using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TenkiApp.Services;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private Dictionary<string, WeatherCardInfo> _weatherCards = new Dictionary<string, WeatherCardInfo>();
        private List<CityData> _allCities = new List<CityData>();
        private bool _isSearchBoxPlaceholder = true;

        private readonly WeatherService _weatherService = new WeatherService();
        private readonly WeatherParser _weatherParser = new WeatherParser();

        public MainWindow() {
            InitializeComponent();

            InitializeCityData();
            SetupPlaceholder();
            PopulateCityCards();
            UpdateDateDisplay();

            // 東京を初期表示
            LoadWeatherData(35.6762, 139.6503, "東京");

            // 全カード更新（非同期）
            _ = LoadAllCityWeather();
        }

        private void UpdateDateDisplay() {
            DateTime today = DateTime.Today;
            string dayOfWeek = today.ToString("dddd", new System.Globalization.CultureInfo("ja-JP"));
            DateText.Text = $"{today.Month}月{today.Day}日 ({dayOfWeek})";
        }

        // ==================== 都市カード生成 ====================
        private void PopulateCityCards() {
            var cities = new (string name, double lat, double lon)[] {
                ("札幌市", 43.0642, 141.3469),
                ("仙台市", 38.2682, 140.8694),
                ("東京", 35.6762, 139.6503),
                ("横浜市", 35.4437, 139.6380),
                ("名古屋市", 35.1815, 136.9066),
                ("金沢市", 36.5946, 136.6256),
                ("京都市", 35.0116, 135.7681),
                ("大阪市", 34.6937, 135.5023),
                ("神戸市", 34.6901, 135.1955),
                ("広島市", 34.3853, 132.4553),
                ("福岡市", 33.5904, 130.4017),
                ("那覇市", 26.2124, 127.6809)
            };

            foreach (var c in cities) {
                var weatherTb = new TextBlock {
                    Text = "❓",
                    FontSize = 36,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 8, 0, 0)
                };
                var cityNameTb = new TextBlock {
                    Text = c.name,
                    FontSize = 15,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                var tempTb = new TextBlock {
                    Text = "-- / --",
                    FontSize = 14,
                    FontWeight = FontWeights.SemiBold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(162, 155, 254))
                };
                var precipTb = new TextBlock {
                    Text = "--%",
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(116, 185, 255)),
                    Margin = new Thickness(0, 5, 0, 0)
                };

                AddCityCard(c.name, c.lat, c.lon, cityNameTb, weatherTb, tempTb, precipTb);
            }
        }

        private void AddCityCard(string cityName, double lat, double lon,
            TextBlock cityNameTb, TextBlock weather, TextBlock temp, TextBlock precip) {

            var border = new Border {
                Style = (Style)TryFindResource("ModernCityCard"),
                Tag = $"{lat},{lon},{cityName}"
            };
            border.MouseLeftButtonUp += WeatherCard_Click;

            // ホバーアニメーション
            border.MouseEnter += (s, e) => {
                var animation = new DoubleAnimation {
                    To = 1.05,
                    Duration = TimeSpan.FromMilliseconds(200)
                };
                var scaleTransform = new ScaleTransform(1, 1);
                border.RenderTransform = scaleTransform;
                border.RenderTransformOrigin = new Point(0.5, 0.5);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            };
            border.MouseLeave += (s, e) => {
                var animation = new DoubleAnimation {
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(200)
                };
                if (border.RenderTransform is ScaleTransform st) {
                    st.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                    st.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
                }
            };

            var stack = new StackPanel();
            stack.Children.Add(cityNameTb);
            stack.Children.Add(weather);
            stack.Children.Add(temp);
            stack.Children.Add(precip);

            border.Child = stack;
            CityCardsPanel.Children.Add(border);

            if (!_weatherCards.ContainsKey(cityName)) {
                _weatherCards.Add(cityName, new WeatherCardInfo {
                    Lat = lat,
                    Lon = lon,
                    WeatherText = weather,
                    TempText = temp,
                    PrecipText = precip,
                    CardBorder = border
                });
            }
        }

        // ==================== 全カード更新 ====================
        private async Task LoadAllCityWeather() {
            var tasks = new List<Task>();
            foreach (var card in _weatherCards.Values) {
                tasks.Add(LoadCityWeatherCard(card));
            }
            await Task.WhenAll(tasks);
        }

        private async Task LoadCityWeatherCard(WeatherCardInfo cardInfo) {
            try {
                var data = await _weatherService.GetCityCardWeatherAsync(cardInfo.Lat, cardInfo.Lon);
                var current = data.RootElement.GetProperty("current");
                var daily = data.RootElement.GetProperty("daily");

                int weatherCode = current.GetProperty("weather_code").GetInt32();
                cardInfo.WeatherText.Text = GetWeatherEmoji(weatherCode);

                if (daily.TryGetProperty("temperature_2m_max", out var maxArr) && maxArr.GetArrayLength() > 0 &&
                    daily.TryGetProperty("temperature_2m_min", out var minArr) && minArr.GetArrayLength() > 0) {

                    int maxTemp = (int)Math.Round(maxArr[0].GetDouble());
                    int minTemp = (int)Math.Round(minArr[0].GetDouble());
                    cardInfo.TempText.Text = $"{maxTemp}° / {minTemp}°";
                }

                if (daily.TryGetProperty("precipitation_probability_max", out var precipArr) && precipArr.GetArrayLength() > 0) {
                    int precip = precipArr[0].GetInt32();
                    cardInfo.PrecipText.Text = precip > 0 ? $"💧 {precip}%" : "💧 0%";
                }
            }
            catch {
                cardInfo.WeatherText.Text = "❓";
                cardInfo.TempText.Text = "-- / --";
                cardInfo.PrecipText.Text = "💧 --%";
            }
        }

        // ==================== カードクリック ====================
        private void WeatherCard_Click(object sender, MouseButtonEventArgs e) {
            if (sender is Border border && border.Tag is string data) {
                var parts = data.Split(',');
                if (parts.Length >= 3 &&
                    double.TryParse(parts[0], out double lat) &&
                    double.TryParse(parts[1], out double lon)) {
                    LoadWeatherData(lat, lon, parts[2]);
                }
            }
        }

        // ==================== 検索機能 ====================
        private void SetupPlaceholder() {
            SearchTextBox.Foreground = new SolidColorBrush(Color.FromRgb(162, 155, 254));
            SearchTextBox.Text = "都市名で検索...";
            _isSearchBoxPlaceholder = true;
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (_isSearchBoxPlaceholder) {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground = Brushes.White;
                _isSearchBoxPlaceholder = false;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text)) {
                SetupPlaceholder();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (_isSearchBoxPlaceholder) return;

            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText)) {
                SearchResultsPopup.IsOpen = false;
                return;
            }

            // 曖昧検索（部分一致）
            var results = _allCities
                .Where(c => c.NameLower.Contains(searchText) ||
                           c.PrefLower.Contains(searchText) ||
                           c.Name.Contains(SearchTextBox.Text.Trim()))
                .Take(15)
                .ToList();

            if (results.Any()) {
                SearchResultsList.ItemsSource = results;
                SearchResultsPopup.IsOpen = true;
            } else {
                SearchResultsPopup.IsOpen = false;
            }
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                SearchButton_Click(sender, e);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            if (_isSearchBoxPlaceholder) return;

            string searchText = SearchTextBox.Text.Trim();
            var city = _allCities.FirstOrDefault(c =>
                c.Name.Equals(searchText, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(searchText));

            if (city != null) {
                LoadWeatherData(city.Latitude, city.Longitude, city.Name);
                SearchResultsPopup.IsOpen = false;
            }
        }

        private void SearchResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SearchResultsList.SelectedItem is CityData city) {
                LoadWeatherData(city.Latitude, city.Longitude, city.Name);
                SearchResultsPopup.IsOpen = false;
                SearchTextBox.Text = city.Name;
                _isSearchBoxPlaceholder = false;
            }
        }

        // ==================== 天気データ読み込み ====================
        private async void LoadWeatherData(double latitude, double longitude, string cityName) {
            try {
                LoadingText.Text = "読み込み中...";
                CityNameText.Text = cityName;

                var data = await _weatherService.GetWeatherAsync(latitude, longitude);
                var current = _weatherParser.ParseCurrent(data);

                CurrentTempText.Text = $"{current.Temperature:F1}°";
                WeatherDescText.Text = GetWeatherDescription(current.WeatherCode);
                HumidityText.Text = $"{current.Humidity:F0}%";
                WindSpeedText.Text = $"{current.WindSpeed:F1} m/s";
                MaxTempText.Text = $"{current.MaxTemp:F1}°";
                MinTempText.Text = $"{current.MinTemp:F1}°";

                var hourly = _weatherParser.ParseHourly(data);
                HourlyForecastList.ItemsSource = hourly;

                LoadingText.Text = "";
            }
            catch (Exception ex) {
                LoadingText.Text = $"エラー: {ex.Message}";
            }
        }

        // ==================== ヘルパー ====================
        private string GetWeatherDescription(int code) {
            return code switch {
                0 => "快晴 ☀️",
                1 or 2 => "晴れ 🌤️",
                3 => "曇り ☁️",
                45 or 48 => "霧 🌫️",
                51 or 53 or 55 => "霧雨 🌦️",
                61 or 63 or 65 => "雨 🌧️",
                71 or 73 or 75 => "雪 ❄️",
                77 => "みぞれ 🌨️",
                80 or 81 or 82 => "にわか雨 🌦️",
                85 or 86 => "にわか雪 🌨️",
                95 => "雷雨 ⛈️",
                96 or 99 => "雷雨（雹）⛈️",
                _ => "不明 ❓"
            };
        }

        private string GetWeatherEmoji(int code) {
            return code switch {
                0 => "☀️",
                1 or 2 => "🌤️",
                3 => "☁️",
                45 or 48 => "🌫️",
                51 or 53 or 55 => "🌦️",
                61 or 63 or 65 => "🌧️",
                71 or 73 or 75 => "❄️",
                77 => "🌨️",
                80 or 81 or 82 => "🌦️",
                85 or 86 => "🌨️",
                95 => "⛈️",
                96 or 99 => "⛈️",
                _ => "❓"
            };
        }

        // ==================== 市町村データ ====================
        private void InitializeCityData() {
            _allCities = new List<CityData> {
                // 北海道
                new CityData { Name = "札幌市", Prefecture = "北海道", Latitude = 43.0642, Longitude = 141.3469 },
                new CityData { Name = "函館市", Prefecture = "北海道", Latitude = 41.7688, Longitude = 140.7288 },
                new CityData { Name = "旭川市", Prefecture = "北海道", Latitude = 43.7706, Longitude = 142.3650 },
                new CityData { Name = "帯広市", Prefecture = "北海道", Latitude = 42.9222, Longitude = 143.2044 },
                
                // 東北
                new CityData { Name = "青森市", Prefecture = "青森県", Latitude = 40.8244, Longitude = 140.7400 },
                new CityData { Name = "仙台市", Prefecture = "宮城県", Latitude = 38.2682, Longitude = 140.8694 },
                new CityData { Name = "秋田市", Prefecture = "秋田県", Latitude = 39.7186, Longitude = 140.1024 },
                new CityData { Name = "山形市", Prefecture = "山形県", Latitude = 38.2404, Longitude = 140.3633 },
                new CityData { Name = "福島市", Prefecture = "福島県", Latitude = 37.7500, Longitude = 140.4676 },
                
                // 関東
                new CityData { Name = "東京", Prefecture = "東京都", Latitude = 35.6762, Longitude = 139.6503 },
                new CityData { Name = "横浜市", Prefecture = "神奈川県", Latitude = 35.4437, Longitude = 139.6380 },
                new CityData { Name = "鎌倉市", Prefecture = "神奈川県", Latitude = 35.3197, Longitude = 139.5488 },
                new CityData { Name = "藤沢市", Prefecture = "神奈川県", Latitude = 35.3378, Longitude = 139.4908 },
                new CityData { Name = "江の島", Prefecture = "神奈川県", Latitude = 35.3031, Longitude = 139.4850 },
                new CityData { Name = "さいたま市", Prefecture = "埼玉県", Latitude = 35.8617, Longitude = 139.6455 },
                new CityData { Name = "深谷市", Prefecture = "埼玉県", Latitude = 36.1970, Longitude = 139.2818 },
                new CityData { Name = "熊谷市", Prefecture = "埼玉県", Latitude = 36.1469, Longitude = 139.3879 },
                new CityData { Name = "千葉市", Prefecture = "千葉県", Latitude = 35.6074, Longitude = 140.1065 },
                new CityData { Name = "水戸市", Prefecture = "茨城県", Latitude = 36.3418, Longitude = 140.4468 },
                new CityData { Name = "宇都宮市", Prefecture = "栃木県", Latitude = 36.5658, Longitude = 139.8836 },
                new CityData { Name = "足利市", Prefecture = "栃木県", Latitude = 36.3311, Longitude = 139.4447 },
                new CityData { Name = "前橋市", Prefecture = "群馬県", Latitude = 36.3911, Longitude = 139.0608 },
                new CityData { Name = "邑楽町", Prefecture = "群馬県", Latitude = 36.2570, Longitude = 139.4673 },
                new CityData { Name = "甘楽町", Prefecture = "群馬県", Latitude = 36.2520, Longitude = 138.9324 },
                new CityData { Name = "伊勢崎市", Prefecture = "群馬県", Latitude = 36.3217, Longitude = 139.1866 },
                new CityData { Name = "千代田町", Prefecture = "群馬県", Latitude = 36.2167, Longitude = 139.4172 },
                new CityData { Name = "太田市", Prefecture = "群馬県", Latitude = 36.2917, Longitude = 139.3875 },
                
                // 中部
                new CityData { Name = "新潟市", Prefecture = "新潟県", Latitude = 37.9161, Longitude = 139.0364 },
                new CityData { Name = "富山市", Prefecture = "富山県", Latitude = 36.6953, Longitude = 137.2113 },
                new CityData { Name = "金沢市", Prefecture = "石川県", Latitude = 36.5946, Longitude = 136.6256 },
                new CityData { Name = "福井市", Prefecture = "福井県", Latitude = 36.0652, Longitude = 136.2216 },
                new CityData { Name = "甲府市", Prefecture = "山梨県", Latitude = 35.6636, Longitude = 138.5684 },
                new CityData { Name = "長野市", Prefecture = "長野県", Latitude = 36.6513, Longitude = 138.1811 },
                new CityData { Name = "岐阜市", Prefecture = "岐阜県", Latitude = 35.4232, Longitude = 136.7605 },
                new CityData { Name = "静岡市", Prefecture = "静岡県", Latitude = 34.9769, Longitude = 138.3830 },
                new CityData { Name = "名古屋市", Prefecture = "愛知県", Latitude = 35.1815, Longitude = 136.9066 },
                
                // 近畿
                new CityData { Name = "津市", Prefecture = "三重県", Latitude = 34.7303, Longitude = 136.5086 },
                new CityData { Name = "大津市", Prefecture = "滋賀県", Latitude = 35.0045, Longitude = 135.8686 },
                new CityData { Name = "京都市", Prefecture = "京都府", Latitude = 35.0116, Longitude = 135.7681 },
                new CityData { Name = "大阪市", Prefecture = "大阪府", Latitude = 34.6937, Longitude = 135.5023 },
                new CityData { Name = "神戸市", Prefecture = "兵庫県", Latitude = 34.6901, Longitude = 135.1955 },
                new CityData { Name = "奈良市", Prefecture = "奈良県", Latitude = 34.6851, Longitude = 135.8050 },
                new CityData { Name = "和歌山市", Prefecture = "和歌山県", Latitude = 34.2261, Longitude = 135.1675 },
                
                // 中国
                new CityData { Name = "鳥取市", Prefecture = "鳥取県", Latitude = 35.5014, Longitude = 134.2382 },
                new CityData { Name = "松江市", Prefecture = "島根県", Latitude = 35.4723, Longitude = 133.0506 },
                new CityData { Name = "岡山市", Prefecture = "岡山県", Latitude = 34.6551, Longitude = 133.9195 },
                new CityData { Name = "広島市", Prefecture = "広島県", Latitude = 34.3853, Longitude = 132.4553 },
                new CityData { Name = "山口市", Prefecture = "山口県", Latitude = 34.1858, Longitude = 131.4706 },
                
                // 四国
                new CityData { Name = "徳島市", Prefecture = "徳島県", Latitude = 34.0658, Longitude = 134.5594 },
                new CityData { Name = "高松市", Prefecture = "香川県", Latitude = 34.3401, Longitude = 134.0434 },
                new CityData { Name = "松山市", Prefecture = "愛媛県", Latitude = 33.8416, Longitude = 132.7657 },
                new CityData { Name = "高知市", Prefecture = "高知県", Latitude = 33.5597, Longitude = 133.5311 },
                
                // 九州
                new CityData { Name = "福岡市", Prefecture = "福岡県", Latitude = 33.5904, Longitude = 130.4017 },
                new CityData { Name = "佐賀市", Prefecture = "佐賀県", Latitude = 33.2495, Longitude = 130.2988 },
                new CityData { Name = "長崎市", Prefecture = "長崎県", Latitude = 32.7503, Longitude = 129.8779 },
                new CityData { Name = "熊本市", Prefecture = "熊本県", Latitude = 32.8031, Longitude = 130.7079 },
                new CityData { Name = "大分市", Prefecture = "大分県", Latitude = 33.2382, Longitude = 131.6126 },
                new CityData { Name = "宮崎市", Prefecture = "宮崎県", Latitude = 31.9077, Longitude = 131.4202 },
                new CityData { Name = "鹿児島市", Prefecture = "鹿児島県", Latitude = 31.5966, Longitude = 130.5571 },
                new CityData { Name = "那覇市", Prefecture = "沖縄県", Latitude = 26.2124, Longitude = 127.6809 }
            };
        }

        // ==================== データクラス ====================
        public class CityData {
            public string Name { get; set; } = "";
            public string Prefecture { get; set; } = "";
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string NameLower => Name.ToLower();
            public string PrefLower => Prefecture.ToLower();
        }

        public class WeatherCardInfo {
            public double Lat { get; set; }
            public double Lon { get; set; }
            public TextBlock WeatherText { get; set; } = null!;
            public TextBlock TempText { get; set; } = null!;
            public TextBlock PrecipText { get; set; } = null!;
            public Border CardBorder { get; set; } = null!;
        }
    }
}
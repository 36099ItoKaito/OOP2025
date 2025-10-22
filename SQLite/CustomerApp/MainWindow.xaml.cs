using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SQLite;

namespace CustomerApp;

public class Customer : INotifyPropertyChanged {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    private string _name = string.Empty;
    public string Name {
        get => _name;
        set {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private string _phoneNumber = string.Empty;
    public string PhoneNumber {
        get => _phoneNumber;
        set {
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }

    private string _address = string.Empty;
    public string Address {
        get => _address;
        set {
            _address = value;
            OnPropertyChanged(nameof(Address));
        }
    }

    private byte[]? _imageData;
    public byte[]? ImageData {
        get => _imageData;
        set {
            _imageData = value;
            OnPropertyChanged(nameof(ImageData));
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    [Ignore]
    public BitmapImage? ImageSource {
        get {
            if (ImageData == null || ImageData.Length == 0) return null;
            return ByteArrayToImage(ImageData);
        }
    }

    private static BitmapImage? ByteArrayToImage(byte[] data) {
        if (data == null || data.Length == 0) return null;

        var image = new BitmapImage();
        using (var mem = new MemoryStream(data)) {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = mem;
            image.EndInit();
        }
        image.Freeze();
        return image;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public partial class MainWindow : Window {
    private readonly SQLiteConnection _database;
    public ObservableCollection<Customer> Customers { get; set; }
    private byte[]? _imageData;

    public MainWindow() {
        InitializeComponent();

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CustomerApp.db");

        _database = new SQLiteConnection(dbPath);
        _database.CreateTable<Customer>();

        Customers = new ObservableCollection<Customer>();
        LoadCustomers();

        CustomerList.ItemsSource = Customers;
    }

    private void LoadCustomers() {
        var customers = _database.Table<Customer>().ToList();
        Customers.Clear();
        foreach (var customer in customers) {
            Customers.Add(customer);
        }
    }

    private void CustomerList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
        if (CustomerList.SelectedItem is Customer customer) {
            NameTextBox.Text = customer.Name;
            PhoneTextBox.Text = customer.PhoneNumber;
            AddressTextBox.Text = customer.Address;
            _imageData = customer.ImageData;

            if (customer.ImageData != null && customer.ImageData.Length > 0) {
                CustomerImage.Source = ByteArrayToImage(customer.ImageData);
            } else {
                CustomerImage.Source = null;
            }
        }
    }

    private void NewButton_Click(object sender, RoutedEventArgs e) {
        var name = NameTextBox.Text.Trim();
        var phone = PhoneTextBox.Text.Trim();
        var address = AddressTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name)) {
            MessageBox.Show("名前を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(phone)) {
            MessageBox.Show("電話番号を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(address)) {
            MessageBox.Show("住所を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (_imageData == null || _imageData.Length == 0) {
            MessageBox.Show("画像を選択してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newCustomer = new Customer {
            Name = name,
            PhoneNumber = phone,
            Address = address,
            ImageData = _imageData
        };

        _database.Insert(newCustomer);
        MessageBox.Show("新規登録しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);

        LoadCustomers();
        ClearForm();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        if (CustomerList.SelectedItem is not Customer customer) {
            MessageBox.Show("更新する顧客を選択してください。", "選択エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var name = NameTextBox.Text.Trim();
        var phone = PhoneTextBox.Text.Trim();
        var address = AddressTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name)) {
            MessageBox.Show("名前を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(phone)) {
            MessageBox.Show("電話番号を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(address)) {
            MessageBox.Show("住所を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (_imageData == null || _imageData.Length == 0) {
            MessageBox.Show("画像を選択してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        customer.Name = name;
        customer.PhoneNumber = phone;
        customer.Address = address;
        customer.ImageData = _imageData;

        _database.Update(customer);
        MessageBox.Show("更新しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);

        LoadCustomers();
        ClearForm();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        if (CustomerList.SelectedItem is not Customer customer) {
            MessageBox.Show("削除する顧客を選択してください。", "選択エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"「{customer.Name}」を削除してもよろしいですか?",
            "削除確認",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes) {
            _database.Delete(customer);
            MessageBox.Show("削除しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadCustomers();
            ClearForm();
        }
    }

    private void SelectImageButton_Click(object sender, RoutedEventArgs e) {
        var dialog = new OpenFileDialog {
            Filter = "画像ファイル|*.jpg;*.jpeg;*.png;*.bmp;*.gif|すべてのファイル|*.*",
            Title = "画像を選択"
        };

        if (dialog.ShowDialog() == true) {
            try {
                _imageData = File.ReadAllBytes(dialog.FileName);
                CustomerImage.Source = new BitmapImage(new Uri(dialog.FileName));
            }
            catch (Exception ex) {
                MessageBox.Show($"画像の読み込みに失敗しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ClearImageButton_Click(object sender, RoutedEventArgs e) {
        _imageData = null;
        CustomerImage.Source = null;
    }

    private void ClearForm() {
        NameTextBox.Text = string.Empty;
        PhoneTextBox.Text = string.Empty;
        AddressTextBox.Text = string.Empty;
        _imageData = null;
        CustomerImage.Source = null;
        CustomerList.SelectedItem = null;
    }

    private BitmapImage? ByteArrayToImage(byte[] data) {
        if (data == null || data.Length == 0) return null;

        var image = new BitmapImage();
        using (var mem = new MemoryStream(data)) {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = mem;
            image.EndInit();
        }
        image.Freeze();
        return image;
    }

    protected override void OnClosed(EventArgs e) {
        _database?.Close();
        base.OnClosed(e);
    }
}
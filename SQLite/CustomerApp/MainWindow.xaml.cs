using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SQLite;

namespace CustomerApp;

public class Customer {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public byte[]? ImageData { get; set; }
}

public partial class MainWindow : Window {

    public MainWindow() {
        
    }
}
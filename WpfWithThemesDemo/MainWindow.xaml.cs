using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfWithThemesDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            string[] themes = ["light", "dark"];
            ListOfThemes.ItemsSource = themes;
            ListOfThemes.SelectionChanged += ListOfThemesOnSelectionChanged;
            ListOfThemes.SelectedIndex = 0;
        };
    }

    private void ListOfThemesOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var theme = ListOfThemes.SelectedItem as string;
        var uri = new Uri($"{theme}.xaml", UriKind.Relative);
        var resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
        Resources.Clear();
        Resources.MergedDictionaries.Add(resourceDictionary);
    }

    private static IEnumerable<string> SetListOfThemes()
    {
        //var path = Path.Combine(Environment.CurrentDirectory, "Themes");
        return Directory.GetFiles("pack://application:,,,/Themes")
            .Select(fileName => new FileInfo(fileName))
            .Select(file => file.Name.Replace(file.Extension, ""));
    }
}
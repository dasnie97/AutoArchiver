using AutoArchiver.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoArchiver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MainWindow> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IAppSettingsManager _appSettingsManager;

        public MainWindow(  IConfiguration configuration,
                            ILogger<MainWindow> logger,
                            IHostApplicationLifetime applicationLifetime,
                            IAppSettingsManager appSettingsManager)
        {
            InitializeComponent();
            _configuration = configuration;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _appSettingsManager = appSettingsManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var inputDir1 = _appSettingsManager.GetAppSettings().Config.Directories.Input;

            _appSettingsManager.GetAppSettings().Config.Directories.Input.Add("Dupa");
            _appSettingsManager.SaveAppSettings();

            inputDir1 = _appSettingsManager.GetAppSettings().Config.Directories.Input;

            //JsonSerializer.Deserialize<AppSettings>(_configuration.ToString())

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _applicationLifetime.StopApplication();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ShowDialog();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void AddPathButton_Click(object sender, RoutedEventArgs e)
        {
            string chosenPath = FileBrowserDialog.ChooseFolderUsingFolderExplorer();
            if (chosenPath == string.Empty)
            {
                return;
            }

            Grid grid = new Grid();

            ColumnDefinition pathColumn = new ColumnDefinition();
            ColumnDefinition fileExplorerButtonColumn = new ColumnDefinition();
            ColumnDefinition removePathButtonColumn = new ColumnDefinition();

            pathColumn.Width = new GridLength(14, GridUnitType.Star);
            fileExplorerButtonColumn.Width = new GridLength(1, GridUnitType.Star);
            removePathButtonColumn.Width = new GridLength(1, GridUnitType.Star);

            grid.ColumnDefinitions.Add(pathColumn);
            grid.ColumnDefinitions.Add(fileExplorerButtonColumn);
            grid.ColumnDefinitions.Add(removePathButtonColumn);

            TextBox pathTextBox = new TextBox();
            pathTextBox.Text = chosenPath;
            pathTextBox.IsReadOnly = true;

            Button fileExplorerButton = new Button();
            fileExplorerButton.Width = 40;
            fileExplorerButton.Height = 20;
            fileExplorerButton.Content = "Edytuj";
            fileExplorerButton.Click += FileExplorerButtonClick;

            Button removePathButton = new Button();
            removePathButton.Width = 40;
            removePathButton.Height = 20;
            removePathButton.Content = "Usun";
            removePathButton.Click += RemovePathButtonClick;

            grid.Children.Add(pathTextBox);
            grid.Children.Add(fileExplorerButton);
            grid.Children.Add(removePathButton);

            Grid.SetColumn(pathTextBox, 0);
            Grid.SetColumn(fileExplorerButton, 1);
            Grid.SetColumn(removePathButton, 2);
                        
            Test.Children.Add(grid);
        }

        private void FileExplorerButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid grid = (Grid)button.Parent;
            TextBox textBox = (TextBox)grid.Children[0];

            string chosenPath = FileBrowserDialog.ChooseFolderUsingFolderExplorer();
            if (chosenPath != string.Empty)
            {
                textBox.Text = chosenPath;
            }
        }

        private void RemovePathButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid grid = (Grid)button.Parent;
            Test.Children.Remove(grid) ;
        }
    }
}

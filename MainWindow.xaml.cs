using AutoArchiver.Helpers;
using AutoArchiver.Windows;
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

        #region Ctor

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

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfigurationFromAppSettings();
        }

        private void AddPathButton_Click(object sender, RoutedEventArgs e)
        {
            string chosenPath = FileBrowserDialog.ChooseFolderUsingFolderExplorer();
            if (chosenPath == string.Empty)
            {
                return;
            }
            CreateNewArchivePath(chosenPath);
        }

        private void AddExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
            //CreateNewArchiveExtension("Ext");
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

        private void RemoveGrid(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            Grid grid = (Grid)element.Parent;
            if (grid.Parent != null)
            {
                Panel panel = (Panel)grid.Parent;
                panel.Children.Remove(grid);
            }
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveConfigurationToAppSettings();
        }

        #endregion

        #region IHost

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

        #endregion

        #region Privates

        private void CreateNewArchivePath(string chosenPath)
        {
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
            removePathButton.Click += RemoveGrid;

            grid.Children.Add(pathTextBox);
            grid.Children.Add(fileExplorerButton);
            grid.Children.Add(removePathButton);

            Grid.SetColumn(pathTextBox, 0);
            Grid.SetColumn(fileExplorerButton, 1);
            Grid.SetColumn(removePathButton, 2);

            ArchivePathsStackPanel.Children.Add(grid);
        }

        private void CreateNewArchiveExtension(string extension)
        {
            Grid grid = new Grid();

            ColumnDefinition pathColumn = new ColumnDefinition();
            ColumnDefinition removeExtensionCheckboxColumn = new ColumnDefinition();

            pathColumn.Width = new GridLength(5, GridUnitType.Star);
            removeExtensionCheckboxColumn.Width = new GridLength(1, GridUnitType.Star);

            grid.ColumnDefinitions.Add(pathColumn);
            grid.ColumnDefinitions.Add(removeExtensionCheckboxColumn);

            CheckBox extenstionCheckBox = new CheckBox();
            extenstionCheckBox.Content = extension;

            Button removeExtensionButton = new Button();
            removeExtensionButton.Width = 30;
            removeExtensionButton.Height = 19;
            removeExtensionButton.Content = "Usun";
            removeExtensionButton.Click += RemoveGrid;

            grid.Children.Add(extenstionCheckBox);
            grid.Children.Add(removeExtensionButton);

            Grid.SetColumn(extenstionCheckBox, 0);
            Grid.SetColumn(removeExtensionButton, 1);

            ArchiveExtensionsStackPanel.Children.Add(grid);
        }

        private void SaveConfigurationToAppSettings()
        {
            _appSettingsManager.GetAppSettings().Config.Directories.Input.Clear();
            _appSettingsManager.GetAppSettings().Config.Extensions.Clear();

            foreach (Grid grid in ArchivePathsStackPanel.Children)
            {
                TextBox pathTextBox = (TextBox)grid.Children[0];
                _appSettingsManager.GetAppSettings().Config.Directories.Input.Add(pathTextBox.Text);
            }

            foreach (Grid grid in ArchiveExtensionsStackPanel.Children)
            {
                CheckBox archiveCheckBox = (CheckBox)grid.Children[0];
                _appSettingsManager.GetAppSettings().Config.Extensions.Add((string)archiveCheckBox.Content);
            }

            _appSettingsManager.SaveAppSettings();
        }

        private void LoadConfigurationFromAppSettings()
        {
            foreach (string value in _appSettingsManager.GetAppSettings().Config.Directories.Input)
            {
                CreateNewArchivePath(value);
            }

            foreach (string value in _appSettingsManager.GetAppSettings().Config.Extensions)
            {
                CreateNewArchiveExtension(value);
            }
        }
        #endregion

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.OriginalSource;

            if (txtBox == element || popup == element || element.Parent == popup)
                return;

            popup.IsOpen = !string.IsNullOrEmpty(txtBox.Text);

            //TODO: finish adding archive extension using popup
        }
    }
}

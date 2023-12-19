using AutoArchiver.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var background = "#FFE2DADA";
            Color color = (Color)ColorConverter.ConvertFromString(background);
            var brush = new SolidColorBrush(color);
            textBox1.Background = brush;
        }

        private void textBox_MouseLeave(object sender, MouseEventArgs e)
        {
            textBox1.Background = null;
        }

        private void textBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBox1.IsReadOnly = false;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBox1.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = new TextBox();
            textBox.Text = "";
            
            Test.Children.Add(textBox);

            var inputDir1 = _appSettingsManager.GetAppSettings().DirectoryConfig.InputDirectories;

            _appSettingsManager.GetAppSettings().DirectoryConfig.InputDirectories.Add("Dupa");
            _appSettingsManager.SaveAppSettings();

            inputDir1 = _appSettingsManager.GetAppSettings().DirectoryConfig.InputDirectories;

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
    }
}

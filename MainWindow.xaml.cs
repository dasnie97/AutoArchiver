using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public MainWindow(  IConfiguration configuration,
                            ILogger<MainWindow> logger,
                            IHostApplicationLifetime applicationLifetime)
        {
            InitializeComponent();
            _configuration = configuration;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
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
            var directoryConfig = _configuration.GetSection("DirectoryConfig");
            string inputDir = directoryConfig.GetValue<string>("InputDir");
            textBox.Text = "";
            
            Test.Children.Add(textBox);


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

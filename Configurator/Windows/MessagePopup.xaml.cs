using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoArchiver.Windows
{
    /// <summary>
    /// Interaction logic for MessagePopup.xaml
    /// </summary>
    public partial class MessagePopup : Window
    {
        public MessagePopup()
        {
            InitializeComponent();
            GotFocus += MainWindow_GotFocus;
        }
        void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.OriginalSource;

            if (txtBox == element || popup == element || element.Parent == popup)
                return;

            popup.IsOpen = !string.IsNullOrEmpty(txtBox.Text);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}

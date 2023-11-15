﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoArchiver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}

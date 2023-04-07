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

namespace LastPraktika
{
    /// <summary>
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Window
    {
        public Sklad()
        {
            InitializeComponent();
            PageFrame.Content = new CategoryPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Form_factor();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new SpecificationWindow();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new CategoryPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ProductPage();
        }
    }
}

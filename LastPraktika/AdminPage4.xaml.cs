using LastPraktika.newshopDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace LastPraktika
{
    /// <summary>
    /// Логика взаимодействия для AdminPage4.xaml
    /// </summary>
    public partial class AdminPage4 : Page
    {
        shopsTableAdapter shop = new shopsTableAdapter();
        public AdminPage4()
        {
            InitializeComponent();
            MyData.ItemsSource = shop.GetData();
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                xBOX.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(xBOX.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                shop.InsertQuery(xBOX.Text);
                xBOX.Text = "";
                MyData.ItemsSource = shop.GetData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (xBOX.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                shop.UpdateQuery(xBOX.Text,Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                xBOX.Text = "";
                MyData.ItemsSource = shop.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //DELETE
        {
            shop.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            xBOX.Text = "";
            MyData.ItemsSource = shop.GetData();
        }
    }
}

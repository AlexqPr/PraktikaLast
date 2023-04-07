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
    /// Логика взаимодействия для AdminPage1.xaml
    /// </summary>
    public partial class AdminPage1 : Page
    {
        rolesTableAdapter role = new rolesTableAdapter();
        public AdminPage1()
        {
            InitializeComponent();
            MyData.ItemsSource = role.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object id = (MyData.SelectedItem as DataRowView).Row[0];
            role.DeleteQuery(Convert.ToInt32(id));
            MyData.ItemsSource = role.GetData();
            xBOX.Text = "";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(xBOX.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                role.InsertQuery(xBOX.Text);
                MyData.ItemsSource = role.GetData();
                xBOX.Text = "";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (xBOX.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                role.UpdateQuery(xBOX.Text, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                MyData.ItemsSource = role.GetData();
                xBOX.Text = "";
            }
        }
    }
}

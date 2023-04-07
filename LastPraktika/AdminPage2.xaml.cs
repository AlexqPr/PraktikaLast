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
    /// Логика взаимодействия для AdminPage2.xaml
    /// </summary>
    public partial class AdminPage2 : Page
    {
        staffTableAdapter staff = new staffTableAdapter();
        rolesTableAdapter roles = new rolesTableAdapter();
        shopsTableAdapter newshop = new shopsTableAdapter();
        List<string> role_arr = new List<string>();
        List<string> shop_arr = new List<string>();
        public AdminPage2()
        {
            InitializeComponent();
            MyData.ItemsSource = staff.GetData();
            var role = roles.GetData().Rows;
            for (int i = 0; i < role.Count; i++)
            {
                role_arr.Add(role[i][1].ToString());
            }
            xBOX.ItemsSource = role_arr;

            var shoping = newshop.GetData().Rows;
            for (int i = 0; i < shoping.Count; i++)
            {
                shop_arr.Add(shoping[i][1].ToString());
            }
            newSHOP.ItemsSource = shop_arr;
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LastName.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                FirstName.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Patronymic.Text = (MyData.SelectedItem as DataRowView).Row[3].ToString();
                xBOX.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[4])-1;
                newSHOP.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[5]) - 1;

            }
            catch
            {

            }
        
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LastName.Text == "" || FirstName.Text == "" || Patronymic.Text == "" || xBOX.SelectedIndex  == -1 || newSHOP.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                staff.InsertQuery(LastName.Text, FirstName.Text, Patronymic.Text,xBOX.SelectedIndex+1,newSHOP.SelectedIndex+1);
                MyData.ItemsSource = staff.GetData();
                LastName.Text = "";
                FirstName.Text = "";
                Patronymic.Text = "";
                xBOX.SelectedItem = null;
                newSHOP.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            staff.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            MyData.ItemsSource = staff.GetData();
            LastName.Text = "";
            FirstName.Text = "";
            Patronymic.Text = "";
            xBOX.SelectedItem = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (LastName.Text == "" || FirstName.Text == "" || Patronymic.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                staff.UpdateQuery(LastName.Text, FirstName.Text, Patronymic.Text, xBOX.SelectedIndex + 1, newSHOP.SelectedIndex+1, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                MyData.ItemsSource = staff.GetData();
                LastName.Text = "";
                FirstName.Text = "";
                Patronymic.Text = "";
                xBOX.SelectedItem = null;
                newSHOP.SelectedItem = null;
            }
        }
    }
}

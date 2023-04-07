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
    /// Логика взаимодействия для AdminPage3.xaml
    /// </summary>
    public partial class AdminPage3 : Page
    {
        autorizTableAdapter autor = new autorizTableAdapter();
        staffTableAdapter newstaff = new staffTableAdapter();
        List<string> staff_arr = new List<string>();
        public AdminPage3()
        {
            InitializeComponent();
            MyData.ItemsSource = autor.GetData();
            var staffs = newstaff.GetData().Rows;
            for (int i = 0; i < staffs.Count; i++)
            {
                staff_arr.Add(staffs[i][1].ToString());
            }
            Staff.ItemsSource = staff_arr;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Login.Text == ""  || Passworrd.Password  == "" || Staff.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                autor.InsertQuery(Login.Text, Passworrd.Password, Staff.SelectedIndex+1);
                MyData.ItemsSource = autor.GetData();
                Login.Text = "";
                Passworrd.Password = "";
                Staff.SelectedIndex = -1;
            }

            
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Login.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                Passworrd.Password = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Staff.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[3])-1;
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            autor.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            MyData.ItemsSource = autor.GetData();
            Login.Text = "";
            Passworrd.Password = "";
            Staff.SelectedIndex = -1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(Login.Text == "" || Passworrd.Password == "" || Staff.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                
                autor.UpdateQuery(Login.Text,Passworrd.Password,Staff.SelectedIndex+1, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                MyData.ItemsSource = autor.GetData();
                Login.Text = "";
                Passworrd.Password = "";
                Staff.SelectedIndex = -1;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            List<NEwImport> forImport = MyConv.DeserializeObject<List<NEwImport>>();
            foreach (var item in forImport)
            {
                autor.InsertQuery(item.Login, item.Password, item.Staff_id);
            }
            MyData.ItemsSource = autor.GetData();
        }
    }
}

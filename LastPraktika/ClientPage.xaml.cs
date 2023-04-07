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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        clientsTableAdapter client = new clientsTableAdapter();
        information_about_orderTableAdapter infor = new information_about_orderTableAdapter();
        List<string> infor_arr = new List<string>();
        public ClientPage()
        {
            InitializeComponent();
            MyData.ItemsSource = client.GetData();
            var infors = infor.GetData().Rows;
            for (int i = 0; i < infors.Count; i++)
            {
                infor_arr.Add(infors[i][0].ToString());
            }
            xBOX.ItemsSource = infor_arr;
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LastName.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                FirstName.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Patronymic.Text = (MyData.SelectedItem as DataRowView).Row[3].ToString();
                xBOX.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[4])-1;
            }
            catch
            {
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(LastName.Text == ""  ||   FirstName.Text == "" || Patronymic.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                client.InsertQuery(LastName.Text,FirstName.Text,Patronymic.Text, xBOX.SelectedIndex+1);
                LastName.Text = "";
                FirstName.Text = "";
                Patronymic.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = client.GetData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (LastName.Text == "" || FirstName.Text == "" || Patronymic.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                client.UpdateQuery(LastName.Text, FirstName.Text, Patronymic.Text, Convert.ToInt32(xBOX.SelectedItem), Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                LastName.Text = "";
                FirstName.Text = "";
                Patronymic.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = client.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //DELETE
        {
            client.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            LastName.Text = "";
            FirstName.Text = "";
            Patronymic.Text = "";
            xBOX.SelectedIndex = -1;
            MyData.ItemsSource = client.GetData();
        }
    }
}

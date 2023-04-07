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
    /// Логика взаимодействия для SpecificationWindow.xaml
    /// </summary>
    public partial class SpecificationWindow : Page
    {
        specificationTableAdapter spec = new specificationTableAdapter();
        form_factorTableAdapter form = new form_factorTableAdapter();
        List<string> form_arr = new List<string>();
        public SpecificationWindow()
        {
            InitializeComponent();
            MyData.ItemsSource = spec.GetData();
            var forma = form.GetData().Rows;
            for (int i = 0; i < forma.Count; i++)
            {
                form_arr.Add(forma[i][1].ToString());
            }
            xBOX.ItemsSource = form_arr;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(Name_spec.Text == "" || Purpose.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                spec.InsertQuery(Name_spec.Text, Purpose.Text,xBOX.SelectedIndex+1);
                Name_spec.Text = "";
                Purpose.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = spec.GetData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (Name_spec.Text == "" || Purpose.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                spec.UpdateQuery(Name_spec.Text, Purpose.Text, xBOX.SelectedIndex + 1,Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                Name_spec.Text = "";
                Purpose.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = spec.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //DELETE
        {
            spec.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            Name_spec.Text = "";
            Purpose.Text = "";
            xBOX.SelectedIndex = -1;
            MyData.ItemsSource = spec.GetData();
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Name_spec.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                Purpose.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                xBOX.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[3]) - 1;
            }
            catch
            {

            }
        }
    }
}

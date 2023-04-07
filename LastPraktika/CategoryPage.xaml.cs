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
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        categoryTableAdapter cat = new categoryTableAdapter();
        specificationTableAdapter spec = new specificationTableAdapter();
        List<string> spec_arr = new List<string>();
        public CategoryPage()
        {
            InitializeComponent();
            MyData.ItemsSource = cat.GetData();
            var specif = spec.GetData().Rows;
            for (int i = 0; i < specif.Count; i++)
            {
                spec_arr.Add(specif[i][2].ToString());
            }
            xBOX.ItemsSource = spec_arr;
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Name_cat.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                xBOX.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[2]) - 1;
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // CREATE
        {
            if(Name_cat.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                cat.InsertQuery(Name_cat.Text,xBOX.SelectedIndex+1);
                Name_cat.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = cat.GetData();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (Name_cat.Text == "" || xBOX.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                cat.UpdateQuery(Name_cat.Text, xBOX.SelectedIndex + 1, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                Name_cat.Text = "";
                xBOX.SelectedIndex = -1;
                MyData.ItemsSource = cat.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            cat.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            Name_cat.Text = "";
            xBOX.SelectedIndex = -1;
            MyData.ItemsSource = cat.GetData();
        }
    }
}

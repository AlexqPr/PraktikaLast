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
    /// Логика взаимодействия для Form_factor.xaml
    /// </summary>
    public partial class Form_factor : Page
    {
        form_factorTableAdapter form = new form_factorTableAdapter();
        public Form_factor()
        {
            InitializeComponent();
            MyData.ItemsSource = form.GetData();
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Name_form.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                Width.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Height.Text = (MyData.SelectedItem as DataRowView).Row[3].ToString();
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(Name_form.Text  == "" || Width.Text == "" || Height.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if(Convert.ToInt32(Width.Text) <= 0 || Convert.ToInt32(Height.Text) <= 0 )
                {
                    MessageBox.Show("Неверное значение размеров");
                }
                else
                {
                    form.InsertQuery(Name_form.Text, Width.Text, Height.Text);
                    Name_form.Text = "";
                    Width.Text = "";
                    Height.Text = "";
                    MyData.ItemsSource = form.GetData();
                }
               
            }
        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // DELETE
        {
            form.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            Name_form.Text = "";
            Width.Text = "";
            Height.Text = "";
            MyData.ItemsSource = form.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //UPDATE
        {
            if(Name_form.Text == "" || Width.Text == "" || Height.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if(Convert.ToInt32(Width.Text) <= 0 || Convert.ToInt32(Height.Text) <= 0)
                {
                    MessageBox.Show("Неверное значение размеров");
                }
                else
                {
                    form.UpdateQuery(Name_form.Text, Width.Text, Height.Text, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                    Name_form.Text = "";
                    Width.Text = "";
                    Height.Text = "";
                    MyData.ItemsSource = form.GetData();
                }
    
            }
        }
    }
}

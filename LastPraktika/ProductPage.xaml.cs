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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        productTableAdapter product = new productTableAdapter();
        categoryTableAdapter category = new categoryTableAdapter();
        List<string> cat_arr = new List<string>();
        public ProductPage()
        {
            InitializeComponent();
            MyData.ItemsSource = product.GetData();
            var cats = category.GetData().Rows;
            for (int i = 0; i < cats.Count; i++)
            {
                cat_arr.Add(cats[i][1].ToString());
            }
            xBOX.ItemsSource = cat_arr;
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Pr_Name.Text = (MyData.SelectedItem as DataRowView).Row[1].ToString();
                Price.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Pr_Amount.Text = (MyData.SelectedItem as DataRowView).Row[3].ToString();
                xBOX.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[4]) - 1;
            }
            catch
            {
                
            }
          
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(Price.Text == "" || Pr_Amount.Text == "" || xBOX.SelectedIndex == -1 || Pr_Name.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if(Price.Text.Contains(".") == true)
                {
                    MessageBox.Show("Неверное значение в поле Цена!");
                }
                else if(Convert.ToDouble(Price.Text) <= 0)
                {
                    MessageBox.Show("Неверное значение в поле Цена!");
                }
                else if(Pr_Amount.Text.Contains(".") == true || Pr_Amount.Text.Contains(",") == true)
                {
                    MessageBox.Show("Неверное значение в поле Количество!");
                }
                else if (Convert.ToInt32(Pr_Amount.Text) <= 0)
                {
                    MessageBox.Show("Неверное значение в поле Количество!");
                }
                else
                {
                    product.InsertQuery(Pr_Name.Text, Convert.ToDecimal(Price.Text), Convert.ToInt32(Pr_Amount.Text), xBOX.SelectedIndex + 1);
                    Price.Text = "";
                    Pr_Amount.Text = "";
                    xBOX.SelectedIndex = -1;
                    Pr_Name.Text = "";
                    MyData.ItemsSource = product.GetData();

                }
        
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (Price.Text == "" || Pr_Amount.Text == "" || xBOX.SelectedIndex == -1 || Pr_Name.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if (Price.Text.Contains(".") == true)
                {
                    MessageBox.Show("Неверное значение в поле Цена!");
                }
                else if (Convert.ToDouble(Price.Text) <= 0)
                {
                    MessageBox.Show("Неверное значение в поле Цена!");
                }
                else if (Pr_Amount.Text.Contains(".") == true || Pr_Amount.Text.Contains(",") == true)
                {
                    MessageBox.Show("Неверное значение в поле Количество!");
                }
                else if (Convert.ToInt32(Pr_Amount.Text) <= 0)
                {
                    MessageBox.Show("Неверное значение в поле Количество!");
                }
                else
                {
                    product.UpdateQuery(Pr_Name.Text, Convert.ToDecimal(Price.Text), Convert.ToInt32(Pr_Amount.Text), xBOX.SelectedIndex + 1, Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                    Price.Text = "";
                    Pr_Amount.Text = "";
                    xBOX.SelectedIndex = -1;
                    Pr_Name.Text = "";
                    MyData.ItemsSource = product.GetData();

                }
                
                
            
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //DELETE
        {
            product.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            Price.Text = "";
            Pr_Amount.Text = "";
            xBOX.SelectedIndex = -1;
            Pr_Name.Text = "";
            MyData.ItemsSource = product.GetData();
        }
    }
}

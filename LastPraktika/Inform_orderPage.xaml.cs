using LastPraktika.newshopDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для Inform_orderPage.xaml
    /// </summary>
    public partial class Inform_orderPage : Page
    {
        information_about_orderTableAdapter info = new information_about_orderTableAdapter();
        productTableAdapter newproduct = new productTableAdapter();
        List<string> product_arr = new List<string>();
        public Inform_orderPage()
        {
            InitializeComponent();
            MyData.ItemsSource = info.GetData();
            var products = newproduct.GetData().Rows;
            for (int i = 0; i < products.Count; i++)
            {
                product_arr.Add(products[i][1].ToString());
            }
            Product_id.ItemsSource = product_arr;
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Amount_product.Text = (MyData.SelectedItem as DataRowView).Row[2].ToString();
                Product_id.SelectedIndex = Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[3]) - 1;
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //CREATE
        {
            if(Amount_product.Text == "" || Product_id.SelectedIndex == - 1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if(Amount_product.Text.Contains(".") == true || Amount_product.Text.Contains(",") == true )
                {
                    MessageBox.Show("Неверно заполнено поле Количество товара!");
                }
                else if (Convert.ToInt32(Amount_product.Text) <= 0)
                {
                    MessageBox.Show("Неверно заполнено поле Количество товара!");
                }
                else
                {
                    var products = newproduct.GetData().Rows;
                    decimal Amount_order = Convert.ToInt32(Amount_product.Text)*Convert.ToInt32(products[Product_id.SelectedIndex][2]);//Узнаем сумму заказа
                    int ostatok = Convert.ToInt32(products[Product_id.SelectedIndex][3]) - Convert.ToInt32(Amount_product.Text);//Вычитаем из таблицы выбранное количество товара
                    if(ostatok <  0)
                    {
                        MessageBox.Show("Увы, такого количества товара нет на складе. Выберите меньше!");
                    }
                    else
                    {
                        newproduct.UpdateQuery(Product_id.SelectedItem.ToString(), Convert.ToInt32(products[Product_id.SelectedIndex][2]),ostatok, Convert.ToInt32(products[Product_id.SelectedIndex][4]), Convert.ToInt32(products[Product_id.SelectedIndex][0]));
                        info.InsertQuery(Amount_order, Convert.ToInt32(Amount_product.Text), Product_id.SelectedIndex + 1, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString());
                        Amount_product.Text = "";
                        Product_id.SelectedIndex = -1;
                        MyData.ItemsSource = info.GetData();
                    }
                   
                }
          
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //UPDATE
        {
            if (Amount_product.Text == "" || Product_id.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                if(Amount_product.Text.Contains(".") == true || Amount_product.Text.Contains(",") == true)
                {
                    MessageBox.Show("Неверно заполнено поле Количество товара!");
                }
                else if(Convert.ToInt32(Amount_product.Text) <=0)
                {
                    MessageBox.Show("Неверно заполнено поле Количество товара!");
                }
                else
                {
                    info.UpdateQuery(Convert.ToDecimal(Amount_product.Text), Convert.ToInt32(Amount_product.Text), Product_id.SelectedIndex + 1, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
                    Amount_product.Text = "";
                    Product_id.SelectedIndex = -1;
                    MyData.ItemsSource = info.GetData();
                }
             
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //DELETE
        {
            info.DeleteQuery(Convert.ToInt32((MyData.SelectedItem as DataRowView).Row[0]));
            Amount_product.Text = "";
            Product_id.SelectedIndex = -1;
            MyData.ItemsSource = info.GetData();
        }

        private void Zapis()
        {
            string dekstop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FileName = "Чек№";
            var infor = info.GetData().Rows;
            FileName = FileName + (MyData.SelectedItem as DataRowView).Row[0].ToString()  +  ".txt";
            File.AppendAllText(dekstop + "\\" + FileName, "\tDNS" + "\n");
            File.AppendAllText(dekstop + "\\" + FileName, "\tКассовый чек№" + (MyData.SelectedItem as DataRowView).Row[0].ToString() + "\n\n");
            var products = newproduct.GetData().Rows;
            File.AppendAllText(dekstop + "\\" + FileName, "\t" + Product_id.SelectedItem.ToString() + "\t-"  + "\t" + products[Product_id.SelectedIndex][2].ToString() + "\n\n");
            File.AppendAllText(dekstop + "\\" + FileName, "Итог к оплате:" + Convert.ToInt32(Amount_product.Text) * Convert.ToInt32(products[Product_id.SelectedIndex][2]) + "\n");
            File.AppendAllText(dekstop + "\\" + FileName, "Покупатель внес:"+ SumPok.Text + "\n");
            File.AppendAllText(dekstop + "\\" + FileName, "Сдача:" + (Convert.ToInt32(SumPok.Text)  - Convert.ToInt32(Amount_product.Text) * Convert.ToInt32(products[Product_id.SelectedIndex][2]))+ "\n");
            File.AppendAllText(dekstop + "\\" + FileName, "ДАТА:" +(MyData.SelectedItem as DataRowView).Row[4] + "\t" + (MyData.SelectedItem as DataRowView).Row[5] + "\n");
            MessageBox.Show("Чек выгружен");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Zapis();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            List<NEwImport2> forImport = MyConv.DeserializeObject<List<NEwImport2>>();
            foreach (var item in forImport)
            {
                info.InsertQuery(item.SUMMA,item.AMOUNT,item.PRODUCT_ID,item.DATA,item.TIME);
            }
            MyData.ItemsSource = info.GetData();
        }
    }
}

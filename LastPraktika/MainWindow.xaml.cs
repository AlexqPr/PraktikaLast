using LastPraktika.newshopDataSetTableAdapters;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        autorizTableAdapter autor = new autorizTableAdapter();
        staffTableAdapter staff = new staffTableAdapter();
        bool mayak = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
           
           


        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var logins = autor.GetData().Rows;

            for (int i = 0; i < logins.Count; i++)
            {
                if (logins[i][1].ToString() == Login.Text && logins[i][2].ToString() == Password.Password)
                {
                    int staffID = (int)logins[i][3];
                    var staff_id = staff.GetData().Rows;
                    for (int j = 0; j < staff_id.Count; j++)
                    {
                        if (staff_id[j][0].ToString() == staffID.ToString())
                        {
                            MessageBox.Show("Добро пожаловать " + staff_id[j][2] + " " + staff_id[j][3]);
                            mayak = true;
                            switch (staffID)
                            {
                                case 1:
                                    AdminWindow window = new AdminWindow();
                                    window.Show();
                                    this.Close();
                                    break;
                                case 2:
                                    KassaWindow window1 = new KassaWindow();
                                    window1.Show();
                                    this.Close();
                                    break;
                                case 3:
                                    Sklad window2 = new Sklad();
                                    window2.Show();
                                    this.Close();
                                    break;
                            }
                        }
                    }
                }

            }
            if (mayak == false)
            {
                MessageBox.Show("Вы ввели неверные данные");
            }
        }
    }
}

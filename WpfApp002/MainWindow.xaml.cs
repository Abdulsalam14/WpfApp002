using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp002
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataTable table;
        Users users = new Users();
        public MainWindow()
        {
            InitializeComponent();
            users.GetData();
            table = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lgnbx.Text) || string.IsNullOrEmpty(pswrdbx.Text))
            {
                MessageBox.Show("---");
                return;
            }
            bool b = false;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (lgnbx.Text.ToString() == table.Rows[i][1].ToString())
                {
                    if (pswrdbx.Text == table.Rows[i][2].ToString())
                    {
                        MessageBox.Show("Sizin Id Nomreniz: " + table.Rows[i][0].ToString(), "Daxil Oldunuz");
                    }
                    else MessageBox.Show("Password yanlishdir.");
                    b = true;
                }
            }
            if (!b) MessageBox.Show("Bu login movcud deyil.");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            App.Current.MainWindow.Close();
        }
    }
}

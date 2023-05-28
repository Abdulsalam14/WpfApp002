using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp002
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        SqlConnection? connection;
        SqlDataReader? reader;
        string connectionstring;
        public Window1()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("AppConfig.json");
            var config = builder.Build();
            connectionstring = config.GetConnectionString("ConnectionString1")!;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(lgnbx.Text) && !string.IsNullOrEmpty(pswrdbx.Text) && !string.IsNullOrEmpty(pswrdbx2.Text))
            {
                if (pswrdbx.Text==pswrdbx2.Text)
                {
                    using (connection=new SqlConnection(connectionstring))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.Parameters.AddWithValue("@login", lgnbx.Text);
                        cmd.CommandText = "SELECT [Login] FROM LoginPassword WHERE [Login] =@login";
                        bool b = false;
                        using (reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read()) b = true;
                        }
                        if (b)
                        {
                            string insertQuery = @"INSERT INTO LoginPassword ([Login],[Password]) VALUES (@login,@password)";
                            SqlCommand cmd2 = new SqlCommand(insertQuery, connection);
                            cmd2.Parameters.AddWithValue("@login", lgnbx.Text);
                            cmd2.Parameters.AddWithValue("@password", pswrdbx.Text);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Registered Successfully");
                            MainWindow main = new MainWindow();
                            main.Show();
                            Window.GetWindow(this)?.Close();
                        }
                        else MessageBox.Show("Bu login istifade olunub.");
                    }
                }
                else
                {
                    MessageBox.Show("Password fail");
                }
            }
            else
            {
                MessageBox.Show("Butun xanalari doldurun");
            }
        }
    }
}

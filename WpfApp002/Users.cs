using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WpfApp002
{
    public class Users : DataTable
    {
        string? connectionstring;
        public void GetData()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("AppConfig.json");
            var config = builder.Build();
            connectionstring = config.GetConnectionString("ConnectionString1")!;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM LoginPassword ", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool b = true;
                    while (reader.Read())
                    {
                        if (b)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                this.Columns.Add(reader.GetName(i));
                            }
                            b = false;
                        }
                        DataRow dataRow = NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader[i];
                        }
                        Rows.Add(dataRow);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace DAL
{
    public class DBAccess : IDBAccess
    {
        private string connectionString;
        private string databaseName;

        public string Path { set; get; }
        public string DatabaseName
        {
            set
            {
                this.databaseName = value;
                connectionString = @"Data Source=DESKTOP-M25C50U\WORKSQL; Initial Catalog=" + this.databaseName + "; Integrated Security=True;";
            }
            get => databaseName;
        }

        public DBAccess(string database,string path)
        {
            this.Path = path;
            this.databaseName = database;
            connectionString = @"Data Source=DESKTOP-M25C50U\WORKSQL; Initial Catalog=" + this.databaseName + "; Integrated Security=True;";
        }

        public IEnumerable<object> GetData(string code, KeyValuePair<string, object> parameters)
        {
            if (File.Exists(this.Path))
            {
                StreamReader reader = new StreamReader(this.Path);
                string text = reader.ReadToEnd();
                if(text.Contains(code))
                {
                    string temp1 = text.Substring(text.IndexOf(code));
                    string query = "";
                    if (temp1.IndexOf("name") >= 0)
                    {
                        string temp2 = temp1.Remove(temp1.IndexOf("name"));
                        query = temp2.Remove(0, code.Length);
                    }
                    else
                    {
                        query = temp1.Remove(0, code.Length);
                    }

                    if (parameters.Key != null && parameters.Value != null)
                        query = query.Replace(parameters.Key, parameters.Value.ToString());

                    using (SqlConnection connection = new SqlConnection(this.connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query,connection);
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        var data = new List<object>[dataReader.FieldCount];
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            data[i] = new List<object>();
                        }
                        while (dataReader.Read())
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                data[i].Add(dataReader[i]);
                            }
                        }
                        return data;
                    }
                }
                else
                {
                    throw new Exception("Incorrect code-name!");
                }
            }
            else
            {
                throw new Exception("Incorrect path!");
            }
            return null;
        }
    }
}

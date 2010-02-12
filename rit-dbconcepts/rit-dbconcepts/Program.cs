using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Type;


namespace rit_dbconcepts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string conString = "SERVER=trevor-mack.com;" +
               "DATABASE=database_concepts;" + "UID=mrt9364;" +
               "PASSWORD=QzVns;";

            MySqlConnection connection = new MySqlConnection(myConString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELET * FROM movies";

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                    listBox1.Items.Add(row);
                }
            }
            connection.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

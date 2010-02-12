using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace rit_dbconcepts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initList();
        }

        public void initList() {
            string conString = "SERVER=trevor-mack.com;" +
               "DATABASE=database_concepts;" + "UID=mrt9364;" +
               "PASSWORD=QzVns;";

            MySqlConnection connection = new MySqlConnection(conString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT title FROM movie;";

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ",";
                    listBox1.Items.Add( row );
                }
            }
            connection.Close();
        }
    }
}

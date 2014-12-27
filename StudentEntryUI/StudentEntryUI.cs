using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentEntryUI
{
    public partial class StudentEntryUI : Form
    {
        string connectionString = @"Server=BITM-401-PC04; Database=UniversityDB; Integrated Security=True";


        public StudentEntryUI()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string email = emailTextBox.Text;
            
            SqlConnection connection = new SqlConnection(connectionString);

            string query = String.Format("INSERT INTO tStudent VALUES('{0}','{1}','{2}')", name, address, email);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Inserted Successfully!");
            }
            else
            {
                MessageBox.Show("Insert Failed!");
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            Clear();
            string sql = "Select * FROM tStudent";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader Reader = cmd.ExecuteReader();

            listView.Items.Clear();

            while (Reader.Read())
            {

                ListViewItem lv = new ListViewItem(Reader.GetString(1));
                lv.SubItems.Add(Reader.GetString(2));
                lv.SubItems.Add(Reader.GetString(3));
                listView.Items.Add(lv);
            }

            Reader.Close();
            cnn.Close();

        }

        public void Clear()
        {
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            emailTextBox.Text = "";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BiometricDb
{
    public partial class LocationMaintenance : Form
    {

        Bitmap finalImg;
        System.Data.SqlClient.SqlConnection con;
        public LocationMaintenance()
        {
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            textBox10.Enabled = false;
            textBox1.Enabled = true;         
            comboBox1.Enabled = true; 
            buttonSave.Enabled = true;      
            buttonSearch.Enabled = false;

        }


        public void search(string s)
        {
    
            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\FERGAL O NEILL\\Documents\\Fergal Final Year Folder\\Software Engineering Project\\BiometricDb\\BiometricDb\\WindowsFormsApplication1\\InD.mdf;Integrated Security=True";
            con.Open();


            using (var cmd = new SqlCommand("Select * from Locations where Id = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", s);

                SqlDataAdapter daLocation = new SqlDataAdapter(cmd);
                DataSet dsLocationSearch = new DataSet("LocationSearch");
                daLocation.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                daLocation.Fill(dsLocationSearch, "Locations");

                DataTable tblLocationDetails;
                tblLocationDetails = dsLocationSearch.Tables["Locations"];

                foreach (DataRow drCurrent in tblLocationDetails.Rows)
                {
                    textBox10.Text = drCurrent["Id"].ToString();
                    textBox1.Text = drCurrent["LocationName"].ToString();            
                    comboBox1.Text = drCurrent["AccessLevel"].ToString();                
                }
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                buttonSave.Enabled = true;
                buttonSearch.Enabled = false;
                textBox10.Enabled = false;
                buttonNew.Enabled = false;
                buttonDelete.Enabled = true;

            }

        }


        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            string query = "DELETE FROM Locations WHERE Id = @id";
            SqlCommand cmd1 = new SqlCommand(query, con);
            cmd1.Parameters.AddWithValue("@id", textBox10.Text);

            int result = cmd1.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Location successfully deleted");
            }
            else
            {
                MessageBox.Show("Error deleting Location");
            }

            cleanup();
        }

        public void cleanup()
        {

            textBox10.Text = "";
            textBox10.Enabled = true;
            textBox1.Text = "";
            comboBox1.Text = "";
            buttonSave.Enabled = false;  
            buttonSearch.Enabled = true;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            con = new System.Data.SqlClient.SqlConnection();

            con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\FERGAL O NEILL\\Documents\\Fergal Final Year Folder\\Software Engineering Project\\BiometricDb\\BiometricDb\\WindowsFormsApplication1\\InD.mdf;Integrated Security=True";


            string query;
            if (textBox10.Text != "")
            {
                query = "UPDATE Locations SET LocationName=@locationName, AccessLevel=@accessLevel WHERE Id =" + textBox10.Text;

            }
            else
            {
                query = "INSERT INTO Locations(LocationName, AccessLevel) VALUES(@locationName,@accessLevel)";

            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                int accessLevel = 1;
                if (comboBox1.SelectedItem == "Level 1")
                {
                    accessLevel = 1;
                }
                else if (comboBox1.SelectedItem == "Level 2")
                {
                    accessLevel = 2;
                }
                else if (comboBox1.SelectedItem == "Level 3")
                {
                    accessLevel = 3;
                }

                con.Open();
                cmd.Parameters.AddWithValue("@locationName", textBox1.Text);
                cmd.Parameters.AddWithValue("@accessLevel", accessLevel);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
                con.Close();
                cleanup();


            }
        }

        private void buttonSearch_Click_1(object sender, EventArgs e)
        {
            search(textBox10.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LocationMaintenance_Load(object sender, EventArgs e)
        {

        }

       

    }
}
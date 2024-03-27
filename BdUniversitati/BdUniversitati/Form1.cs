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

namespace BdUniversitati
{
    public partial class Form1 : Form
    {
        SqlConnection myCon = new SqlConnection();
        DataSet dsUniv;
        DataSet dsFac;
        public Form1()
        {
            InitializeComponent();
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UniversitatiDatabasemdf.mdf;Integrated Security=True";
            myCon.Open();
            dsUniv = new DataSet();
            dsFac = new DataSet();
            SqlDataAdapter daUniv = new SqlDataAdapter("SELECT * FROM Universitati",myCon);
            daUniv.Fill(dsUniv,"Universitati");
            SqlDataAdapter daFac = new SqlDataAdapter("SELECT * FROM Facultati", myCon);
            daFac.Fill(dsFac, "Facultati");
            foreach(DataRow dr in dsUniv.Tables["Universitati"].Rows)
            {
                String name = dr.ItemArray.GetValue(1).ToString();
                listBox_Univ.Items.Add(name);
            }
            myCon.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox_Univ_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Fac.Items.Clear();
            textBox_City.Clear();
            int code = 0;
            String UnivSelected = listBox_Univ.SelectedIndex.ToString();
            foreach(DataRow dr in dsUniv.Tables["Universitati"].Rows)
            {
                if(UnivSelected == dr.ItemArray.GetValue(1).ToString())
                {
                    textBox_City.Text = dr.ItemArray.GetValue(2).ToString();
                    code = Convert.ToInt32(dr.ItemArray.GetValue(3));
                    textBox_CodUniv.Text = code.ToString();
                }
            }
            foreach(DataRow dr in dsFac.Tables["Facultati"].Rows)
            {
                if(code == Convert.ToInt32(dr.ItemArray.GetValue(2)))
                {
                    String nameFac = dr.ItemArray.GetValue(1).ToString();
                    listBox_Fac.Items.Add(nameFac);
                }
            }

        }
    }
}

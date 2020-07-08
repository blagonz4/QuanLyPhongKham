﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using QuanLyPhongKham.GUI;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace QuanLyPhongKham.DAL
{
    public class DataProvider
    {
        private static DataProvider instance;
        private string connectionSTR;        

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider();
                return DataProvider.instance;
            }
            private set => instance = value;
        }

        private DataProvider() {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["ConnectionString"].Value = String.Format(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLPhongKham;Integrated Security=True", Environment.MachineName);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            connectionSTR = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public DataTable ExecuteQuery(string query, Dictionary<String, String> parameters)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (String key in parameters.Keys)
                    {
                        command.Parameters.AddWithValue(key, parameters[key]);
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                return data;
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<String, String> parameters)
        {

            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (String key in parameters.Keys)
                    {
                        command.Parameters.AddWithValue(key, parameters[key]);
                    }
                }
                data = command.ExecuteNonQuery();

                return data;
            }
        }

        public object ExecuteScalar(string query, Dictionary<String, String> parameters)
        {

            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (String key in parameters.Keys)
                    {
                        command.Parameters.AddWithValue(key, parameters[key]);
                    }
                }
                data = command.ExecuteScalar();

                return data;
            }
        }
        /*public void LoginCheck()
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["frmLogin"];
            string acc = ((frmLogin)f).txb_account.Text;
            string pass = ((frmLogin)f).txb_pass.Text;
            string query = "SELECT * from Dangnhap WHERE taikhoan = '" + acc + "' AND matkhau = '" + pass + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            if (result.Rows.Count >0 )
            {
                frmMain f = new frmMain(result.Rows[0][0].ToString(), result.Rows[0][1].ToString(), result.Rows[0][2].ToString());
                this.Hide();
                f.ShowDialog();
            }

            else
            {
                MessageBox.Show("Nhập lại tài khoản/ mật khẩu");

            }

        }*/
    }
}

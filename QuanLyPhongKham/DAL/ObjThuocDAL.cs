﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using QuanLyPhongKham.BLL;
using QuanLyPhongKham.DAL;
using System.Windows.Forms;
using QuanLyPhongKham.GUI;

namespace QuanLyPhongKham.DAL
{
    class ObjThuocDAL
    {
        private static ObjThuocDAL instance;

        public static ObjThuocDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjThuocDAL();
                }
                return instance;
            }
            set { instance = value; }
        }
        public string id { get; set; }
        public string tenth { get; set; }
        public string slg { get; set; }
        public DateTime nsx { get; set; }
        public DateTime hsd { get; set; }
        public string ncc { get; set; }
        public string gia { get; set; }
        public ObjThuocDAL() { }


        public DataTable GetInfo()
        {
            DataTable dt = new DataTable();
            string LoadQuery = "SELECT * FROM THUOC";
            dt = DataProvider.Instance.ExecuteQuery(LoadQuery);
            return dt;
        }


        public void AddThuoc()
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["frmMain"];
            string id = ((frmMain)f).tb_maThuoc.Text;
            string ten = ((frmMain)f).tb_tenThuocKho.Text;
            string slg = ((frmMain)f).tb_slThuocKho.Text;
            string ncc = ((frmMain)f).tb_nhaCC.Text;
            string giathanh = ((frmMain)f).tb_giaThuoc.Text;
            string nsx = ((frmMain)f).ngaySanXuatPicker.Text;
            string hsd = ((frmMain)f).hanSDPicker.Text;



            string AddQuery = "INSERT INTO THUOC(MaThuoc,TenThuoc,SoLuong,NSX,HSD,NCC,Gia)" +
                    "VALUES('" + id + "', '" + ten + "', '" + slg + "', '" + nsx + "', '" + hsd + "', '" + ncc + "', '" +
                      giathanh + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(AddQuery);
            if (result > 0)
            {
                MessageBox.Show("Thuốc đã thêm vào kho ");
            }

        }


        public void XoaThuoc()
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["frmMain"];
            string id_xoa = ((frmMain)f).tb_maThuoc.Text;

            string DeleteQuery = "DELETE FROM THUOC WHERE MaThuoc = '" + id_xoa + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(DeleteQuery);
            if (result > 0)
            {
                MessageBox.Show("Thông tin thuốc đã bị xoá,bấm xem để xem dữ liệu mới", "Thông báo", MessageBoxButtons.OK);
            }
        }

        public void SuaThuoc()
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["frmMain"];
            string id = ((frmMain)f).tb_maThuoc.Text;
            string ten = ((frmMain)f).tb_tenThuocKho.Text;
            string slg = ((frmMain)f).tb_slThuocKho.Text;
            string ncc = ((frmMain)f).tb_nhaCC.Text;
            string giathanh = ((frmMain)f).tb_giaThuoc.Text;
            string nsx = ((frmMain)f).ngaySanXuatPicker.Text;
            string hsd = ((frmMain)f).hanSDPicker.Text;


            string UpdateQuery = "UPDATE THUOC " +
                   "SET TenThuoc= '" + ten + "', SoLuong='" + slg + "',NCC= '" + ncc + "',Gia= '" + giathanh + "',NSX= '" + nsx + "', HSD='" +
                     hsd + "' WHERE MaThuoc = '"+id+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(UpdateQuery);
            if (result > 0)
            {
                MessageBox.Show("Thông tin thuốc đã được sửa ");
            }

        }



        public DataTable FindThuoc()
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["frmMain"];



            string id, ten, slg, nsx, hsd, ncc, gia;


            if (!string.IsNullOrEmpty(((frmMain)f).tb_maThuoc.Text))
                id = "='" + ((frmMain)f).tb_maThuoc.Text + "'";
            else id = "is not null";



            if (!string.IsNullOrEmpty(((frmMain)f).tb_tenThuocKho.Text))
                ten = "='" + ((frmMain)f).tb_tenThuocKho.Text + "'";
            else ten = "is not null";

            if (!string.IsNullOrEmpty(((frmMain)f).tb_slThuocKho.Text))
                slg = "='" + ((frmMain)f).tb_slThuocKho.Text + "'";
            else slg = "is not null";

            string today = DateTime.Now.ToString("dd/MM/yyyy");



            if (((frmMain)f).ngaySanXuatPicker.Text != today)
                nsx = "='" + ((frmMain)f).ngaySanXuatPicker.Text + "'";
            else nsx = "is not null";

            if (!string.IsNullOrEmpty(((frmMain)f).hanSDPicker.Text))
                hsd = "='" + ((frmMain)f).hanSDPicker.Text + "'";
            else hsd = "is not null";


            if (!string.IsNullOrEmpty(((frmMain)f).tb_nhaCC.Text))
                ncc = "='" + ((frmMain)f).tb_bn_klb.Text + "'";
            else ncc = "is not null";

            if (!string.IsNullOrEmpty(((frmMain)f).tb_giaThuoc.Text))
                gia = "='" + ((frmMain)f).tb_giaThuoc.Text + "'";
            else gia = "is not null";



            DataTable dt = new DataTable();
            string LoadQuery = "SELECT * FROM Thuoc" +
                                " where MaThuoc " + id + " and TenThuoc " + ten + " and SoLuong " + slg + "" +
                                 " and NSX " + nsx + "  and HSD " + hsd + " and Gia " + gia;       

            dt = DataProvider.Instance.ExecuteQuery(LoadQuery);
            return dt;
        }
    }
}
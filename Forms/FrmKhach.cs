using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using QuanLyKhachSan.Database;


namespace QuanLyKhachSan.Forms
{
    public class FrmKhach : Form
    {
        TextBox txtCMND;
        TextBox txtHoTen;
        TextBox txtSDT;
        TextBox txtDiaChi;

        DateTimePicker dtNgaySinh;

        Button btnThem;


        public FrmKhach()
        {
            TaoGiaoDien();
        }


        private void TaoGiaoDien()
        {
            this.Text = "Quản lý khách hàng";
            this.Width = 500;
            this.Height = 400;


            Label l1 = new Label();
            l1.Text = "CMND";
            l1.Top = 30;
            l1.Left = 30;


            txtCMND = new TextBox();
            txtCMND.Top = 30;
            txtCMND.Left = 150;


            Label l2 = new Label();
            l2.Text = "Họ tên";
            l2.Top = 70;
            l2.Left = 30;


            txtHoTen = new TextBox();
            txtHoTen.Top = 70;
            txtHoTen.Left = 150;


            Label l3 = new Label();
            l3.Text = "Ngày sinh";
            l3.Top = 110;
            l3.Left = 30;


            dtNgaySinh = new DateTimePicker();
            dtNgaySinh.Top = 110;
            dtNgaySinh.Left = 150;


            Label l4 = new Label();
            l4.Text = "Điện thoại";
            l4.Top = 150;
            l4.Left = 30;


            txtSDT = new TextBox();
            txtSDT.Top = 150;
            txtSDT.Left = 150;


            Label l5 = new Label();
            l5.Text = "Địa chỉ";
            l5.Top = 190;
            l5.Left = 30;


            txtDiaChi = new TextBox();
            txtDiaChi.Top = 190;
            txtDiaChi.Left = 150;


            btnThem = new Button();
            btnThem.Text = "Thêm khách";
            btnThem.Top = 240;
            btnThem.Left = 150;

            btnThem.Click += btnThem_Click;


            Controls.Add(l1);
            Controls.Add(txtCMND);

            Controls.Add(l2);
            Controls.Add(txtHoTen);

            Controls.Add(l3);
            Controls.Add(dtNgaySinh);

            Controls.Add(l4);
            Controls.Add(txtSDT);

            Controls.Add(l5);
            Controls.Add(txtDiaChi);

            Controls.Add(btnThem);
        }



        private void btnThem_Click(
            object sender,
            EventArgs e)
        {
            using (SqlConnection conn = DB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd =
                new SqlCommand(
                    "sp_ThemKhach",
                    conn);


                cmd.CommandType =
                CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue(
                    "@CMND",
                    txtCMND.Text);


                cmd.Parameters.AddWithValue(
                    "@HoTen",
                    txtHoTen.Text);


                cmd.Parameters.AddWithValue(
                    "@NgaySinh",
                    dtNgaySinh.Value);


                cmd.Parameters.AddWithValue(
                    "@DT",
                    txtSDT.Text);


                cmd.Parameters.AddWithValue(
                    "@DC",
                    txtDiaChi.Text);


                cmd.ExecuteNonQuery();
            }


            MessageBox.Show(
                "Thêm khách thành công");
        }
    }
}
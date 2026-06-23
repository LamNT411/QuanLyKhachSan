using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using QuanLyKhachSan.Database;


namespace QuanLyKhachSan.Forms
{
    public class FrmDatPhong : Form
    {

        TextBox txtCMND = new();
        TextBox txtPhong = new();
        TextBox txtMaNV = new();

        DateTimePicker dtNhan = new();
        DateTimePicker dtTra = new();

        Button btnDat = new();


        public FrmDatPhong()
        {
            Text = "Đặt phòng";

            Width = 400;
            Height = 400;


            btnDat.Text = "Đặt phòng";

            btnDat.Click += btnDat_Click;


            Controls.Add(txtCMND);
            Controls.Add(txtPhong);
            Controls.Add(txtMaNV);
            Controls.Add(dtNhan);
            Controls.Add(dtTra);
            Controls.Add(btnDat);


            txtCMND.Top = 20;
            txtPhong.Top = 60;
            txtMaNV.Top = 100;

            dtNhan.Top = 140;
            dtTra.Top = 180;

            btnDat.Top = 230;
        }



        private void btnDat_Click(object sender, EventArgs e)
        {

            using SqlConnection conn = DB.GetConnection();

            conn.Open();


            SqlCommand cmd =
            new SqlCommand("sp_DatPhong", conn);


            cmd.CommandType =
            System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);

            cmd.Parameters.AddWithValue("@SoPhong", txtPhong.Text);

            cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);


            cmd.Parameters.AddWithValue("@NgayDK",
                DateTime.Now);


            cmd.Parameters.AddWithValue("@NgayNhan",
                dtNhan.Value);


            cmd.Parameters.AddWithValue("@NgayTra",
                dtTra.Value);



            cmd.ExecuteNonQuery();


            MessageBox.Show(
            "Đặt phòng thành công");

        }
    }
}
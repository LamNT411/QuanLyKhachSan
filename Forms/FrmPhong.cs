using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using QuanLyKhachSan.Database;

namespace QuanLyKhachSan.Forms
{
    public class FrmPhong : Form
    {
        DataGridView dgvPhong = new DataGridView();

        public FrmPhong()
        {
            Text = "Quản lý phòng";
            ////////////Width = 800;
            Height = 500;

            dgvPhong.Dock = DockStyle.Fill;

            Controls.Add(dgvPhong);

            Load += FrmPhong_Load;
        }


        private void FrmPhong_Load(object sender, EventArgs e)
        {
            LoadPhong();
        }


        private void LoadPhong()
        {
            using SqlConnection conn = DB.GetConnection();

            conn.Open();

            SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT * FROM tblPhong",
                conn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dgvPhong.DataSource = dt;
        }
    }
}
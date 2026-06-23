using System;
using System.Windows.Forms;
using QuanLyKhachSan.Reports;

namespace QuanLyKhachSan
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            HoaDonPDF.TaoHoaDon();

            MessageBox.Show(
                "Da tao HoaDon.pdf"
            );
        }
    }
}

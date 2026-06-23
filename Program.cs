using System;
using System.Windows.Forms;
using QuanLyKhachSan.Forms;

namespace QuanLyKhachSan
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //Application.Run(new FrmKhach());
            //Application.Run(new FrmPhong());
            Application.Run(new FrmDatPhong());
            Application.Run(new FrmMain());
        }
    }
}
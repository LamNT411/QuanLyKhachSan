
using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Database;
using QuanLyKhachSan.Reports;

namespace QuanLyKhachSan;

public class FrmDatPhong : Form
{
    TextBox txtCMND = new();
    TextBox txtPhong = new();
    TextBox txtMaNV = new();

    DateTimePicker dtNhan = new();
    DateTimePicker dtTra = new();

    Button btnDat = new();
    Button btnHoaDon = new();

    public FrmDatPhong()
    {
        CreateForm();
    }

    void CreateForm()
    {
        Text = "QUẢN LÝ ĐẶT PHÒNG";
        Width = 500;
        Height = 450;
        StartPosition = FormStartPosition.CenterScreen;

        Label lblCMND = new()
        {
            Text = "CMND khách hàng:",
            Left = 30,
            Top = 40,
            Width = 130
        };

        txtCMND.SetBounds(180, 40, 200, 25);

        Label lblPhong = new()
        {
            Text = "Số phòng:",
            Left = 30,
            Top = 90,
            Width = 130
        };

        txtPhong.SetBounds(180, 90, 200, 25);

        Label lblMaNV = new()
        {
            Text = "Mã nhân viên:",
            Left = 30,
            Top = 140,
            Width = 130
        };

        txtMaNV.SetBounds(180, 140, 200, 25);

        Label lblNhan = new()
        {
            Text = "Ngày nhận:",
            Left = 30,
            Top = 190,
            Width = 130
        };

        dtNhan.SetBounds(180, 190, 200, 25);

        Label lblTra = new()
        {
            Text = "Ngày trả:",
            Left = 30,
            Top = 240,
            Width = 130
        };

        dtTra.SetBounds(180, 240, 200, 25);

        btnDat.Text = "ĐẶT PHÒNG";
        btnDat.SetBounds(70, 310, 140, 40);
        btnDat.Click += btnDat_Click;

        btnHoaDon.Text = "XUẤT HÓA ĐƠN PDF";
        btnHoaDon.SetBounds(240, 310, 180, 40);
        btnHoaDon.Click += btnHoaDon_Click;

        Controls.AddRange(new Control[]
        {
            lblCMND, txtCMND,
            lblPhong, txtPhong,
            lblMaNV, txtMaNV,
            lblNhan, dtNhan,
            lblTra, dtTra,
            btnDat, btnHoaDon
        });
    }

    // Đặt phòng
    private void btnDat_Click(object? sender, EventArgs e)
    {
        try
        {
            using SqlConnection conn = DB.GetConnection();
            conn.Open();

            using SqlCommand cmd =
                new("sp_DatPhong", conn);

            cmd.CommandType =
                System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
            cmd.Parameters.AddWithValue("@SoPhong", txtPhong.Text);
            cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
            cmd.Parameters.AddWithValue("@NgayDK", DateTime.Now);
            cmd.Parameters.AddWithValue("@NgayNhan", dtNhan.Value);
            cmd.Parameters.AddWithValue("@NgayTra", dtTra.Value);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Đặt phòng thành công!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi đặt phòng");
        }
    }

    // Xuất PDF
    private void btnHoaDon_Click(object? sender, EventArgs e)
    {
        try
        {
            MessageBox.Show("Đang tạo hóa đơn...");

            string file = HoaDonPDF.TaoHoaDon(
                txtCMND.Text.Trim(),
                txtPhong.Text.Trim(),
                dtNhan.Value.Date,
                dtTra.Value.Date
            );

            MessageBox.Show(
                "XUẤT PDF THÀNH CÔNG!\n\n" + file
            );

            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = file,
                    UseShellExecute = true
                }
            );
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.ToString(),
                "LỖI XUẤT PDF"
            );
        }
    }
}



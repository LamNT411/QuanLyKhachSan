
using System;
using System.IO;
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuanLyKhachSan.Database;

namespace QuanLyKhachSan.Reports
{
    public class HoaDonPDF
    {
        public static string TaoHoaDon(
            string cmnd,
            string soPhong,
            DateTime ngayNhan,
            DateTime ngayTra)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            if (ngayTra.Date <= ngayNhan.Date)
                throw new Exception("Ngày trả phải sau ngày nhận!");

            string hoTen;
            string dienThoai;
            string diaChi;
            decimal donGia;

            // Lấy thông tin khách hàng và phòng từ SQL Server
            using (SqlConnection conn = DB.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        k.HoTen,
                        k.DienThoai,
                        k.DiaChi,
                        p.SoPhong,
                        p.DonGia
                    FROM tblKhach k
                    CROSS JOIN tblPhong p
                    WHERE k.CMND = @CMND
                    AND p.SoPhong = @SoPhong";

                using SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CMND", cmnd);
                cmd.Parameters.AddWithValue("@SoPhong", soPhong);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new Exception(
                        "Không tìm thấy khách hàng hoặc phòng!");
                }

                hoTen = reader["HoTen"]?.ToString() ?? "";
                dienThoai = reader["DienThoai"]?.ToString() ?? "";
                diaChi = reader["DiaChi"]?.ToString() ?? "";

                donGia = reader["DonGia"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(reader["DonGia"]);
            }

            // Tính tiền
            int soNgay = (ngayTra.Date - ngayNhan.Date).Days;
            decimal tongTien = donGia * soNgay;

            // Tạo đường dẫn PDF
            string folder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "HoaDonKhachSan");

            Directory.CreateDirectory(folder);

            string tenFile = $"HoaDon_{cmnd}_{soPhong}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            string file = Path.Combine(folder, tenFile);

            // Tạo hóa đơn PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    page.Header()
                        .Column(col =>
                        {
                            col.Item()
                                .Text("QUAN LY KHACH SAN")
                                .FontSize(22)
                                .Bold()
                                .AlignCenter();

                            col.Item()
                                .Text("HOA DON THANH TOAN")
                                .FontSize(18)
                                .Bold()
                                .AlignCenter();
                        });

                    page.Content()
                        .PaddingTop(30)
                        .Column(col =>
                        {
                            col.Spacing(12);

                            col.Item().Text(
                                $"Ngay lap: {DateTime.Now:dd/MM/yyyy}");

                            col.Item().Text(
                                $"CMND: {cmnd}");

                            col.Item().Text(
                                $"Khach hang: {hoTen}");

                            col.Item().Text(
                                $"Dien thoai: {dienThoai}");

                            col.Item().Text(
                                $"Dia chi: {diaChi}");

                            col.Item().Text(
                                $"So phong: {soPhong}");

                            col.Item().Text(
                                $"Ngay nhan: {ngayNhan:dd/MM/yyyy}");

                            col.Item().Text(
                                $"Ngay tra: {ngayTra:dd/MM/yyyy}");

                            col.Item().Text(
                                $"So ngay thue: {soNgay}");

                            col.Item().PaddingTop(15)
                                .Text(
                                    $"Don gia phong: {donGia:N0} VND");

                            col.Item().Text(
                                $"Tong tien: {tongTien:N0} VND")
                                .FontSize(20)
                                .Bold();
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text("CAM ON QUY KHACH!");
                });
            })
            .GeneratePdf(file);

            return file;
        }
    }
}

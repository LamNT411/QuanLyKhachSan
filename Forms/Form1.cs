
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.IO;

namespace QuanLyKhachSan;

public partial class Form1 : Form
{
    TextBox txtName = new();
    DateTimePicker dtBirth = new();
    TextBox txtCMND = new();
    TextBox txtPhone = new();

    Button btnSave = new();
    Button btnPDF = new();

    string connectionString =
        "Server=localhost;Database=CustomerDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public Form1()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        InitializeComponent();
        CreateForm();
    }

    // Tạo giao diện
    void CreateForm()
    {
        Text = "Customer Management";
        Width = 450;
        Height = 400;
        StartPosition = FormStartPosition.CenterScreen;

        Label name = new()
        {
            Text = "Họ Tên",
            Left = 20,
            Top = 30
        };

        txtName.Left = 150;
        txtName.Top = 30;
        txtName.Width = 200;

        Label birth = new()
        {
            Text = "Ngày Sinh",
            Left = 20,
            Top = 70
        };

        dtBirth.Left = 150;
        dtBirth.Top = 70;

        Label cmnd = new()
        {
            Text = "CMND",
            Left = 20,
            Top = 110
        };

        txtCMND.Left = 150;
        txtCMND.Top = 110;
        txtCMND.Width = 200;

        Label phone = new()
        {
            Text = "SĐT",
            Left = 20,
            Top = 150
        };

        txtPhone.Left = 150;
        txtPhone.Top = 150;
        txtPhone.Width = 200;

        // Nút lưu
        btnSave.Text = "LƯU";
        btnSave.Left = 80;
        btnSave.Top = 220;
        btnSave.Width = 100;

        btnSave.Click += SaveData;

        // Nút xuất PDF
        btnPDF.Text = "XUẤT PDF";
        btnPDF.Left = 210;
        btnPDF.Top = 220;
        btnPDF.Width = 120;

        btnPDF.Click += btnPDF_Click;

        Controls.Add(name);
        Controls.Add(txtName);

        Controls.Add(birth);
        Controls.Add(dtBirth);

        Controls.Add(cmnd);
        Controls.Add(txtCMND);

        Controls.Add(phone);
        Controls.Add(txtPhone);

        Controls.Add(btnSave);
        Controls.Add(btnPDF);
    }

    // Lưu dữ liệu vào SQL Server
    void SaveData(object? sender, EventArgs e)
    {
        try
        {
            using SqlConnection conn = new(connectionString);

            conn.Open();

            string sql = @"
                INSERT INTO Customers
                (FullName, BirthDate, CMND, Phone)
                VALUES
                (@name, @birth, @cmnd, @phone)";

            using SqlCommand cmd = new(sql, conn);

            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@birth", dtBirth.Value.Date);
            cmd.Parameters.AddWithValue("@cmnd", txtCMND.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Lưu khách hàng thành công!");

            // Tạo PDF sau khi lưu
            TaoPDF();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // Sự kiện xuất PDF
    private void btnPDF_Click(object? sender, EventArgs e)
    {
        try
        {
            TaoPDF();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi xuất PDF",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // Hàm tạo file PDF
    void TaoPDF()
    {
        string pdfFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "CustomerInfo.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                page.Header()
                    .Text("THÔNG TIN KHÁCH HÀNG")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().Column(column =>
                {
                    column.Item().PaddingTop(20)
                        .Text($"Họ tên: {txtName.Text}");

                    column.Item()
                        .Text($"Ngày sinh: {dtBirth.Value:dd/MM/yyyy}");

                    column.Item()
                        .Text($"CMND: {txtCMND.Text}");

                    column.Item()
                        .Text($"SĐT: {txtPhone.Text}");
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Cam on quy khach");
            });
        })
        .GeneratePdf(pdfFile);

        MessageBox.Show("Tạo PDF thành công!\n" + pdfFile);

        // Mở file PDF
        if (File.Exists(pdfFile))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = pdfFile,
                UseShellExecute = true
            });
        }
    }
}

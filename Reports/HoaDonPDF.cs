using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace QuanLyKhachSan.Reports
{
    public class HoaDonPDF
    {
        public static void TaoHoaDon()
        {

            QuestPDF.Settings.License =
                LicenseType.Community;


            Document.Create(container =>
            {

                container.Page(page =>
                {

                    page.Size(PageSizes.A4);

                    page.Margin(40);


                    page.Header()
                    .Text("HOA DON KHACH SAN")
                    .Bold()
                    .FontSize(20)
                    .AlignCenter();


                    page.Content()
                    .Column(col =>
                    {

                        col.Item()
                        .Text("Khach hang: Nguyen Van A");


                        col.Item()
                        .Text("Phong: P101");


                        col.Item()
                        .Text("Ngay nhan: 24/06/2026");


                        col.Item()
                        .Text("Ngay tra: 26/06/2026");


                        col.Item()
                        .PaddingTop(20)
                        .Text("Tong tien: 1.500.000 VND")
                        .Bold();


                    });


                    page.Footer()
                    .AlignCenter()
                    .Text("Cam on quy khach");

                });

            })
            .GeneratePdf("HoaDon.pdf");


        }
    }
}
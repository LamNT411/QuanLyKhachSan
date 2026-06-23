namespace QuanLyKhachSan
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnInHoaDon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        
            private void InitializeComponent()
        {
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.btnInHoaDon.Location = new System.Drawing.Point(20, 20);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(150, 40);
            this.btnInHoaDon.Text = "In Hoa Don";
            this.btnInHoaDon.Visible = true;

            this.Controls.Add(this.btnInHoaDon);

            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Quan Ly Khach San";

            this.ResumeLayout(false);
        }

    
    }
}
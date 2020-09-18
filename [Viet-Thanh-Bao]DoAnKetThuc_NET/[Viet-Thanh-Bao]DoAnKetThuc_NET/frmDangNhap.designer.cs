namespace _Viet_Thanh_Bao_DoAnKetThuc_NET
{
    partial class frmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenTaiKhoan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.cbHienThiMatKhau = new System.Windows.Forms.CheckBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.lbShowConnect = new System.Windows.Forms.Label();
            this.btnThayDoiKetNoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên Tài Khoản: ";
            // 
            // txtTenTaiKhoan
            // 
            this.txtTenTaiKhoan.Location = new System.Drawing.Point(132, 58);
            this.txtTenTaiKhoan.Name = "txtTenTaiKhoan";
            this.txtTenTaiKhoan.Size = new System.Drawing.Size(187, 26);
            this.txtTenTaiKhoan.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật Khẩu: ";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(132, 102);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(187, 26);
            this.txtMatKhau.TabIndex = 3;
            // 
            // cbHienThiMatKhau
            // 
            this.cbHienThiMatKhau.AutoSize = true;
            this.cbHienThiMatKhau.Location = new System.Drawing.Point(132, 134);
            this.cbHienThiMatKhau.Name = "cbHienThiMatKhau";
            this.cbHienThiMatKhau.Size = new System.Drawing.Size(170, 24);
            this.cbHienThiMatKhau.TabIndex = 4;
            this.cbHienThiMatKhau.Text = "Hiển Thị Mật Khẩu";
            this.cbHienThiMatKhau.UseVisualStyleBackColor = true;
            this.cbHienThiMatKhau.CheckedChanged += new System.EventHandler(this.cbHienThiMatKhau_CheckedChanged);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Location = new System.Drawing.Point(132, 164);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(101, 30);
            this.btnDangNhap.TabIndex = 5;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            this.btnDangNhap.Enter += new System.EventHandler(this.btnDangNhap_Enter);
            // 
            // lbShowConnect
            // 
            this.lbShowConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbShowConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbShowConnect.Location = new System.Drawing.Point(12, 9);
            this.lbShowConnect.Name = "lbShowConnect";
            this.lbShowConnect.Size = new System.Drawing.Size(253, 43);
            this.lbShowConnect.TabIndex = 6;
            this.lbShowConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThayDoiKetNoi
            // 
            this.btnThayDoiKetNoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThayDoiKetNoi.Location = new System.Drawing.Point(271, 12);
            this.btnThayDoiKetNoi.Name = "btnThayDoiKetNoi";
            this.btnThayDoiKetNoi.Size = new System.Drawing.Size(49, 40);
            this.btnThayDoiKetNoi.TabIndex = 7;
            this.btnThayDoiKetNoi.Text = ".....";
            this.btnThayDoiKetNoi.UseVisualStyleBackColor = true;
            this.btnThayDoiKetNoi.Click += new System.EventHandler(this.btnThayDoiKetNoi_Click);
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 205);
            this.Controls.Add(this.btnThayDoiKetNoi);
            this.Controls.Add(this.lbShowConnect);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.cbHienThiMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTenTaiKhoan);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenTaiKhoan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox cbHienThiMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label lbShowConnect;
        private System.Windows.Forms.Button btnThayDoiKetNoi;
    }
}


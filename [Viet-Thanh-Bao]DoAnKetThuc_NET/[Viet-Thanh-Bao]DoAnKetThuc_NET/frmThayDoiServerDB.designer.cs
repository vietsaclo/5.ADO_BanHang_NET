namespace _Viet_Thanh_Bao_DoAnKetThuc_NET
{
    partial class frmThayDoiServerDB
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbHienThiMatKhau = new System.Windows.Forms.CheckBox();
            this.btnKiemTraKetNoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(13, 13);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(113, 26);
            this.txtServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(133, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "SERVER";
            // 
            // txtDatabase
            // 
            this.txtDatabase.ForeColor = System.Drawing.Color.Red;
            this.txtDatabase.Location = new System.Drawing.Point(13, 62);
            this.txtDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.ReadOnly = true;
            this.txtDatabase.Size = new System.Drawing.Size(113, 26);
            this.txtDatabase.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(133, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "DATABASE";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(13, 109);
            this.txtUserID.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(113, 26);
            this.txtUserID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(133, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "USER ID";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(13, 157);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(113, 26);
            this.txtPassword.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(133, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "PASSWORD";
            // 
            // cbHienThiMatKhau
            // 
            this.cbHienThiMatKhau.AutoSize = true;
            this.cbHienThiMatKhau.Location = new System.Drawing.Point(13, 187);
            this.cbHienThiMatKhau.Name = "cbHienThiMatKhau";
            this.cbHienThiMatKhau.Size = new System.Drawing.Size(170, 24);
            this.cbHienThiMatKhau.TabIndex = 8;
            this.cbHienThiMatKhau.Text = "Hiển Thị Mật Khẩu";
            this.cbHienThiMatKhau.UseVisualStyleBackColor = true;
            this.cbHienThiMatKhau.CheckedChanged += new System.EventHandler(this.cbHienThiMatKhau_CheckedChanged);
            // 
            // btnKiemTraKetNoi
            // 
            this.btnKiemTraKetNoi.Location = new System.Drawing.Point(13, 226);
            this.btnKiemTraKetNoi.Name = "btnKiemTraKetNoi";
            this.btnKiemTraKetNoi.Size = new System.Drawing.Size(132, 30);
            this.btnKiemTraKetNoi.TabIndex = 9;
            this.btnKiemTraKetNoi.Text = "Kiểm Tra Kết Nối";
            this.btnKiemTraKetNoi.UseVisualStyleBackColor = true;
            this.btnKiemTraKetNoi.Click += new System.EventHandler(this.btnKiemTraKetNoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Enabled = false;
            this.btnLuu.Location = new System.Drawing.Point(147, 226);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 30);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmThayDoiServerDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 271);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnKiemTraKetNoi);
            this.Controls.Add(this.cbHienThiMatKhau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmThayDoiServerDB";
            this.Text = "Thay đổi server database";
            this.Load += new System.EventHandler(this.frmThayDoiServerDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbHienThiMatKhau;
        private System.Windows.Forms.Button btnKiemTraKetNoi;
        private System.Windows.Forms.Button btnLuu;
        public System.Windows.Forms.TextBox txtServer;
        public System.Windows.Forms.TextBox txtDatabase;
        public System.Windows.Forms.TextBox txtUserID;
        public System.Windows.Forms.TextBox txtPassword;
    }
}
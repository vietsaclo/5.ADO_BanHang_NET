namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.Reported
{
    partial class frmRptInHoaDon
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
            this.cprtInHoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cprtInHoaDon
            // 
            this.cprtInHoaDon.ActiveViewIndex = -1;
            this.cprtInHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cprtInHoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.cprtInHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cprtInHoaDon.Location = new System.Drawing.Point(0, 0);
            this.cprtInHoaDon.Name = "cprtInHoaDon";
            this.cprtInHoaDon.Size = new System.Drawing.Size(674, 332);
            this.cprtInHoaDon.TabIndex = 0;
            // 
            // frmRptInHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 332);
            this.Controls.Add(this.cprtInHoaDon);
            this.Name = "frmRptInHoaDon";
            this.Text = "frmRptInHoaDon";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer cprtInHoaDon;

    }
}
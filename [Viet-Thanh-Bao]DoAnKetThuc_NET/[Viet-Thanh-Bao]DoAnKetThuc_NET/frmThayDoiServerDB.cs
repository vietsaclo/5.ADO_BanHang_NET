using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Models;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET
{
    public partial class frmThayDoiServerDB : Form
    {
        public _lbDatabase db;

        public frmThayDoiServerDB()
        {
            InitializeComponent();
        }

        private void frmThayDoiServerDB_Load(object sender, EventArgs e)
        {
            txtDatabase.Text = db.dbName;
        }

        private void btnKiemTraKetNoi_Click(object sender, EventArgs e)
        {
            db = new _lbDatabase(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUserID.Text.Trim(), txtPassword.Text.Trim());

            if (db._laKetNoiThanhCong())
            {
                MessageBox.Show("Kết Nối thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuu.Enabled = true;
            }
            else
            {
                MessageBox.Show("Kết Nối Thất Bại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLuu.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThiMatKhau.Checked)
                txtPassword.PasswordChar = '\0';
            else
                txtPassword.PasswordChar = '*';
        }
    }
}

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
using _Viet_Thanh_Bao_DoAnKetThuc_NET.QuanLy;
using System.Threading;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.BanHang;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET
{
    public partial class frmDangNhap : Form
    {
        private _lbDatabase db;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void initDatabase()
        {
            lbShowConnect.Text = string.Format("Server= {0}; Database= {1}; User ID= {2}; pwd= {3}", db.svName, db.dbName, db.uID, _lbDatabase._maHoaChuoi(db.pwd));
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            db = new _lbDatabase();
            initDatabase();



        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (!db._laKetNoiThanhCong())
            {
                MessageBox.Show("Kết nối đến Database Thất Bại!");
                return;
            }
            string tenTK = txtTenTaiKhoan.Text.Trim(),
                matKhau = txtMatKhau.Text.Trim();
            if (string.IsNullOrEmpty(tenTK))
            {
                MessageBox.Show("Tên tài khoản khác rỗng!");
                txtTenTaiKhoan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Mật khẩu khác rỗng!");
                txtMatKhau.Focus();
                return;
            }

            string query = string.Format("SELECT * FROM NHANVIEN WHERE TAIKHOAN = '{0}'", tenTK);
            System.Data.DataTable dt = db._layDuLieuXuatRaDatatable(query);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Tên tài khoản này không tồn tại!");
                txtTenTaiKhoan.Focus();
                return;
            }
            string passDB = dt.Rows[0]["MATKHAU"].ToString();
            if (!passDB.Equals(matKhau))
            {
                MessageBox.Show("Mật khẩu bạn nhập không khớp!");
                txtMatKhau.Focus();
                return;
            }

            int laAdmin = int.Parse(dt.Rows[0]["PHANQUYEN"].ToString());
            if (laAdmin == 1)
            {
                MessageBox.Show("Bạn đang đăng nhập với quyền Quan Ly", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmQuanLy frmQL = new frmQuanLy();
                frmQL.db = this.db;
                this.Hide();
                frmQL.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn đang đăng nhập với quyền Ban Hang", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmBanHang frmBH = new frmBanHang();
                frmBH.db = this.db;
                frmBH.nhanVien = dt.Rows[0];
                this.Hide();
                frmBH.ShowDialog();
                this.Close();
            }
        }

        private void cbHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThiMatKhau.Checked)
                txtMatKhau.PasswordChar = '\0';
            else
                txtMatKhau.PasswordChar = '*';
        }

        private void btnThayDoiKetNoi_Click(object sender, EventArgs e)
        {
            frmThayDoiServerDB changeSV = new frmThayDoiServerDB();
            changeSV.db = this.db;
            DialogResult res = changeSV.ShowDialog();
            if (res == DialogResult.OK)
            {
                db = changeSV.db;
                initDatabase();
            }
        }

        private void btnDangNhap_Enter(object sender, EventArgs e)
        {
            if (!db._laKetNoiThanhCong())
            {
                MessageBox.Show("Kết nối đến Database Thất Bại!");
                return;
            }
            string tenTK = txtTenTaiKhoan.Text.Trim(),
                matKhau = txtMatKhau.Text.Trim();
            if (string.IsNullOrEmpty(tenTK))
            {
                MessageBox.Show("Tên tài khoản khác rỗng!");
                txtTenTaiKhoan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Mật khẩu khác rỗng!");
                txtMatKhau.Focus();
                return;
            }

            string query = string.Format("SELECT * FROM NHANVIEN WHERE TAIKHOAN = '{0}'", tenTK);
            System.Data.DataTable dt = db._layDuLieuXuatRaDatatable(query);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Tên tài khoản này không tồn tại!");
                txtTenTaiKhoan.Focus();
                return;
            }
            string passDB = dt.Rows[0]["MATKHAU"].ToString();
            if (!passDB.Equals(matKhau))
            {
                MessageBox.Show("Mật khẩu bạn nhập không khớp!");
                txtMatKhau.Focus();
                return;
            }

            int laAdmin = int.Parse(dt.Rows[0]["PHANQUYEN"].ToString());
            if (laAdmin == 1)
            {
                MessageBox.Show("Bạn đang đăng nhập với quyền Quan Ly", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmQuanLy frmQL = new frmQuanLy();
                frmQL.db = this.db;
                this.Hide();
                frmQL.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn đang đăng nhập với quyền Ban Hang", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmBanHang frmBH = new frmBanHang();
                frmBH.db = this.db;
                frmBH.nhanVien = dt.Rows[0];
                this.Hide();
                frmBH.ShowDialog();
                this.Close();
            }
        }
    }
}
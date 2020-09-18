using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Models;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.QuanLy
{
    public partial class frmTheLoai : Form
    {
        public frmTheLoai()
        {
            InitializeComponent();
        }
        //===================Khai Báo Thuộc Tính===========================
        private SqlDataAdapter da;
        private DataRow dr;
        private DataSet ds;
        public _lbDatabase db;
        private DataTable dtGvTheLoai;
        private DataTable dtGvTheLoaiTruyenTenLoai;

        private void KhaiBaoThuocTinh()
        {
            ds = new DataSet();

            da = new SqlDataAdapter("select * from TheLoai", db.Conn);
            da.Fill(ds, "TheLoai");
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[0] };


        }
        //===================End Khai Báo Thuộc Tính=======================

        //====================Tải Dữ Liệu Lên==============================
        //Tải Dữ Liệu Đầy Đủ Của Bảng
        private void TaiDuLieuGridviewTheLoai()
        {
            db.Conn.Open();
            string query = "select * from TheLoai";
            da = new SqlDataAdapter(query, db.Conn);
            dtGvTheLoai = new DataTable();
            da.Fill(dtGvTheLoai);
            dtGvTheLoai.PrimaryKey = new DataColumn[] { dtGvTheLoai.Columns[0] };

            dgvTheLoai.DataSource = dtGvTheLoai;
            db.Conn.Close();
        }
        //Kiểm Tra Tồn Tại
        private bool laTimThayMotDongCoTen(string tenLoai)
        {
            foreach (DataRow dr in dtGvTheLoai.Rows)
                if (dr[1].ToString().Equals(tenLoai))
                    return true;
            return false;
        }
        
        //Lấy Dữ Liệu từ Bảng Thể Loại
        private DataRow layDuLieuTuBangTheLoai(string MaLoai)
        {
            DataRow dr = dtGvTheLoai.Rows.Find(MaLoai);
            return dr;
        }
        //Tải Dữ Liệu Bảng Gridview với tham số truyền vào là tên loại
        private void TaiDuLieuGridViewTheLoaiTimKiem(string TenLoai)
        {
            db.Conn.Open();
            string query = string.Format("select * from TheLoai where TenLoai like '%'+ '{0}' + '%'", TenLoai);
            ds = new DataSet();
            da = new SqlDataAdapter(query, db.Conn);
            dtGvTheLoaiTruyenTenLoai = new DataTable();
            da.Fill(dtGvTheLoaiTruyenTenLoai);
            //dtGvTheLoaiTruyenTenLoai.PrimaryKey = new DataColumn[] { dtGvTheLoai.Columns[0] };
            dgvTheLoai.DataSource = dtGvTheLoaiTruyenTenLoai;

            db.Conn.Close();
        }
        //========================================================================

        //==========================Kiểm Tra Dữ Liệu Ô nhập=======================
        private bool KiemTraORong()
        {
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                return false;
            }
            return true;
        }


        //========================================================================

        //==================Các Nút Thực Hiện Chức Năng====================
        //Nút Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenLoai.Enabled = true;
            txtTenLoai.Clear();
            txtTimKiem.Clear();
            txtTenLoai.Focus();
            btnLuu.Enabled = true;

        }
        //Nút Tìm Kiếm
        private void btnTim_Click(object sender, EventArgs e)
        {
            var TenLoaiTimKiem = txtTimKiem.Text.Trim();
            TaiDuLieuGridViewTheLoaiTimKiem(TenLoaiTimKiem);
        }
        //Nút Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn Có Chắc Muốn Thoát Chương Trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }

        }
        //Nút Load Lại Dữ Liệu
        private void btnLoadLaiDuLieu_Click(object sender, EventArgs e)
        {
            KhaiBaoThuocTinh();
            txtTenLoai.Enabled = false;
            TaiDuLieuGridviewTheLoai();
        }
        //Nút Xóa
        private String getMaLoai(String tenLoai)
        {
            db.Conn.Open();
            String selectStr = "select MALOAI from TheLoai where TenLoai = N'" + tenLoai + "'";
            SqlCommand cmd = new SqlCommand(selectStr, db.Conn);
            String maLoai = cmd.ExecuteScalar().ToString();
            db.Conn.Close();
            return maLoai;

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {

            string TenLoai = txtTenLoai.Text.Trim();
            String maLoai = getMaLoai(TenLoai);
            if (string.IsNullOrEmpty(TenLoai))
            {
                MessageBox.Show("Vui Lòng Chọn Thể Loại Bạn Muốn Xóa");
                return;
            }
            DialogResult r = MessageBox.Show("Xác Nhận Bạn Muốn Xóa ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                DataRow dr = ds.Tables["TheLoai"].Rows.Find(maLoai);
                if (dr != null)
                {
                    dr.Delete();
                    da = new SqlDataAdapter("SELECT * FROM TheLoai", db.Conn);
                    SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                    int task = da.Update(ds, "TheLoai");
                    if (task > 0)
                    {
                        MessageBox.Show("Xoa Thanh Cong");
                        TaiDuLieuGridviewTheLoai();
                    }
                    else
                        MessageBox.Show("Xoa That Bai");
                }
            }
        }
        //Sự Kiện Nhấn DataGridview sẽ hiện lên textbox
        private void dgvTheLoai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowID = e.RowIndex;
            if (RowID < 0 || RowID > dgvTheLoai.Rows.Count)
                return;
            dr = dtGvTheLoai.Rows[RowID];
            txtMaLoai.Text = dr[0].ToString();
            txtTenLoai.Text = dr[1].ToString();
            btnSua.Enabled = true;
        }

        //=================================================================

        private void Form2_Load(object sender, EventArgs e)
        {
            KhaiBaoThuocTinh();
            TaiDuLieuGridviewTheLoai();
            btnSua.Enabled = false;
        }
        //===================================================================








        //Loi 1:31 pm : Chua lam doc sua=========

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Tenloai = txtTenLoai.Text;
            if (!KiemTraORong())
            {
                MessageBox.Show("Vui Lòng Bạn Nhập Đầy Đủ ", "Thông Báo");
                return;
            }
            if (laTimThayMotDongCoTen(txtTenLoai.Text) && txtTenLoai.Enabled == true)
            {
                DialogResult r = MessageBox.Show(string.Format("Tên Loại Này Đã Tồn Tại!!! \nNếu Bạn Muốn Thêm Thì Nhấn N0 Và Nhập Tên Khác !!!\nNếu Bạn Muốn Cập Nhập Thì Nhấn YES"), "Thông Báo", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                   
                }
                else
                {
                    txtTenLoai.Focus();
                }
            }
            else if (!laTimThayMotDongCoTen(txtTenLoai.Text) && txtTenLoai.Enabled == true)
            {

                DataRow dr = dtGvTheLoai.NewRow();
                dr[0] = laySoIDMax() + 1;
                dr[1] = Tenloai;
                dr[2] = 0;
                dtGvTheLoai.Rows.Add(dr);
                da = new SqlDataAdapter("select * from TheLoai", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da);
                int task = da.Update(dtGvTheLoai);
                if (task > 0)
                {
                    MessageBox.Show("Tác Vụ Thành Công", "Thông Báo");
                    TaiDuLieuGridviewTheLoai();
                }
                else
                {
                    MessageBox.Show("Tác Vụ Thất Bại", "Thông Báo");
                }

            }
            else
            {
                MessageBox.Show("Fail", "Thông báo ");
                return;
            }
        }

        private int laySoIDMax()
        {
            List<int> lst = new List<int>();
            foreach (DataRow dr in dtGvTheLoai.Rows)
                lst.Add(int.Parse(dr[0].ToString()));
            return lst.Max();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            txtTenLoai.Enabled = true;
            DialogResult r = MessageBox.Show("Bạn muốn sữa nó không ? ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                txtTenLoai.Enabled = false;
            }
            else
            {
                dr[0] = txtMaLoai.Text;
                dr[1] = txtTenLoai.Text.Trim();
                dr[2] = 0;
                da = new SqlDataAdapter("select * from TheLoai", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da);
                int task = da.Update(dtGvTheLoai);
                if (task > 0)
                {
                    MessageBox.Show("Tác Vụ Thành Công", "Thông Báo");
                    TaiDuLieuGridviewTheLoai();
                }
                else
                {
                    MessageBox.Show("Tác Vụ Thất Bại", "Thông Báo");
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
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
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Reported;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.QuanLy
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }
        private DataSet ds;
        public _lbDatabase db;
        private SqlDataAdapter da;
        private DataRow dr;
        private DataTable dtgvSanPham;
        private void KhaiBaoThuocTinh()
        {
            ds = new DataSet();

            da = new SqlDataAdapter("select * from sanpham", db.Conn);
            da.Fill(ds, "SanPham");
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[0] };

            da = new SqlDataAdapter("select * from SANPHAM,THELOAI where SANPHAM.THELOAI=THELOAI.MALOAI", db.Conn);
            da.Fill(ds, "LoaiSanPham");
            ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns[0] };
        }
        //=====Tải Dữ Liệu========
        private void TaiDuLieuSanPham()
        {
            //select MaSP,TenSP,SLTON,DONGIA,TENLOAI,SANPHAM.DAXOA,IMG,THELOAI from SANPHAM,THELOAI where SANPHAM.THELOAI=THELOAI.MALOAI

            string query = "select * from SanPham";
            da = new SqlDataAdapter(query, db.Conn);
            dtgvSanPham = new DataTable();
            da.Fill(dtgvSanPham);
            dtgvSanPham.PrimaryKey = new DataColumn[] { dtgvSanPham.Columns[0] };

            dgvSanPham.DataSource = dtgvSanPham;
        }

        private void LoadComBox()
        {
            cbLoaiSanPham.DataSource = ds.Tables["LoaiSanPham"];
            cbLoaiSanPham.DisplayMember = "TenLoai";
            cbLoaiSanPham.ValueMember = "MaLoai";
        }
        //========================
        private void Form3_Load(object sender, EventArgs e)
        {
            KhaiBaoThuocTinh();
            TaiDuLieuSanPham();
            LoadComBox();
        }
        //======== Sự Kiện ========
        private void chkAnh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnh.Checked == true)
            {
                lbAnh.Visible = txtAnh.Visible = btnOpen.Visible = pictureBox1.Visible = true;
            }
            else
            {
                lbAnh.Visible = txtAnh.Visible = btnOpen.Visible = pictureBox1.Visible = false;
            }
        }

        private void chkTim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTim.Checked)
            {
                lbTim.Visible = txtTim.Visible = btnTim.Visible = true;
            }
            else
            {
                lbTim.Visible = txtTim.Visible = btnTim.Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn Có Chắc Muốn Thoát Chương Trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cbLoaiSanPham.SelectedItem = 0;
            txtTenSanPham.ReadOnly = txtDonGia.ReadOnly = txtSoLuongTon.ReadOnly = txtAnh.ReadOnly = false;
            txtTenSanPham.Text = txtDonGia.Text = txtSoLuongTon.Text = txtAnh.Text = "";
            txtTenSanPham.Focus();
        }


        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            TaiDuLieuSanPham();
            txtTenSanPham.Text = txtDonGia.Text = txtSoLuongTon.Text = txtAnh.Text = "";
            txtTenSanPham.ReadOnly = txtDonGia.ReadOnly = txtSoLuongTon.ReadOnly = txtAnh.ReadOnly = true;
        }

        private void txtSoLuongTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void open()
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                SaveFileDialog s = new SaveFileDialog();
                f.InitialDirectory = "D:/Hình Ảnh/";
                f.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
                f.Title = "Chọn hình ảnh của mặt hàng!";
                f.FilterIndex = 2;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //&& s.ShowDialog == DialogResult.OK
                    pictureBox1.Image = Image.FromFile(f.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                    txtAnh.Text = f.FileName;

                }
            }
            catch { }
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            open();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowID = e.RowIndex;
            if (rowID < 0 || rowID > dtgvSanPham.Rows.Count)
                return;
            dr = dtgvSanPham.Rows[rowID];
            cbLoaiSanPham.SelectedValue = dr["TheLoai"].ToString();
            txtMaSP.Text = dr["MaSP"].ToString();
            //txtMaSP.DataBindings.Clear();
            //tx.DataBindings.Add("Text", dtgvSanPham.DataSource, "MaSP");
            txtTenSanPham.DataBindings.Clear();
            txtTenSanPham.DataBindings.Add("Text", dgvSanPham.DataSource, "TenSP");
            txtSoLuongTon.DataBindings.Clear();
            txtSoLuongTon.DataBindings.Add("Text", dgvSanPham.DataSource, "SLTON");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("Text", dgvSanPham.DataSource, "DONGIA");
            txtAnh.DataBindings.Clear();
            txtAnh.DataBindings.Add("Text", dgvSanPham.DataSource, "IMG");
            //cbLoaiSanPham.DataBindings.Clear();
            //cbLoaiSanPham.DataBindings.Add("Text", dgvSanPham.DataSource, "TenLoai");
        }
        private bool TenSanPhamTonTai(string TenSP)
        {
            foreach (DataRow dr in dtgvSanPham.Rows)
            {
                if (dr[1].ToString().Equals(TenSP))
                    return true;
            }
            return false;
        }
        private int laySoIDMax()
        {
            List<int> lst = new List<int>();
            foreach (DataRow dr in dtgvSanPham.Rows)
                lst.Add(int.Parse(dr[0].ToString()));
            return lst.Max();
        }
        //chưa xong
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSanPham.Text) || string.IsNullOrEmpty(txtDonGia.Text) || string.IsNullOrEmpty(txtSoLuongTon.Text))
            {
                MessageBox.Show("Không được để rỗng");
                return;
            }
            if (string.IsNullOrEmpty(txtAnh.Text))
            {
                txtAnh.Text = "imgNotFound.png";
            }
            if (TenSanPhamTonTai(txtTenSanPham.Text) && txtTenSanPham.ReadOnly == false)
            {
                DialogResult r = MessageBox.Show(string.Format("Tên Sản Phẩm Này Đã Tồn Tại!!! \nNếu Bạn Muốn Thêm Thì Nhấn N0 Và Nhập Tên Khác !!!\nNếu Bạn Muốn Cập Nhập Thì Nhấn YES"), "Thông Báo", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {

                }
                else
                {
                    txtTenSanPham.Focus();
                }
            }
            else if (!TenSanPhamTonTai(txtTenSanPham.Text) && txtTenSanPham.Enabled == true)
            {
                MessageBox.Show(cbLoaiSanPham.SelectedValue.ToString());
                DataRow dr = dtgvSanPham.NewRow();
                dr[0] = laySoIDMax() + 1;
                dr[1] = txtTenSanPham.Text.Trim();
                dr[2] = int.Parse(txtSoLuongTon.Text.Trim());
                dr[3] = int.Parse(txtDonGia.Text.Trim());
                dr[4] = cbLoaiSanPham.SelectedValue;
                dr[5] = 0;
                dr[6] = txtAnh.Text.Trim(); 
                dtgvSanPham.Rows.Add(dr);
                //da = new SqlDataAdapter("select * from TheLoai", connection.Conn);
                //em sai khuc  select ha thay
                //thay cho em hoi ,tac vu sua cua em no sai khong thay
                da = new SqlDataAdapter("select * from SANPHAM", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da);
                int task = da.Update(dtgvSanPham);
                if (task > 0)
                {
                    MessageBox.Show("Tác Vụ Thành Công", "Thông Báo");
                    TaiDuLieuSanPham();
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
        private String getMaSP(String TenSP)
        {
            db.Conn.Open();
            String selectStr = "select MaSP from SanPham where TenSP = N'" + TenSP + "'";
            SqlCommand cmd = new SqlCommand(selectStr, db.Conn);
            String maSP = cmd.ExecuteScalar().ToString();
            db.Conn.Close();
            return maSP;

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string tenSP = txtTenSanPham.Text.Trim();
            string maSP = getMaSP(txtTenSanPham.Text.Trim());
            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Vui Lòng Chọn Thể Loại Bạn Muốn Xóa");
                return;
            }
            DialogResult r = MessageBox.Show("Xác Nhận Bạn Muốn Xóa ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                DataRow dr = ds.Tables["SanPham"].Rows.Find(maSP);
                if (dr != null)
                {
                    dr.Delete();
                    da = new SqlDataAdapter("SELECT * FROM SanPham", db.Conn);
                    SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                    int task = da.Update(ds, "SanPham");
                    if (task > 0)
                    {
                        MessageBox.Show("Xóa Thành Công");
                        TaiDuLieuSanPham();
                    }
                    else
                        MessageBox.Show("Xóa Thất Bại");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Em kieu tra no dung la dc roi, con lam kieu nay thi toi thay hoi bat tien
            //Tra no sao thay ?
            //Kiểm tra đúng là đc rồi
            //v kiem tra textbox co rong khong neu khong rong minh sua no dung khong thay
            //Em làm theo yêu cầu giáo viên là đc rồi, cái này sao lại hỏi tôi, tôi chỉ sửa hết lỗi, chứ đâu có biết giao viên bắt làm gì
            //tai nay em lam do an nen phan tac vu thao tac bam sua thi thay no k hop ly cho lam kieu nhu la khong dung nhu app nguoi ta lam thao tac a
            //sua thi bam kieu gi?
            //em nhan ten sp hien len textbox ten sp roi em dua vao do em sua a
            //thôi em tự làm, tôi thấy cách code này hơi tệ, 
            //Cứ làm theo giáo viên là dc rồi, khi nào cần sửa lỗi thì tôi làm, chứ tôi không biết yêu cầu giáo viên làm gì nên tôi không thể biết sửa cái gì
            //Nếu em không hiểu thì phải hỏi Tác Giả
            //a em cam on thay a
            txtTenSanPham.ReadOnly = false;
            DialogResult r = MessageBox.Show("Bạn muốn sữa nó không ? ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                txtTenSanPham.ReadOnly = true;
            }
            else
            {
                dr[0] = txtMaSP.Text;
                dr[1] = txtTenSanPham.Text.Trim();
                dr[2] = int.Parse(txtSoLuongTon.Text.Trim());
                dr[3] = int.Parse(txtDonGia.Text.Trim());
                dr[4] = cbLoaiSanPham.SelectedValue;
                dr[5] = 0;
                dr[6] = txtAnh.Text.Trim();
                da = new SqlDataAdapter("select * from SanPham", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da);
                int task = da.Update(dtgvSanPham);
                if (task > 0)
                {
                    MessageBox.Show("Tác Vụ Thành Công", "Thông Báo");
                    TaiDuLieuSanPham();
                }
                else
                {
                    MessageBox.Show("Tác Vụ Thất Bại", "Thông Báo");
                }

            }



            //=========================
        }

        private void btnXemIn_Click(object sender, EventArgs e)
        {
            //BC_SanPham rpt = new BC_SanPham();
            //rpt.SetDatabaseLogon(db.uID, db.pwd, db.svName, db.dbName);
            //Form1 frmBC = new Form1();
            //frmBC.crpSanPham.ReportSource = rpt;
            //frmBC.crpSanPham.Refresh();
            //frmBC.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
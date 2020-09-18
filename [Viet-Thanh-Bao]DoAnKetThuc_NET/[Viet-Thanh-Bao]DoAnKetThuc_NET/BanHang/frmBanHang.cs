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
using System.Data.SqlClient;
using System.Threading;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Reported;
//using _Viet_Thanh_Bao_DoAnKetThuc_NET.Reported;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.BanHang
{
    public partial class frmBanHang : Form
    {
        private const string imgPolder = @"../../Contents/Images/";
        public _lbDatabase db;
        public DataRow nhanVien;
        private DataSet dset;
        private SqlDataAdapter da;
        private DataRow khachHang;
        private int tongTienHD;
        private int maHDCho = 0;

        public frmBanHang()
        {
            InitializeComponent();
        }

        private DataTable layDanhSachSanPhamTheoGiaTien()
        {
            string quey = string.Empty;
            if (rdoDuoi2Trieu.Checked)
                quey = "SELECT * FROM SANPHAM WHERE DONGIA < 2000000";
            else if (rdoDuoi5Trieu.Checked)
                quey = "SELECT * FROM SANPHAM WHERE DONGIA < 5000000";
            else
                quey = "SELECT * FROM SANPHAM WHERE DONGIA > 5000000";

            DataTable dt = db._layDuLieuXuatRaDatatable(quey);
            return dt;
        }

        private DataTable layDanhSachSanPhamTheoTheLoai(int theLoai)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            if (theLoai == -1)
                query = "SELECT * FROM SANPHAM";
            else
                query = string.Format("SELECT * FROM SANPHAM WHERE THELOAI = {0}", theLoai);
            dt = db._layDuLieuXuatRaDatatable(query);

            return dt;
        }

        private DataTable layDanhSachSanPhamTheoTimKiem(string giaTri)
        {
            DataTable dt = new DataTable();
            string query = string.Format("SELECT * FROM SANPHAM WHERE TENSP LIKE N'%{0}%'", giaTri);
            dt = db._layDuLieuXuatRaDatatable(query);
            return dt;
        }

        private void taiDuLieuLstBoxTheLoai()
        {
            DataTable dt = dset.Tables["TheLoai"];
            DataRow dr = dt.NewRow();
            dr[0] = -1;
            dr[1] = "Tất Cả Sản Phẩm";
            dt.Rows.InsertAt(dr, 0);

            lstBoxTheLoai.DataSource = dt;
            lstBoxTheLoai.DisplayMember = "TenLoai";
            lstBoxTheLoai.ValueMember = "MaLoai";
            lstBoxTheLoai.SelectedIndex = 0;
        }

        private void khoiTaoThuocTinh()
        {
            tongTienHD = 0;
            dset = new DataSet();
            da = new SqlDataAdapter("SELECT * FROM THELOAI", db.Conn);
            da.Fill(dset, "TheLoai");
            dset.Tables[0].PrimaryKey = new DataColumn[] { dset.Tables[0].Columns[0] };

            da = new SqlDataAdapter("SELECT * FROM SANPHAM", db.Conn);
            da.Fill(dset, "SanPham");
            dset.Tables[1].PrimaryKey = new DataColumn[] { dset.Tables[1].Columns[0] };

            da = new SqlDataAdapter("SELECT * FROM KHACHHANG", db.Conn);
            da.Fill(dset, "KhachHang");
            dset.Tables[2].PrimaryKey = new DataColumn[] { dset.Tables[2].Columns[0] };

            da = new SqlDataAdapter("SELECT * FROM HOADON", db.Conn);
            da.Fill(dset, "HoaDon");
            dset.Tables["HoaDon"].PrimaryKey = new DataColumn[] { dset.Tables["HoaDon"].Columns[0] };

            da = new SqlDataAdapter("SELECT * FROM CHITIETHD", db.Conn);
            da.Fill(dset, "ChiTietHD");
            dset.Tables["ChiTietHD"].PrimaryKey = new DataColumn[] { dset.Tables["ChiTietHD"].Columns[0], dset.Tables["ChiTietHD"].Columns[1] };
        }

        private void initMyComponent()
        {
            pnSanPhams.BackColor = Color.FromArgb(180, 174, 181);
            pnLogo.BackgroundImage = Image.FromFile(imgPolder + "logoSieuThi.png");
            pnLogo.BackgroundImageLayout = ImageLayout.Stretch;
            lbIconInfo.Image = Image.FromFile(imgPolder + "iconInfo1.png");
            pnTacVus.BackColor = Color.LightBlue;
            pnThongKeHomNay.BackgroundImage = Image.FromFile(imgPolder + "iconPhanTichBanHang.png");
            pnThongKeHomNay.BackgroundImageLayout = ImageLayout.Center;
            pnBaoCaoHomNay.BackgroundImage = Image.FromFile(imgPolder + "iconBaoCaoBanHang.png");
            pnBaoCaoHomNay.BackgroundImageLayout = ImageLayout.Center;
            pnIconChaoMung.BackgroundImage = Image.FromFile(imgPolder + "iconChaoMungUser.png");
            pnIconChaoMung.BackgroundImageLayout = ImageLayout.Center;
            lbKhachHangDoi.TextAlign = ContentAlignment.MiddleCenter;
            lbKhachHangDoi.Text = "Khách Hàng Đang Chờ In Hóa Đơn";
            rdoDuoi2Trieu.Checked = true;
            lbChaoMung.TextAlign = ContentAlignment.MiddleLeft;
            lbChaoMung.Text = string.Format("Chào Mừng Nhân Viên: {0} ({1})", nhanVien["TaiKhoan"].ToString(), nhanVien["TenNV"].ToString());
            cboxLocTheoLichSu.SelectedIndex = 0;
        }

        private void frmQuanLy_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            khoiTaoThuocTinh();
            initMyComponent();
            taiDuLieuLstBoxTheLoai();
            taiSanPhams(layDanhSachSanPhamTheoTheLoai(-1));
            taiTrvLichSuGiaoDich();
        }

        private void taiSanPhams(DataTable dt)
        {
            Thread th = new Thread(() =>
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    pnSanPhams.Controls.Clear();
                    pnSanPhams.AutoScroll = true;
                    Panel pn;
                    int dem = 0, px = 0, py = 0, space = 20, sl = dt.Rows.Count;
                    ttripProgessBarTienTrinhTaiSanPhams.Minimum = 0;
                    ttripProgessBarTienTrinhTaiSanPhams.Maximum = sl;
                    ttripProgessBarTienTrinhTaiSanPhams.Step = 1;
                    for (int i = 0; i < sl; i++)
                    {
                        pn = sanXuatPanel(dt.Rows[i]);

                        pn.Location = new Point(px++ * (pn.Width + space) + space, py * (pn.Height + space) + space);
                        pnSanPhams.Controls.Add(pn);
                        ttripProgessBarTienTrinhTaiSanPhams.PerformStep();
                        if (i == sl - 1)
                            ttripProgessBarTienTrinhTaiSanPhams.Value = 0;
                        if (dem++ == 4)
                        {
                            px = 0;
                            py += 1;
                            dem = 0;
                        }
                    }
                });
            });
            th.Start();
        }

        private Panel sanXuatPanel(DataRow dr)
        {
            Panel pn = new Panel();
            pn.Width = 200;
            pn.Height = 280;
            pn.BackColor = Color.Gray;
            pn.Cursor = Cursors.Default;
            pn.BorderStyle = BorderStyle.Fixed3D;
            pn.Tag = dr;

            Panel pnAnh = new Panel();
            pnAnh.Name = "pnAnhSanPham";
            pnAnh.Size = new Size(180, 150);
            pnAnh.Top = 10;
            pnAnh.Left = 10;
            pnAnh.BackColor = Color.Red;
            Image img = Image.FromFile(imgPolder + "imgNotFound.png");
            pnAnh.BackgroundImage = img;
            pnAnh.BackgroundImageLayout = ImageLayout.Stretch;

            Label lbMaSP = new Label();
            lbMaSP.AutoSize = true;
            lbMaSP.TextAlign = ContentAlignment.MiddleCenter;
            lbMaSP.BackColor = Color.DarkRed;
            lbMaSP.ForeColor = Color.Gold;
            lbMaSP.Text = string.Format("MaSP: {0}", dr["MaSP"].ToString());
            lbMaSP.Top = lbMaSP.Left = 5;
            pnAnh.Controls.Add(lbMaSP);

            Label lbGia = new Label();
            lbGia.Width = 180;
            lbGia.Height = 20;
            lbGia.Top = 170;
            lbGia.Left = 10;
            lbGia.BackColor = Color.DarkRed;
            lbGia.ForeColor = Color.Gold;
            lbGia.Font = new Font(lbGia.Font.FontFamily, 11.0f);
            lbGia.TextAlign = ContentAlignment.MiddleCenter;
            lbGia.Text = string.Format("Giá {0:N0} VND", dr["DonGia"]);

            Label lbSLTon = new Label();
            lbSLTon.Width = 180;
            lbSLTon.Height = 20;
            lbSLTon.Top = 200;
            lbSLTon.Left = 10;
            lbSLTon.ForeColor = Color.Gold;
            lbSLTon.Font = new Font(lbGia.Font.FontFamily, 11.0f);
            lbSLTon.TextAlign = ContentAlignment.MiddleCenter;
            lbSLTon.Text = string.Format("Tồn Kho {0:N0} Cái", dr["SLTon"]);

            Label lbTenSanPham = new Label();
            lbTenSanPham.Width = 180;
            lbTenSanPham.Height = 40;
            lbTenSanPham.Top = 230;
            lbTenSanPham.Left = 10;
            lbTenSanPham.ForeColor = Color.Gold;
            lbTenSanPham.Font = new Font(lbGia.Font.FontFamily, 7f);
            lbTenSanPham.TextAlign = ContentAlignment.MiddleCenter;
            lbTenSanPham.Text = dr["TenSP"].ToString();

            pn.Controls.Add(pnAnh);
            pn.Controls.Add(lbGia);
            pn.Controls.Add(lbSLTon);
            pn.Controls.Add(lbTenSanPham);

            pnAnh.Cursor = Cursors.Hand;
            pnAnh.MouseMove += pnAnh_MouseMove;
            pnAnh.MouseLeave += pnAnh_MouseLeave;
            pnAnh.Click += pnAnh_Click;

            return pn;
        }

        void pnAnh_Click(object sender, EventArgs e)
        {
            Form frmThongTin = new Form();
            frmThongTin.StartPosition = FormStartPosition.CenterScreen;
            frmThongTin.Text = "Thông Tin Người Mua";
            frmThongTin.Width = 500;
            frmThongTin.Height = 210;
            frmThongTin.Font = new Font(frmThongTin.Font.FontFamily, 15f);

            TextBox tbHoTen = new TextBox();
            tbHoTen.Name = "tbHoTen";
            tbHoTen.Width = 250;
            tbHoTen.Height = 20;
            tbHoTen.Top = tbHoTen.Left = 20;
            frmThongTin.Controls.Add(tbHoTen);

            Label lb_1 = new Label();
            lb_1.AutoSize = true;
            lb_1.Text = "(* Phải Điền)";
            lb_1.ForeColor = Color.DarkRed;
            lb_1.Top = 20;
            lb_1.Left = 300;
            frmThongTin.Controls.Add(lb_1);

            TextBox tbSDT = new TextBox();
            tbSDT.Name = "tbSDT";
            tbSDT.Width = 250;
            tbSDT.Height = 20;
            tbSDT.Top = 70;
            tbSDT.Left = 20;
            frmThongTin.Controls.Add(tbSDT);

            Label lb_2 = new Label();
            lb_2.AutoSize = true;
            lb_2.Text = "(* Phải Điền)";
            lb_2.ForeColor = Color.DarkRed;
            lb_2.Top = 70;
            lb_2.Left = 300;
            frmThongTin.Controls.Add(lb_2);

            Button btnXacNhan = new Button();
            btnXacNhan.Text = "Xác Nhận";
            btnXacNhan.Top = 120;
            btnXacNhan.Left = 20;
            btnXacNhan.Width = 150;
            btnXacNhan.Height = 40;
            frmThongTin.Controls.Add(btnXacNhan);

            Button btnHuy = new Button();
            btnHuy.Text = "Hủy Mua";
            btnHuy.Top = 120;
            btnHuy.Left = 200;
            btnHuy.Width = 150;
            btnHuy.Height = 40;
            frmThongTin.Controls.Add(btnHuy);

            btnHuy.Click += btnHuy_Click;
            btnXacNhan.Click += btnXacNhan_Click;
            tbSDT.KeyPress += tbSDT_KeyPress;

            if (khachHang != null)
            {
                tbHoTen.Text = khachHang["TenKH"].ToString();
                tbSDT.Text = khachHang["DienThoai"].ToString();
                tbHoTen.ReadOnly = tbSDT.ReadOnly = true;
            }
            btnXacNhan.Tag = (sender as Panel).Parent.Tag;
            frmThongTin.ShowDialog();
        }

        void tbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        void btnXacNhan_Click(object sender, EventArgs e)
        {
            Form frm = ((sender as Control).Parent as Form);
            string hoTen = frm.Controls["tbHoTen"].Text,
                SDT = frm.Controls["tbSDT"].Text;
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Ho Ten Phai Khac Rong!");
                frm.Controls["tbHoTen"].Focus();
                return;
            }
            if (string.IsNullOrEmpty(SDT))
            {
                MessageBox.Show("SDT Phai Khac Rong!");
                frm.Controls["tbSDT"].Focus();
                return;
            }
            if (SDT.Length > 11 || SDT.Length < 9)
            {
                MessageBox.Show("SDT Nhập Không Đúng Định Dạng!");
                frm.Controls["tbSDT"].Focus();
                return;
            }

            if (khachHang == null)
            {
                khachHang = dset.Tables["KhachHang"].NewRow();
                khachHang["TenKH"] = hoTen;
                khachHang["DienThoai"] = SDT;
                bool task = ganKhachHangDangDoi(khachHang);
                if (!task)
                {
                    MessageBox.Show("Đã Có lỗi phát Sinh!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frm.Close();
            DataRow drSanPham = ((sender as Button).Tag as DataRow);
            themMotSanPham(drSanPham);
            tabCT.SelectedIndex = 1;
        }

        private bool ganKhachHangDangDoi(DataRow khachHang)
        {
            int tid = db._laySoLuong("SELECT MAX(MAKH) FROM KHACHHANG");
            if (tid == -1)
                tid = 1;
            lbMaKhachHangGH.Text = (tid + 1).ToString();
            khachHang["MaKH"] = tid + 1;
            khachHang["DaXoa"] = 0;
            lbTenKhachHangGH.Text = khachHang["TenKH"].ToString();
            lbSDTGH.Text = khachHang["DienThoai"].ToString();
            themMotKhachHangCho(khachHang);
            dset.Tables["KhachHang"].Rows.Add(khachHang);
            da = new SqlDataAdapter("SELECT * FROM KHACHHANG", db.Conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            int task = da.Update(dset, "KhachHang");
            return task > 0;
        }

        private void themMotKhachHangCho(DataRow khachHang)
        {
            pnKhachHangDois.Controls.Clear();
            Label lbKhachCho = new Label();
            lbKhachCho.Cursor = Cursors.Hand;
            lbKhachCho.Width = 300;
            lbKhachCho.Height = 30;
            lbKhachCho.BackColor = Color.DarkRed;
            lbKhachCho.ForeColor = Color.Gold;
            string str = string.Format("Mã: {0} | Tên: {1} | SDT: {2}", khachHang["MaKH"], khachHang["TenKH"], khachHang["DienThoai"]);
            lbKhachCho.TextAlign = ContentAlignment.MiddleCenter;
            lbKhachCho.Text = str;
            lbKhachCho.Top = 4;
            lbKhachCho.Left = 10;
            lbKhachCho.Click += lbKhachCho_Click;
            lbKhachCho.MouseMove += lbKhachCho_MouseMove;
            lbKhachCho.MouseLeave += lbKhachCho_MouseLeave;
            pnKhachHangDois.Controls.Add(lbKhachCho);
        }

        void lbKhachCho_MouseLeave(object sender, EventArgs e)
        {
            ttripLblShowTrangThai.Text = string.Empty;
        }

        void lbKhachCho_MouseMove(object sender, MouseEventArgs e)
        {
            ttripLblShowTrangThai.Text = "Nhấn Trái Chuộc vào để xóa khách hàng này";
        }

        void lbKhachCho_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn Có Chắc Muốn Xóa Khách Hàng Đang Chờ Này Không!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                khachHang = null;
                ganKhachHangTuChoiMua();
            }
        }

        private void ganKhachHangTuChoiMua()
        {
            pnKhachHangDois.Controls.Clear();
            lbMaKhachHangGH.Text = txtTongSoLuong.Text = lbTenKhachHangGH.Text = lbSDTGH.Text = txtTongTien.Text = "null";
            pnGioHang.Controls.Clear();
            tabCT.SelectedIndex = 0;
        }

        private Panel laSanPhamTonTaiTrongGioHang(string maSP)
        {
            foreach (Control ctr in pnGioHang.Controls)
                if ((ctr.Tag as DataRow)["maSP"].ToString().Equals(maSP))
                    return ctr as Panel;
            return null;
        }

        private void themMotSanPham(DataRow dr)
        {
            Panel pnSPTim = laSanPhamTonTaiTrongGioHang(dr["maSP"].ToString());
            if (pnSPTim != null)
            {
                int sl = int.Parse(pnSPTim.Controls["lbSoLuongSanPham"].Tag.ToString());
                Label lbSL = pnSPTim.Controls["lbSoLuongSanPham"] as Label;
                lbSL.Tag = (sl + 1).ToString();
                lbSL.Text = string.Format("SL Mua: {0}", lbSL.Tag);
                setTongTienGioHang();
                return;
            }
            Panel pnItemGioHang = sanXuatPanel(dr);
            pnItemGioHang.Height = 350;

            Label lbSperator = new Label();
            lbSperator.Width = 200;
            lbSperator.Height = 10;
            lbSperator.BackColor = Color.White;
            lbSperator.Top = 280;
            pnItemGioHang.Controls.Add(lbSperator);

            Label lbChonSoLuongMua = new Label();
            lbChonSoLuongMua.AutoSize = false;
            lbChonSoLuongMua.Width = 200;
            lbChonSoLuongMua.TextAlign = ContentAlignment.MiddleCenter;
            lbChonSoLuongMua.Text = "CHỌN SỐ LƯỢNG SẢN PHẨM";
            lbChonSoLuongMua.Top = 290;
            lbChonSoLuongMua.ForeColor = Color.DarkRed;
            lbChonSoLuongMua.Font = new Font(lbChonSoLuongMua.Font.FontFamily, 9f, FontStyle.Bold);
            pnItemGioHang.Controls.Add(lbChonSoLuongMua);

            Label lbGiamSL = new Label();
            Label lbTangSL = new Label();
            lbGiamSL.Text = "-";
            lbTangSL.Text = "+";
            lbGiamSL.Cursor = lbTangSL.Cursor = Cursors.Hand;
            lbGiamSL.Width = lbGiamSL.Height = lbTangSL.Width = lbTangSL.Height = 30;
            lbGiamSL.BackColor = lbTangSL.BackColor = Color.DarkSalmon;
            lbGiamSL.ForeColor = lbTangSL.ForeColor = Color.DarkRed;
            lbGiamSL.Font = lbTangSL.Font = new Font(lbGiamSL.Font.FontFamily, 14f, FontStyle.Bold);
            lbGiamSL.Top = lbTangSL.Top = 310;
            lbGiamSL.TextAlign = lbTangSL.TextAlign = ContentAlignment.MiddleCenter;
            lbGiamSL.Left = 10;
            lbTangSL.Left = 150;
            lbGiamSL.Click += lbGiamSL_Click;
            lbTangSL.Click += lbTangSL_Click;

            pnItemGioHang.Controls.Add(lbGiamSL);
            pnItemGioHang.Controls.Add(lbTangSL);

            Label lbSoLuongSanPham = new Label();
            lbSoLuongSanPham.Name = "lbSoLuongSanPham";
            lbSoLuongSanPham.Tag = 1;
            lbSoLuongSanPham.Width = 100;
            lbSoLuongSanPham.Height = 30;
            lbSoLuongSanPham.BackColor = Color.DarkOliveGreen;
            lbSoLuongSanPham.ForeColor = Color.White;
            lbSoLuongSanPham.TextAlign = ContentAlignment.MiddleCenter;
            lbSoLuongSanPham.Text = string.Format("SL Mua: {0}", lbSoLuongSanPham.Tag);
            lbSoLuongSanPham.Top = 310;
            lbSoLuongSanPham.Left = 45;
            pnItemGioHang.Controls.Add(lbSoLuongSanPham);

            pnItemGioHang.Controls["pnAnhSanPham"].Click -= pnAnh_Click;
            pnItemGioHang.Controls["pnAnhSanPham"].Click += pnAnhSanPhamGioHang_click;
            int space = 20;
            int soT = pnGioHang.Controls.Count / 4, soL = pnGioHang.Controls.Count % 4;// soT: Buoc Nhay Top | soL: Buoc Nhay L
            if (pnGioHang.Controls.Count == 0)
                soL = 0;
            int top = soT * (pnItemGioHang.Height + space),
                left = soL * (pnItemGioHang.Width + space);
            pnItemGioHang.Top = top;
            pnItemGioHang.Left = left;
            pnGioHang.Controls.Add(pnItemGioHang);
            setTongTienGioHang();
        }

        void lbTangSL_Click(object sender, EventArgs e)
        {
            Panel pnItemGH = (sender as Label).Parent as Panel;
            Label lbSL = pnItemGH.Controls["lbSoLuongSanPham"] as Label;
            int sl = int.Parse(lbSL.Tag.ToString());
            lbSL.Tag = sl + 1;
            lbSL.Text = string.Format("SL Mua: {0}", lbSL.Tag);
            setTongTienGioHang();
        }

        void lbGiamSL_Click(object sender, EventArgs e)
        {
            Panel pnItemGH = (sender as Label).Parent as Panel;
            Label lbSL = pnItemGH.Controls["lbSoLuongSanPham"] as Label;
            int sl = int.Parse(lbSL.Tag.ToString());
            if (sl == 1) return;
            lbSL.Tag = sl - 1;
            lbSL.Text = string.Format("SL Mua: {0}", lbSL.Tag);
            setTongTienGioHang();
        }

        private void pnAnhSanPhamGioHang_click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn Có Chắc Muốn Xóa Sản Phẩm Này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Control pnItemGioHang = (sender as Panel).Parent;
                pnGioHang.Controls.Remove(pnItemGioHang);
                List<Control> lstCtr = new List<Control>();
                foreach (Control ctr in pnGioHang.Controls)
                    lstCtr.Add(ctr);
                pnGioHang.Controls.Clear();
                foreach (Control ctr in lstCtr)
                    themMotSanPham(ctr.Tag as DataRow);
            }
        }

        void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác Nhận Hủy", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Form frm = ((sender as Control).Parent as Form);
                frm.Close();
            }
        }

        void pnAnh_MouseLeave(object sender, EventArgs e)
        {
            Control ct = (sender as Control).Parent;
            ct.BackColor = Color.Gray;
            DataRow dr = ct.Tag as DataRow;
            ttripLblShowTrangThai.Text = string.Empty;
        }

        void pnAnh_MouseMove(object sender, MouseEventArgs e)
        {
            Control ct = (sender as Control).Parent;
            ct.BackColor = Color.DarkOrchid;
            DataRow dr = ct.Tag as DataRow;
            ttripLblShowTrangThai.Text = dr["TenSP"].ToString();
        }

        private void lbIconInfo_MouseMove(object sender, MouseEventArgs e)
        {
            lbIconInfo.Image = Image.FromFile(imgPolder + "iconInfo2.png");
            lbIconInfo.Cursor = Cursors.Hand;
        }

        private void lbIconInfo_MouseLeave(object sender, EventArgs e)
        {
            lbIconInfo.Image = Image.FromFile(imgPolder + "iconInfo1.png");
        }

        private void pnThongKeHomNay_MouseMove(object sender, MouseEventArgs e)
        {
            Control ct = sender as Control;
            ct.BackColor = Color.LightCoral;
        }

        private void pnThongKeHomNay_MouseLeave(object sender, EventArgs e)
        {
            Control ct = sender as Control;
            ct.BackColor = Color.LightBlue;
        }

        private void lstBoxTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maLoai = -2;
            if (int.TryParse(lstBoxTheLoai.SelectedValue.ToString(), out maLoai))
                taiSanPhams(layDanhSachSanPhamTheoTheLoai(maLoai));
        }

        private void rdoDuoi2Trieu_CheckedChanged(object sender, EventArgs e)
        {
            taiSanPhams(layDanhSachSanPhamTheoGiaTien());
        }

        int h = 0, m = 0, s = 0;
        private void tmerPhienLamViec_Tick(object sender, EventArgs e)
        {
            ttripLblShowThoiGianPhienLamViec.Text = string.Format("Thời Gian Phiên: {0} : {1} : {2} s", h, m, s++);
            if (s == 60)
            {
                s = 0;
                m += 1;
            }
            if (m == 60)
            {
                m = 0;
                h += 1;
            }
        }

        private void pnIconChaoMung_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác Nhận Đăng Xuất Khỏi Chương Trình", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                frmDangNhap frmDN = new frmDangNhap();
                this.Hide();
                frmDN.ShowDialog();
                this.Close();
            }
        }

        private int setTongTienGioHang()
        {
            int tong = 0,
                tongSL = 0;
            foreach (Control ctr in pnGioHang.Controls)
            {
                int sl = int.Parse(ctr.Controls["lbSoLuongSanPham"].Tag.ToString()),
                    donGia = int.Parse((ctr.Tag as DataRow)["DonGia"].ToString());
                tong += sl * donGia;
                tongSL += sl;
            }
            tongTienHD = tong;
            txtTongTien.Text = string.Format("{0:N0} VND", tongTienHD);
            txtTongSoLuong.Text = string.Format("{0:N0} CÁI", tongSL);
            return tongTienHD;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (khachHang == null) return;
            inHoaDon();
            maHDCho = 0;
        }

        private void inHoaDon()
        {
            //Report
            if (!themGiaoDichVaoDatabase() || maHDCho == 0) return;
            rptInHoaDon rptInHD = new rptInHoaDon();
            rptInHD.SetDatabaseLogon(db.uID, db.pwd, db.svName, db.dbName);
            rptInHD.SetParameterValue("locTheoMaHoaDon", maHDCho);
            rptInHD.SetParameterValue("lbKhachHang", khachHang["TENKH"].ToString());
            rptInHD.SetParameterValue("lbMaKhachHang", khachHang["maKH"].ToString());
            rptInHD.SetParameterValue("lbSDT", khachHang["DienThoai"].ToString());
            frmRptInHoaDon frmInHD = new frmRptInHoaDon();
            frmInHD.db = db;
            frmInHD.cprtInHoaDon.ReportSource = rptInHD;
            frmInHD.cprtInHoaDon.Refresh();
            frmInHD.ShowDialog();

            khachHang = null;
            ganKhachHangTuChoiMua();
            taiTrvLichSuGiaoDich();
            MessageBox.Show("Giao Dich Thanh Cong!");
        }

        private bool themGiaoDichVaoDatabase()
        {
            DataRow drNewHD = dset.Tables["HoaDon"].NewRow();
            drNewHD["maHD"] = 0;
            drNewHD["maKH"] = khachHang["MaKH"];
            drNewHD["maNV"] = nhanVien["MaNV"];
            drNewHD["NgayLap"] = DateTime.Now;
            drNewHD["TongTien"] = tongTienHD;
            drNewHD["DaXoa"] = false;
            drNewHD["LaDangCho"] = true;
            dset.Tables["HoaDon"].Rows.Add(drNewHD);
            da = new SqlDataAdapter("SELECT * FROM HOADON", db.Conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            int task = da.Update(dset, "HoaDon");
            if (task < 1)
            {
                MessageBox.Show("Giao dịch thất bại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //them vao bang chitet
            drNewHD = db._layDuLieuXuatRaDatatable("SELECT * FROM HOADON WHERE LADANGCHO = 1").Rows[0];
            int sl;
            string maSP, maHD = drNewHD["MaHD"].ToString();
            DataRow drNewCT;
            maHDCho = int.Parse(maHD);
            foreach (Control ctr in pnGioHang.Controls)
            {
                sl = int.Parse(ctr.Controls["lbSoLuongSanPham"].Tag.ToString());
                maSP = (ctr.Tag as DataRow)["MaSP"].ToString();
                drNewCT = dset.Tables["ChiTietHD"].NewRow();
                drNewCT["MaHD"] = maHD;
                drNewCT["MaSP"] = maSP;
                drNewCT["soLuong"] = sl;
                drNewCT["DaXoa"] = false;
                dset.Tables["ChiTietHD"].Rows.Add(drNewCT);
            }
            da = new SqlDataAdapter("SELECT * FROM CHITIETHD", db.Conn);
            cmd = new SqlCommandBuilder(da);
            task = da.Update(dset, "ChiTietHD");
            if (task < 0)
            {
                MessageBox.Show("Giao dịch thất bại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            task = db._thucThiCauLenh(string.Format("UPDATE HOADON SET LADANGCHO = 0 WHERE MAHD = {0}", maHD));
            if (task < 0)
            {
                MessageBox.Show("Giao dịch thất bại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnMuaThem_Click(object sender, EventArgs e)
        {
            tabCT.SelectedIndex = 0;
        }

        private void taiTrvLichSuGiaoDich(DataTable dtHD)
        {
            trvLichSuGiaoDich.Nodes.Clear();
            DataTable dtTemp;
            string maHD;
            for (int i = 0; i < dtHD.Rows.Count; i++)
            {
                maHD = dtHD.Rows[i][0].ToString();
                trvLichSuGiaoDich.Nodes.Add("Ma HD :" + maHD);
                trvLichSuGiaoDich.Nodes[i].Tag = maHD;
                dtTemp = db._layDuLieuXuatRaDatatable(string.Format("SELECT K.TENKH FROM HOADON H, KHACHHANG K WHERE H.MAKH = K.MAKH AND H.MAHD = {0}", int.Parse(maHD)));
                trvLichSuGiaoDich.Nodes[i].Nodes.Add(dtTemp.Rows[0][0].ToString());
                trvLichSuGiaoDich.Nodes[i].Nodes[0].Tag = maHD;
                dtTemp = db._layDuLieuXuatRaDatatable(string.Format("SELECT S.MASP, C.SOLUONG FROM CHITIETHD C, SANPHAM S WHERE C.MASP = S.MASP AND C.MAHD = {0}", int.Parse(maHD)));
                foreach (DataRow dr in dtTemp.Rows)
                    trvLichSuGiaoDich.Nodes[i].Nodes[0].Nodes.Add(string.Format("Ma SP: {0} - SL Mua: {1}", dr["maSP"], dr["soLuong"]));
            }

            trvLichSuGiaoDich.ExpandAll();
        }

        private void taiTrvLichSuGiaoDich()
        {
            string filter = cboxLocTheoLichSu.SelectedItem.ToString();
            if (filter.Equals("Tất Cả"))
                taiTrvLichSuGiaoDich(db._layDuLieuXuatRaDatatable("SELECT * FROM HOADON"));
            else if (filter.Equals("Hôm Nay"))
                taiTrvLichSuGiaoDich(db._layDuLieuXuatRaDatatable(string.Format("SELECT * FROM HOADON WHERE DAY(NGAYLAP) = {0} AND MONTH(NGAYLAP) = {1} AND YEAR(NGAYLAP) = {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)));
            else if (filter.Equals("Tháng Này"))
                taiTrvLichSuGiaoDich(db._layDuLieuXuatRaDatatable(string.Format("SELECT * FROM HOADON WHERE MONTH(NGAYLAP) = {0}", DateTime.Now.Month)));
            else
                taiTrvLichSuGiaoDich(db._layDuLieuXuatRaDatatable(string.Format("SELECT * FROM HOADON WHERE YEAR(NGAYLAP) = {0}", DateTime.Now.Year)));
        }

        private void trvLichSuGiaoDich_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 2) return;
            DataTable dt = db._layDuLieuXuatRaDatatable(string.Format("SELECT * FROM SANPHAM S, CHITIETHD C WHERE S.MASP = C.MASP AND C.MAHD = {0}", int.Parse(node.Tag.ToString())));
            lstBLichSuGiaoDich.Items.Clear();
            foreach (DataRow dr in dt.Rows)
                lstBLichSuGiaoDich.Items.Add("- Ma SP: " + dr["maSP"] + " | " + dr["TenSP"]);
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Xác Nhận Xóa tất cả sản phẩm trong giỏ hàng?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                pnGioHang.Controls.Clear();
                setTongTienGioHang();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            taiSanPhams(layDanhSachSanPhamTheoTimKiem(txtTimKim.Text.Trim()));
            txtTimKim.Clear();
        }

        private void cboxLocTheoLichSu_SelectedIndexChanged(object sender, EventArgs e)
        {
            taiTrvLichSuGiaoDich();
        }
    }
}
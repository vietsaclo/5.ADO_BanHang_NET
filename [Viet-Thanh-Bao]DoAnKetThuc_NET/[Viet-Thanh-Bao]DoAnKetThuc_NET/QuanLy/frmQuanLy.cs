using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Models;
using System.Data.SqlClient;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.QuanLy;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.QuanLy
{
    public partial class frmQuanLy : Form
    {
        int ckMenu = 1,ckThemSua = 0;
        bool ckTouch = false;
        DataSet ds2;
        SqlDataAdapter da2;
        public _lbDatabase db;
        public frmQuanLy()
        {
            InitializeComponent();
        }
        bool checkTenTK(string tentk)
        {
            try
            {
                db.Conn.Open();
                string selectNVcheck = "select * from nhanvien where taikhoan = '" + txtTK.Text + "'  ";
                SqlCommand cmd = new SqlCommand(selectNVcheck, db.Conn);
                int count = (int)cmd.ExecuteScalar();
                db.Conn.Close();
                if (count >= 1)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        void loadingPage()
        {
            //xu ly loading
            int demload = 0;
            //tabControlThongKe.SelectedIndex = 0;
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(30);
                this.prgLoading.Percentage = i;
                demload++;
                if (demload == 1)
                    this.lbLoading.Text = "Loading.";
                if (demload == 2)
                    this.lbLoading.Text = "Loading..";
                if (demload == 3)
                {
                    this.lbLoading.Text = "Loading...";
                    demload = 0;
                }
            }
            xuiButton5.Enabled = true;
            panel1.Visible = false;
            //tabControlThongKe.SelectedTab = tabPage2;
            
        }
        void loadingChart()
        {
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart1.Series.Add("Doanh Thu");
            chart1.Series["Doanh Thu"].ChartType = SeriesChartType.Line;
            chart1.Series["Doanh Thu"].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;
            //cho 8 bang
            chart1.Series["Doanh Thu"].Points.AddXY(0, 0);
            chart1.Series["Doanh Thu"].Points.AddXY("tháng 5", 10);
            chart1.Series["Doanh Thu"].Points.AddXY("tháng 6", 50);
            chart1.Series["Doanh Thu"].Points.AddXY("tháng 7", 50);
        }
        void loadingMoneyForStore()
        {

            string selectCTHoadon = "select  MONTH(NGAYLAP) as ng,YEAR(NGAYLAP) as th, sum(CAST(tongtien AS BIGINT)) as tt, sum(soluong) as sl,  count(distinct hoadon.mahd) as slkh from HOADON, CHITIETHD where HOADON.MAHD = CHITIETHD.MAHD group by MONTH(NGAYLAP),YEAR(NGAYLAP) ";

            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(selectCTHoadon, db.Conn);
            da1.Fill(ds1, "tabletk");
            this.dataGridView1.DataSource = ds1.Tables["tabletk"];
            this.dataGridView1.Refresh();

        }
        void loadingTableNV()
        {
            string selectNV = "select manv,tennv,taikhoan,matkhau,PHANQUYEN from nhanvien ";
            ds2 = new DataSet();
            da2 = new SqlDataAdapter(selectNV, db.Conn);
            da2.Fill(ds2, "tablenv");
            this.dataGridView2.DataSource = ds2.Tables["tablenv"];
            this.dataGridView2.Refresh();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //sqlconn.Open();
            //Xu ly loading luc bat dau
            xuiButton5.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread th1 = new Thread(new ThreadStart(loadingPage));
            th1.Start();
            //Xy ly item tren do thi
            loadingChart();
            loadingMoneyForStore();
            loadingTableNV();
            comboBox1.Items.Add("0");
            comboBox1.Items.Add("1");
            comboBox1.SelectedIndex = 0;
            dataGridView2.Rows[0].Cells[0].Selected = false;
            btnLuu.Enabled = false;
            btnSuain.Enabled = false;
            btnThemin.Enabled = true;
            xuiButton7.Enabled = true;


            formLoadSP();
            formloadTheLoai();
        }
        private void xuiButton5_Click(object sender, EventArgs e)
        {




            btnThongKe.Visible = false;
            btnNhanVien.Visible = false;
            btnSanPham.Visible = false;
            btnTheLoai.Visible = false;

            int newX = this.panelThongKe.Location.X;
            int newY = this.panelThongKe.Location.Y;
            //this.panel1.Location = new Point(newX + 1000, newY);
            ckMenu++;
            if (ckMenu % 2 == 0)
                ckTouch = true;
            else
                ckTouch = false;
            if (ckTouch == true)
            {
                for (int i = 0; i < 20; i++)
                {
                    panelMenu.Width += 10;
                    this.panelThongKe.Location = new Point(this.panelThongKe.Location.X + 10, newY);
                    Thread.Sleep(10);

                }
                btnThongKe.Visible = true;
                btnNhanVien.Visible = true;
                btnSanPham.Visible = true;
                btnTheLoai.Visible = true;
            }
            else
            {
                btnThongKe.Visible = false;
                btnNhanVien.Visible = false;
                btnSanPham.Visible = false;
                btnTheLoai.Visible = false;
                for (int i = 0; i < 20; i++)
                {
                    panelMenu.Width -= 10;
                    Console.Write(newX);
                    Console.Write(this.panelThongKe.Location.X);
                    this.panelThongKe.Location = new Point(this.panelThongKe.Location.X - 10, newY);
                    Thread.Sleep(10);
                }
            }


        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            tabControlThongKe.SelectedIndex = 0;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            tabControlThongKe.SelectedIndex = 1;
        }
        private void panelMenu_Click(object sender, EventArgs e)
        {
            btnThongKe.Visible = false;
            btnNhanVien.Visible = false;
            btnSanPham.Visible = false;
            btnTheLoai.Visible = false;

            int newXbtnIMG = this.xuiButton5.Location.X;
            int newX = this.panelThongKe.Location.X;
            int newY = this.panelThongKe.Location.Y;
            //this.panel1.Location = new Point(newX + 1000, newY);
            ckMenu++;
            if (ckMenu % 2 == 0)
                ckTouch = true;
            else
                ckTouch = false;
            if (ckTouch == true)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (xuiButton5.Location.X < 90)
                        this.xuiButton5.Location = new Point(this.xuiButton5.Location.X + 10, this.xuiButton5.Location.Y);

                    panelMenu.Width += 10;
                    this.panelThongKe.Location = new Point(this.panelThongKe.Location.X + 10, newY);
                    Thread.Sleep(10);

                }
                btnThongKe.Visible = true;
                btnNhanVien.Visible = true;
                btnSanPham.Visible = true;
                btnTheLoai.Visible = true;
            }
            else
            {
                btnThongKe.Visible = false;
                btnNhanVien.Visible = false;
                btnSanPham.Visible = false;
                btnTheLoai.Visible = false;
                for (int i = 0; i < 20; i++)
                {
                    if (xuiButton5.Location.X != newXbtnIMG - 90)
                        this.xuiButton5.Location = new Point(this.xuiButton5.Location.X - 10, this.xuiButton5.Location.Y);

                    panelMenu.Width -= 10;
                    Console.Write(newX);
                    Console.Write(this.panelThongKe.Location.X);
                    this.panelThongKe.Location = new Point(this.panelThongKe.Location.X - 10, newY);
                    Thread.Sleep(10);
                }
            }
        }

        public bool checkRow()
        {
            try
            {
                db.Conn.Open();
                string selectCTHoadon = "select  MONTH(NGAYLAP) as ng,YEAR(NGAYLAP) as th, sum(CAST(tongtien AS BIGINT)) as tt, sum(soluong) as sl,  count(distinct hoadon.mahd) as slkh from HOADON, CHITIETHD where HOADON.MAHD = CHITIETHD.MAHD group by MONTH(NGAYLAP),YEAR(NGAYLAP) ";
                SqlCommand cmd = new SqlCommand(selectCTHoadon, db.Conn);
                int count = (int)cmd.ExecuteScalar();
                db.Conn.Close();
                if (count < 3)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string doanhso1, thang, doanhso2, doanhso3;
                float tongtien2, tongtien1, tongtien3;
                DataGridViewRow row1, row2, row3;
                int thanggiua, thangtrai, thangphai;

                thang = "";
                //gets a collection that contains all the rows
                if (e.RowIndex == 0)
                {
                    row1 = this.dataGridView1.Rows[e.RowIndex];
                    row2 = this.dataGridView1.Rows[e.RowIndex + 1];
                    row3 = this.dataGridView1.Rows[e.RowIndex + 2];
                    label77.Text = row1.Cells[2].Value.ToString() + " VND";
                    label88.Text = row1.Cells[3].Value.ToString() + " Sản Phẩm";
                    label99.Text = row1.Cells[4].Value.ToString() + " Khách Hàng";
                    thang = row1.Cells[0].Value.ToString();
                    thanggiua = int.Parse(thang); thangtrai = thanggiua - 1; thangphai = thanggiua + 1;
                    doanhso1 = row1.Cells[2].Value.ToString();
                    doanhso2 = row2.Cells[2].Value.ToString();
                    doanhso3 = row3.Cells[2].Value.ToString();
                    tongtien1 = (float.Parse(doanhso1) / 22000);
                    tongtien2 = (float.Parse(doanhso2) / 22000);
                    tongtien3 = (float.Parse(doanhso3) / 22000);
                    chart1.Series["Doanh Thu"].Points.Clear();
                    chart1.Series["Doanh Thu"].Points.AddXY(0, 0);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangtrai, tongtien1);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thanggiua, tongtien2);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangphai, tongtien3);
                }
                else if (e.RowIndex == dataGridView1.RowCount - 1)
                {
                    row1 = this.dataGridView1.Rows[dataGridView1.RowCount - 3];
                    row2 = this.dataGridView1.Rows[dataGridView1.RowCount - 2];
                    row3 = this.dataGridView1.Rows[dataGridView1.RowCount - 1];
                    label77.Text = row3.Cells[2].Value.ToString() + " VND";
                    label88.Text = row3.Cells[3].Value.ToString() + " Sản Phẩm";
                    label99.Text = row3.Cells[4].Value.ToString() + " Khách Hàng";
                    thang = row3.Cells[0].Value.ToString();
                    thanggiua = int.Parse(thang) - 1; thangtrai = int.Parse(thang) - 2; thangphai = int.Parse(thang);
                    doanhso1 = row1.Cells[2].Value.ToString();
                    doanhso2 = row2.Cells[2].Value.ToString();
                    doanhso3 = row3.Cells[2].Value.ToString();
                    tongtien1 = (float.Parse(doanhso1) / 22000);
                    tongtien2 = (float.Parse(doanhso2) / 22000);
                    tongtien3 = (float.Parse(doanhso3) / 22000);
                    chart1.Series["Doanh Thu"].Points.Clear();
                    chart1.Series["Doanh Thu"].Points.AddXY(0, 0);

                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangtrai, tongtien1);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thanggiua, tongtien2);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangphai, tongtien3);
                }
                else
                {
                    row1 = this.dataGridView1.Rows[e.RowIndex - 1];
                    row2 = this.dataGridView1.Rows[e.RowIndex];
                    row3 = this.dataGridView1.Rows[e.RowIndex + 1];
                    label77.Text = row2.Cells[2].Value.ToString() + " VND";
                    label88.Text = row2.Cells[3].Value.ToString() + " Sản Phẩm";
                    label99.Text = row2.Cells[4].Value.ToString() + " Khách Hàng";
                    thang = row2.Cells[0].Value.ToString();
                    thanggiua = int.Parse(thang); thangtrai = thanggiua - 1; thangphai = thanggiua + 1;
                    doanhso1 = row1.Cells[2].Value.ToString();
                    doanhso2 = row2.Cells[2].Value.ToString();
                    doanhso3 = row3.Cells[2].Value.ToString();
                    tongtien1 = (float.Parse(doanhso1) / 22000);
                    tongtien2 = (float.Parse(doanhso2) / 22000);
                    tongtien3 = (float.Parse(doanhso3) / 22000);
                    chart1.Series["Doanh Thu"].Points.Clear();
                    chart1.Series["Doanh Thu"].Points.AddXY(0, 0);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangtrai, tongtien1);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thanggiua, tongtien2);
                    chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangphai, tongtien3);
                    if (checkRow())
                    {
                        row1 = this.dataGridView1.Rows[e.RowIndex];
                        label77.Text = row1.Cells[2].Value.ToString() + " VND";
                        label88.Text = row1.Cells[3].Value.ToString() + " Sản Phẩm";
                        label99.Text = row1.Cells[4].Value.ToString() + " Khách Hàng";
                        thang = row1.Cells[0].Value.ToString();
                        thangtrai = int.Parse(thang);
                        doanhso1 = row1.Cells[2].Value.ToString();
                        tongtien1 = (float.Parse(doanhso1) / 22000);
                        chart1.Series["Doanh Thu"].Points.Clear();
                        chart1.Series["Doanh Thu"].Points.AddXY(0, 0);
                        chart1.Series["Doanh Thu"].Points.AddXY("tháng" + thangtrai, tongtien1);
                    }
                }


                //int thanggiua = int.Parse(thang),thangtrai = thanggiua - 1,thangphai = thanggiua + 1;


                var chart = chart1.ChartAreas[0];
                chart.AxisX.Minimum = 1;
                chart.AxisX.Maximum = 4;
                if (tongtien1 > 100000 && tongtien2 > 100000 || tongtien3 > 100000)
                {
                    chart.AxisY.Minimum = 0;
                    chart.AxisY.Maximum = 1000000;

                    chart.AxisY.Interval = 0;
                    chart.AxisY.Interval = 100000;
                }
                else if (tongtien1 > 1000000 || tongtien2 > 1000000 || tongtien3 > 1000000)
                {
                    chart.AxisY.Minimum = 0;
                    chart.AxisY.Maximum = 10000000;

                    chart.AxisY.Interval = 0;
                    chart.AxisY.Interval = 1000000;
                }
                else
                {

                    chart.AxisY.Minimum = 0;
                    chart.AxisY.Maximum = 100000;

                    chart.AxisY.Interval = 0;
                    chart.AxisY.Interval = 10000;
                }


            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row1;
                row1 = this.dataGridView2.Rows[e.RowIndex];
                //gets a collection that contains all the rows
                lbmanv.Text = row1.Cells[0].Value.ToString();
                txtTK.Text = row1.Cells[2].Value.ToString();
                txtMK.Text = row1.Cells[3].Value.ToString();
                txtTenNV.Text = row1.Cells[1].Value.ToString();
                comboBox1.SelectedItem = row1.Cells[4].Value.ToString();
                xuiButton7.Enabled = true;
                btnThemin.Enabled = false;
                btnSuain.Enabled = true;
                btnLuu.Enabled = true;
            }
        }

        private void btnThemin_Click(object sender, EventArgs e)
        {
            txtMK.Enabled = true;
            txtTenNV.Enabled = true;
            txtTK.Enabled = true;
            comboBox1.Enabled = true;
            dataGridView2.Enabled = false;
            xuiButton7.Enabled = false;
            btnSuain.Enabled = false;
            btnLuu.Enabled = true;
            btnThemin.Enabled = false;
            txtTenNV.Clear();
            txtTK.Clear();
            txtMK.Clear();
            ckThemSua = 1;
        }

        private void xuiButton7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    db.Conn.Open();
                    string deleteNV = "delete from nhanvien where tennv = '" + txtTenNV.Text + "' and taikhoan = '" + txtTK.Text + "'";
                    SqlCommand cmd = new SqlCommand(deleteNV, db.Conn);
                    cmd.ExecuteNonQuery();
                    loadingTableNV();
                    if (db.Conn.State == ConnectionState.Open)
                        db.Conn.Close();
                    MessageBox.Show("Xoa Thanh Cong");
                }
                catch
                {
                    MessageBox.Show("Xoa That Bai");
                }
            }
        }

        private void btnSuain_Click(object sender, EventArgs e)
        {
            ckThemSua = 2;
            txtMK.Enabled = true;
            txtTenNV.Enabled = true;
            txtTK.Enabled = true;
            comboBox1.Enabled = true;
            btnThemin.Enabled = false;
            xuiButton7.Enabled = false;
            dataGridView2.Enabled = false;
            btnLuu.Enabled = true;

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ckThemSua == 1)//them
            {
                if (String.IsNullOrEmpty(txtTenNV.Text) && String.IsNullOrEmpty(txtTK.Text) && String.IsNullOrEmpty(txtMK.Text) && String.IsNullOrEmpty(comboBox1.SelectedText))
                {
                    txtMK.Text = "";
                    txtTenNV.Text = "";
                    txtTK.Text = "";
                    MessageBox.Show("Ban Nhap sai!.Hay dien day du cac o con trong!");

                }
                else
                {
                    if (checkTenTK(txtTK.Text))
                    {
                        if (db.Conn.State == ConnectionState.Closed)
                            db.Conn.Open();
                        string insertNV;
                        insertNV = "insert into NHANVIEN(TENNV,TAIKHOAN,MATKHAU,PHANQUYEN) values(N'" + txtTenNV.Text + "','" + txtTK.Text + "','" + txtMK.Text + "'," + comboBox1.SelectedItem + ");";
                        SqlCommand cmd = new SqlCommand(insertNV, db.Conn);
                        cmd.ExecuteNonQuery();

                        if (db.Conn.State == ConnectionState.Open)
                            db.Conn.Close();
                        MessageBox.Show("Them Thanh Cong");
                        txtMK.Enabled = false;
                        txtTenNV.Enabled = false;
                        txtTK.Enabled = false;
                        comboBox1.Enabled = false;
                        dataGridView2.Enabled = true;
                    }
                    else
                    {
                        txtMK.Text = "";
                        txtTenNV.Text = "";
                        txtTK.Text = "";
                        MessageBox.Show("Ban Nhap sai!.Hay dien day du cac o con trong!");
                    }
                }
            }
            if (ckThemSua == 2)//update
            {
                try
                {
                    if (db.Conn.State == ConnectionState.Closed)
                        db.Conn.Open();
                    string updateNV = "update nhanvien set   tennv = N'" + txtTenNV.Text + "' , taikhoan = '" + txtTK.Text + "' , matkhau = '" + txtMK.Text + "' , phanquyen='" + comboBox1.SelectedText + "' where manv = '" + int.Parse(lbmanv.Text) + "'";
                    SqlCommand cmd = new SqlCommand(updateNV, db.Conn);
                    cmd.ExecuteNonQuery();
                    if (db.Conn.State == ConnectionState.Open)
                        db.Conn.Close();
                    MessageBox.Show("Update Thanh Cong");
                    txtMK.Enabled = false;
                    txtTenNV.Enabled = false;
                    txtTK.Enabled = false;
                    comboBox1.Enabled = false;
                    dataGridView2.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Update That Bai");
                }
            }
            ckThemSua = 0;
            loadingTableNV();
        }

        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            frmTheLoai frm = new frmTheLoai();
            frm.db = this.db;
            frm.ShowDialog();
            tabControlThongKe.SelectedIndex = 3;
        }
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham frm = new frmSanPham();
            frm.db = this.db;
            frm.ShowDialog();
            tabControlThongKe.SelectedIndex = 2;
        }









        private DataSet ds111;
        public _lbDatabase db111;
        private SqlDataAdapter da111;
        private DataRow dr111;
        private DataTable dtgvSanPham;

        private void KhaiBaoThuocTinh()
        {
            ds111 = new DataSet();

            da111 = new SqlDataAdapter("select * from sanpham", db.Conn);
            da111.Fill(ds111, "SanPham");
            ds111.Tables[0].PrimaryKey = new DataColumn[] { ds111.Tables[0].Columns[0] };

            da111 = new SqlDataAdapter("select * from SANPHAM,THELOAI where SANPHAM.THELOAI=THELOAI.MALOAI", db.Conn);
            da111.Fill(ds111, "LoaiSanPham");
            ds111.Tables[1].PrimaryKey = new DataColumn[] { ds111.Tables[1].Columns[0] };
        }
        private void TaiDuLieuSanPham()
        {
            //select MaSP,TenSP,SLTON,DONGIA,TENLOAI,SANPHAM.DAXOA,IMG,THELOAI from SANPHAM,THELOAI where SANPHAM.THELOAI=THELOAI.MALOAI

            string query = "select * from SanPham";
            da111 = new SqlDataAdapter(query, db.Conn);
            dtgvSanPham = new DataTable();
            da111.Fill(dtgvSanPham);
            dtgvSanPham.PrimaryKey = new DataColumn[] { dtgvSanPham.Columns[0] };

            dgvSanPham.DataSource = dtgvSanPham;
        }
        private void LoadComBox()
        {
            cbLoaiSanPham.DataSource = ds111.Tables["LoaiSanPham"];
            cbLoaiSanPham.DisplayMember = "TenLoai";
            cbLoaiSanPham.ValueMember = "MaLoai";
        }

        private void chkAnh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnh.Checked == true)
            {
                lbAnh.Visible = txtAnh.Visible = btnOpenSP.Visible = pictureBox1.Visible = true;
            }
            else
            {
                lbAnh.Visible = txtAnh.Visible = btnOpenSP.Visible = pictureBox1.Visible = false;
            }
        }

        private void chkTim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTim.Checked)
            {
                lbTim.Visible = txtTim.Visible = btnTimSP.Visible = true;
            }
            else
            {
                lbTim.Visible = txtTim.Visible = btnTimSP.Visible = false;
            }
        }

        private void btnThoatSP_Click(object sender, EventArgs e)
        {

            DialogResult r = MessageBox.Show("Bạn Có Chắc Muốn Thoát Chương Trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            cbLoaiSanPham.SelectedItem = 0;
            txtTenSanPham.ReadOnly = txtDonGia.ReadOnly = txtSoLuongTon.ReadOnly = txtAnh.ReadOnly = false;
            txtTenSanPham.Text = txtDonGia.Text = txtSoLuongTon.Text = txtAnh.Text = "";
            txtTenSanPham.Focus();
        }

        private void btnTaiLaiSP_Click(object sender, EventArgs e)
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

        private void btnOpenSP_Click(object sender, EventArgs e)
        {
            open();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowID = e.RowIndex;
            if (rowID < 0 || rowID > dtgvSanPham.Rows.Count)
                return;
            dr111 = dtgvSanPham.Rows[rowID];
            cbLoaiSanPham.SelectedValue = dr111["TheLoai"].ToString();
            txtMaSP.Text = dr111["MaSP"].ToString();
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

        private void btnLuuSP_Click(object sender, EventArgs e)
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
                da111 = new SqlDataAdapter("select * from SANPHAM", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da111);
                int task = da111.Update(dtgvSanPham);
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

        private void btnSuaSP_Click(object sender, EventArgs e)
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
                dr111[0] = txtMaSP.Text;
                dr111[1] = txtTenSanPham.Text.Trim();
                dr111[2] = int.Parse(txtSoLuongTon.Text.Trim());
                dr111[3] = int.Parse(txtDonGia.Text.Trim());
                dr111[4] = cbLoaiSanPham.SelectedValue;
                dr111[5] = 0;
                dr111[6] = txtAnh.Text.Trim();
                da111 = new SqlDataAdapter("select * from SanPham", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da111);
                int task = da111.Update(dtgvSanPham);
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

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void formLoadSP()
        {
            KhaiBaoThuocTinh();
            TaiDuLieuSanPham();
            LoadComBox();
        }


        //the loai tabcontrol
        //===================Khai Báo Thuộc Tính===========================

        private SqlDataAdapter da222;
        private DataRow dr222;
        private DataSet ds222;
        public _lbDatabase db222;
        private DataTable dtGvTheLoai;
        private DataTable dtGvTheLoaiTruyenTenLoai;
        private void KhaiBaoThuocTinhTL()
        {
            ds222 = new DataSet();

            da222 = new SqlDataAdapter("select * from TheLoai", db.Conn);
            da222.Fill(ds222, "TheLoai");
            ds222.Tables[0].PrimaryKey = new DataColumn[] { ds222.Tables[0].Columns[0] };


        }
        //===================End Khai Báo Thuộc Tính=======================
        //====================Tải Dữ Liệu Lên==============================
        //Tải Dữ Liệu Đầy Đủ Của Bảng
        private void TaiDuLieuGridviewTheLoai()
        {
            db.Conn.Open();
            string query = "select * from TheLoai";
            da222 = new SqlDataAdapter(query, db.Conn);
            dtGvTheLoai = new DataTable();
            da222.Fill(dtGvTheLoai);
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
            ds222 = new DataSet();
            da222 = new SqlDataAdapter(query, db.Conn);
            dtGvTheLoaiTruyenTenLoai = new DataTable();
            da222.Fill(dtGvTheLoaiTruyenTenLoai);
            //dtGvTheLoaiTruyenTenLoai.PrimaryKey = new DataColumn[] { dtGvTheLoai.Columns[0] };
            dgvTheLoai.DataSource = dtGvTheLoaiTruyenTenLoai;

            db.Conn.Close();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            txtTenLoai.Enabled = true;
            txtTenLoai.Clear();
            txtTimKiem.Clear();
            txtTenLoai.Focus();
            btnLuu.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            var TenLoaiTimKiem = txtTimKiem.Text.Trim();
            TaiDuLieuGridViewTheLoaiTimKiem(TenLoaiTimKiem);
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn Có Chắc Muốn Thoát Chương Trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLoadLaiDuLieu_Click(object sender, EventArgs e)
        {
            KhaiBaoThuocTinh();
            txtTenLoai.Enabled = false;
            TaiDuLieuGridviewTheLoai();
        }
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
                DataRow dr = ds222.Tables["TheLoai"].Rows.Find(maLoai);
                if (dr != null)
                {
                    dr.Delete();
                    da222 = new SqlDataAdapter("SELECT * FROM TheLoai", db.Conn);
                    SqlCommandBuilder cmd = new SqlCommandBuilder(da222);
                    int task = da222.Update(ds222, "TheLoai");
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

        private void dgvTheLoai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowID = e.RowIndex;
            if (RowID < 0 || RowID > dgvTheLoai.Rows.Count)
                return;
            dr222 = dtGvTheLoai.Rows[RowID];
            txtMaLoai.Text = dr222[0].ToString();
            txtTenLoai.Text = dr222[1].ToString();
            btnSuaTL.Enabled = true;
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

        private void btnLuuTL_Click(object sender, EventArgs e)
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
                da222 = new SqlDataAdapter("select * from TheLoai", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da222);
                int task = da222.Update(dtGvTheLoai);
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
    

        private void btnSuaTL_Click(object sender, EventArgs e)
        {
            txtTenLoai.Enabled = true;
            DialogResult r = MessageBox.Show("Bạn muốn sữa nó không ? ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                txtTenLoai.Enabled = false;
            }
            else
            {
                dr222[0] = txtMaLoai.Text;
                dr222[1] = txtTenLoai.Text.Trim();
                dr222[2] = 0;
                da222 = new SqlDataAdapter("select * from TheLoai", db.Conn);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(da222);
                int task = da222.Update(dtGvTheLoai);
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

        //========================================================================
        private void formloadTheLoai()
        {

            KhaiBaoThuocTinh();
            TaiDuLieuGridviewTheLoai();
            btnSuaTL.Enabled = false;
        }
        //===================================================================


    }
}

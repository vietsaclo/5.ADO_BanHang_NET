/*
 * Thư viện xử lý liên quan đến truy vấn data base
 * Tác giả: Nguyễn Quốc Việt - 1999 - 08DHTH1
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.Models
{
    public class _lbDatabase
    {
        public string svName { get; set; }
        public string dbName { get; set; }
        public string uID { get; set; }
        public string pwd { get; set; }
        public string _strConn { get; set; }
        public System.Data.SqlClient.SqlConnection Conn { get; set; }

        public _lbDatabase()
        {
            svName = "VIETSACLO-PC\\SQLEXPRESS";
            dbName = "QL_BANHANG_SIEUTHI";
            uID = "sa";
            pwd = "vietsaclo1999";
            _strConn = string.Format("Server= {0}; Database= {1}; User ID= {2}; pwd= {3}", svName, dbName, uID, pwd);
            Conn = new System.Data.SqlClient.SqlConnection(_strConn);
        }

        public _lbDatabase(string serverName, string DbName, string uId, string pwd)
        {
            svName = serverName;
            dbName = DbName;
            this.uID = uId;
            this.pwd = pwd;
            _strConn = string.Format("Server= {0}; Database= {1}; User ID= {2}; pwd= {3}",serverName, DbName, uId, pwd);
            Conn = new System.Data.SqlClient.SqlConnection(_strConn);
        }

        /// <summary>
        /// Phương thức khởi tạo có tham số
        /// </summary>
        /// <param name="chuoiKetNoi">Truyền vào đây chuổi kết nối tới Database</param>
        public _lbDatabase(string chuoiKetNoi)
        {
            _strConn = chuoiKetNoi;
            Conn = new System.Data.SqlClient.SqlConnection(_strConn);
        }

        private System.Data.SqlClient.SqlConnection _getConn()
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(_strConn);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.Write("Error: {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Hàm kiểm tra xem là kết nối tới cơ sở dữ liệu thành công hay chưa.
        /// </summary>
        /// <returns>true: nếu kết nối là thành công</returns>
        public bool _laKetNoiThanhCong()
        {
            using (System.Data.SqlClient.SqlConnection conn = _getConn())
            {
                return conn != null;
            }
        }

        public int _laySoLuong(string query)
        {
            int sl = -1;
            using (System.Data.SqlClient.SqlConnection conn = _getConn())
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                {
                    try
                    {
                        sl = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Err: "+ex.Message);
                        sl = -1;
                    }
                }
            }
            return sl;
        }
        /// <summary>
        /// Hàm thực hiện chức năng lấy data từ Database thông qua một SqlCommad (không cần gán chuổi kết nối cho SqlCommand)
        /// </summary>
        /// <param name="cmd">SqlCommand muốn thực hiện lấy dữ liệu</param>
        /// <returns>Trả về Một kểu dữ liệu DataTable nếu thực hiện lấy dữ liệu thành công, Null: nếu thực hiện không thành công</returns>
        public System.Data.DataTable _layDuLieuXuatRaDatatable(System.Data.SqlClient.SqlCommand cmd)
        {
            using (System.Data.SqlClient.SqlConnection conn = _getConn())
            {
                if (conn == null) return null;
                using (cmd.Connection = conn)
                {
                    using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        using (System.Data.DataSet ds = new System.Data.DataSet())
                        {
                            try
                            {
                                da.Fill(ds);
                                return ds.Tables[0];
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.Message);
                                return null;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Hàm thực hiện chức năng lấy data từ Database thông qua một một câu lệnh query bất kỳ (không cần gán chuổi kết nối cho query)
        /// </summary>
        /// <param name="cmd">Một câu lệnh truy vấn sql bất kỳ</param>
        /// <returns>Trả về Một kểu dữ liệu DataTable nếu thực hiện lấy dữ liệu thành công, Null: nếu thực hiện không thành công</returns>
        public System.Data.DataTable _layDuLieuXuatRaDatatable(string cmd)
        {
            System.Data.SqlClient.SqlCommand scmd = new System.Data.SqlClient.SqlCommand(cmd);
            return _layDuLieuXuatRaDatatable(scmd);
        }

        /// <summary>
        /// Hàm thực thi một câu lệnh và trả về số dòng bị ảnh hưởng (insert, delete, update)
        /// </summary>
        /// <param name="cmd">sqlCommand</param>
        /// <returns> != -1 nếu thực hiện thành công.</returns>
        public int _thucThiCauLenh(System.Data.SqlClient.SqlCommand cmd)
        {
            using (System.Data.SqlClient.SqlConnection conn = _getConn())
            {
                if (conn == null) return -1;
                using (cmd.Connection = conn)
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Hàm thực thi một câu lệnh và trả về số dòng bị ảnh hưởng (insert, delete, update)
        /// </summary>
        /// <param name="cmd">string một câu lệnh query bấc kỳ</param>
        /// <returns></returns>
        public int _thucThiCauLenh(string cmd)
        {
            System.Data.SqlClient.SqlCommand scmd = new System.Data.SqlClient.SqlCommand(cmd);
            return _thucThiCauLenh(scmd);
        }

        /// <summary>
        /// Hàm mã hóa một chuổi thành 32 ký tự Theo giải thuật Băm MD5
        /// </summary>
        /// <param name="oldPassword">Một chuổi cần được mã hóa</param>
        /// <returns>Một chuổi đã được mã hóa thành công</returns>
        public static String _maHoaChuoi(String oldPassword)
        {
            StringBuilder newPass = new StringBuilder();
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            Byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(oldPassword));
            for (int i = 0; i < data.Length; i++)
            {
                newPass.Append(data[i].ToString("x2"));
            }

            return newPass.ToString();
        }
    }
}
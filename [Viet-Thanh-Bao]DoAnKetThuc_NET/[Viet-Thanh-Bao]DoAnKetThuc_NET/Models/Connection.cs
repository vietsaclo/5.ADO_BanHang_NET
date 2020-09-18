using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.Models
{
    class Connection
    {
        private string Chuoi;
        public SqlConnection Conn;
        private string strConn="Data Source=MSI\\SQLEXPRESS;Initial Catalog=QL_BANHANG_SIEUTHI;User ID=sa ; Password = 123456";
        public string StrConn
        {
            get { return strConn;}
        }
        public string chuoi
        {
            get { return Chuoi; }
        }
        public SqlConnection conn
        {
            get { return Conn; }
        }
        public Connection()
        {
            Chuoi = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QL_BANHANG_SIEUTHI;User ID=sa ; Password = 123456";
            Conn = new SqlConnection(Chuoi);
        }
    }
}

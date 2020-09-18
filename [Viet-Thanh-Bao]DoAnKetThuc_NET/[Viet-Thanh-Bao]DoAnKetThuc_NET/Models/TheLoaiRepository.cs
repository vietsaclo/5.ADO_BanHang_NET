using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Viet_Thanh_Bao_DoAnKetThuc_NET.Models;

using System.Data;
using System.Data.SqlClient;

namespace _Viet_Thanh_Bao_DoAnKetThuc_NET.Models
{
    class TheLoaiRepository
    {
        Connection conn = new Connection();
        public List<THELOAI> SearchTheLoai(string TenLoai)
        {
            using (IDbConnection connection=new SqlConnection(conn.StrConn))
            {
                using (IDbCommand command=connection.CreateCommand())
                {
                    command.CommandText = "SearchTheLoai";
                    command.CommandType = CommandType.StoredProcedure;
                    IDataParameter parameter = command.CreateParameter();
                    parameter.Value = TenLoai;
                    parameter.ParameterName = "@TenLoai";
                    parameter.DbType = DbType.String;
                    connection.Open();
                    using (IDataReader reader=command.ExecuteReader())
                    {
                        List<THELOAI> list = new List<THELOAI>();
                        while (reader.Read())
                        {
                            THELOAI obj = new THELOAI
                            {
                                maLoai=(string)reader["MaLoai"],
                                tenLoai=(string)reader["TenLoai"]
                            };
                            list.Add(obj);
                        }
                        return list;
                    }
                }
            }
        }
    }
}

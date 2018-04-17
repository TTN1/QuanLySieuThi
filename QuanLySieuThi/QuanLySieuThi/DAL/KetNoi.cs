using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace QuanLySieuThi.DAL
{
    public class KetNoi
    {
        SqlConnection _connect;
        public KetNoi()
        {
            _connect = new SqlConnection(@"Data Source=ManhCuong-PC;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        }
        public DataTable GetData(string strSql)
        {
            SqlDataAdapter da = new SqlDataAdapter(strSql, _connect);
            DataTable dt = new DataTable();
            if (ConnectionState.Closed == _connect.State)
            {
                _connect.Open();
            }
            da.Fill(dt);
            _connect.Close();
            return dt;
        }
        public string TangMa(string sql, string Ma)
        {
            SqlCommand cm = new SqlCommand(sql, _connect);      // bắt đầu truy vấn
            cm.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cm);     //vận chuyển dữ liệu về
            DataTable dt = new DataTable();                 //tạo 1 kho ảo để chứa dữ liệu
            da.Fill(dt);
            if (dt.Rows.Count <= 0)
            {
                Ma = Ma + "01";
            }
            else
            {
                int k;
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 2));
                k = k + 1;
                if (k < 10)
                {
                    Ma = Ma + "0";
                }
                else if (k < 100)
                {
                    Ma = Ma + "";
                }
                Ma = Ma + k.ToString();
            }
            return Ma;
        }
        public DataTable GetData(string NameProc, SqlParameter[] para)
        {

            SqlCommand cmd = new SqlCommand(NameProc, _connect);
            cmd.CommandType = CommandType.StoredProcedure;
            if (para != null)
                cmd.Parameters.AddRange(para);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            if (ConnectionState.Closed == _connect.State)
            {
                _connect.Open();
            }
            da.Fill(dt);
            _connect.Close();
            return dt;
        }
        public int ExcuteSQL(string strSQL)
        {
            SqlCommand cmd = new SqlCommand(strSQL, _connect);
            if (ConnectionState.Closed == _connect.State)
            {
                _connect.Open();
            }
            int count = cmd.ExecuteNonQuery();
            _connect.Close();
            return count;
        }
        public int ExcuteSQL(string NameProc, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = NameProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
            }
            cmd.Connection = _connect;
            if (ConnectionState.Closed == _connect.State)
            {
                _connect.Open();
            }
            int count = cmd.ExecuteNonQuery();
            _connect.Close();
            return count;
        }

        public bool KiemTraTonTai(string querySQL, string Ma)
        {
            bool b = false;
            SqlCommand cmd = new SqlCommand(querySQL, _connect);
            _connect.Open();
            SqlDataReader adt = cmd.ExecuteReader();
            while (adt.Read())
            {
                if (Ma == adt[0].ToString())
                {
                    b = true;
                    break;
                }
                else
                {
                    b = false;
                    break;
                }
            }
            _connect.Close();
            return b;
        }
    }
}


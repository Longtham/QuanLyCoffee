using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CoffeeShopManager
{
    class Ketnoi
    {
        public SqlConnection cnn;
        public SqlCommand cmd;
        public DataTable dta;
        public SqlDataAdapter ada;

        public void KetNoi_CSDL()
        {
            String strKetNoi = @"Data Source=DESKTOP-TDHMV39\LONG;Initial Catalog=QLNhahang;Integrated Security=True";
            cnn = new SqlConnection(strKetNoi);
            cnn.Open();
        }

        public void HuyKetNoi()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();

        }

        public DataTable Lay_Dulieu(string Sql)
        {
            KetNoi_CSDL();
            ada = new SqlDataAdapter(Sql, cnn);
            dta = new DataTable();
            ada.Fill(dta);
            return dta;
        }

        public void Execute(string sql)
        {
            KetNoi_CSDL();
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            HuyKetNoi();
        }




        public DataTable ExecuteQuery(string sql,object[] parameter=null)
        {
            KetNoi_CSDL();
            DataTable dta = new DataTable();
            cmd = new SqlCommand(sql, cnn);
            if(parameter != null)
            {
                string[] list = sql.Split(' '); 
                int i=0;
                //Phần tử của list chứa @ sẽ được thay bằng biến trong parameter. 
                // Parameter null thì hàm tương đương với hàm Excute ta đã tạo ở trên.
                foreach (string item in list)
                {
                    if(item.Contains('@')) 
                    {
                        cmd.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dta);
            HuyKetNoi();
            return dta;
        }
        public int ExecuteNonQuery(string sql,object[] parameter=null)
        {
            int dta = 0;
            KetNoi_CSDL();
            SqlCommand cmd = new SqlCommand(sql, cnn);
                 if(parameter != null)
                  {
                      string[] list = sql.Split(' ');
                      int i = 0;
                      foreach(string item in list)
                      {
                          if(item.Contains('@'))
                          {
                              cmd.Parameters.AddWithValue(item, parameter[i]);
                              i++;
                          }
                      }
                  }
            dta = cmd.ExecuteNonQuery();
            HuyKetNoi();
            return dta;
        }
        public object ExecuteScalar(string sql)
        {
            KetNoi_CSDL();
            object dta = 0;
            cmd = new SqlCommand(sql, cnn);
            dta=cmd.ExecuteScalar();
            HuyKetNoi();
            return dta;
        }
    }
}

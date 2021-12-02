using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace CoffeeShopManager
{
    public class Table1
    {
        // các thuộc tính của Table1
        private String status;
        private int iD;
        private String name;
        //Hàm get,set tương ứng
        public int ID { get => iD; set => iD = value; }
        public string Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }


        //Hàm tạo với tham số truyền vào là các thuộc tính
        public Table1(int iD , string name, string status)
        {
            this.ID = iD;
            this.Name = name;
            this.Status = status;

        }

        //Hàm tạo với tham số truyền vào là 1 dòng của bảng TableDrink
        public Table1(DataRow row)
        {
            this.ID = (int)row["TbID"];
            this.Name = row["TbName"].ToString();
            this.Status = row["status"].ToString();

        }
       
    }
}

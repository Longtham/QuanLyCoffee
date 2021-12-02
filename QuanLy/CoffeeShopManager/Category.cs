using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager
{
    public class Category
    {
        public Category(int iD,String name)
        {
            this.ID = iD;
            this.Name = name;

        }
        public Category(DataRow row)
        {
            this.ID = (int)row["DCID"];
            this.Name = row["DCName"].ToString();
        }
        private int iD;
        private String name;
        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoffeeShopManager
{
    class BillInfor
    {
        public BillInfor(int iD,int billID,int drinkID,int count)
        {
            this.ID = iD;
            this.BillID = billID;
            this.DrinkID = drinkID;
            this.Count = count;
        }
        public BillInfor(DataRow row)
        {
            this.ID = (int)row["BIID"];
            this.BillID = (int)row["BillID"];
            this.DrinkID = (int)row["DrinkID"];
            this.Count = (int)row["count"];
        }
        
        private int iD;
        private int billID;
        private int drinkID;
        private int count;
        

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int DrinkID { get => drinkID; set => drinkID = value; }
        public int Count { get => count; set => count = value; }
    }
}

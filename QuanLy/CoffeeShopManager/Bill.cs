using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoffeeShopManager
{
    class Bill
    {
        public Bill(int iD,DateTime? dateCheckIn,DateTime? dateCheckOut,int status,int discount)
        {
            this.ID = iD;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["BillID"];
            this.DateCheckIn = (DateTime)row["DateCheckIn"];
            var dateCheckOut_KT = row["dateCheckOut"];
            if (dateCheckOut_KT.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)dateCheckOut_KT;
            }
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        private int iD;
        private int discount;
        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}

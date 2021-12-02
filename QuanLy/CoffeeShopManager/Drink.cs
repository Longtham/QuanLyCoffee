using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoffeeShopManager
{
    public class Drink
    {
        public Drink(int iD,String name,int dCID,int price)
        {
            this.ID = iD;
            this.Name = name;
            this.DCID = dCID;
            this.Price = price;
        }
        public Drink(DataRow row)
        {
            this.ID = (int)row["DrinkID"];
            this.Name = row["Drinkname"].ToString();
            this.DCID = (int)row["DCID"];
            this.Price = (int)row["price"];
        }

        private int iD;
        private String name;
        private int dCID;
        private int price;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int DCID { get => dCID; set => dCID = value; }
        public int Price { get => price; set => price = value; }
    }
}

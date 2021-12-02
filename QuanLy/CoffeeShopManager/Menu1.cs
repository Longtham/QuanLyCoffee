using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace CoffeeShopManager
{
    public class Menu1
    {
        public Menu1(String drinkName, int count, int price, int totalPrice = 0)
        {
            this.DrinkName = drinkName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu1(DataRow row)
        {
            this.DrinkName = row["Drinkname"].ToString();
            this.Count = (int)row["count"];
            this.Price = (int)row["Price"];
            this.TotalPrice = (int)row["TotalPrice"];

        }

        private String drinkName;
        private int count;
        private int price;
        private int totalPrice;

        public string DrinkName { get => drinkName; set => drinkName = value; }
        public int Count { get => count; set => count = value; }
        public int Price { get => price; set => price = value; }
        public int TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}

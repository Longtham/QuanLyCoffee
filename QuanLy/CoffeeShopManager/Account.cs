using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CoffeeShopManager
{
    public class Account
    {
        private String userName;
        private String disPlayName;
        private String password;
        private int type;

        public Account(string userName,string displayName, int type, string password=null)
        {
            this.UserName = userName;
            this.DisPlayName = displayName;
            this.Type = type;
            this.Password = password;
        }
        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.DisPlayName = row["displayName"].ToString();
            this.Type = (int)row["type"];
            this.Password = row["password"].ToString();
        }
        public string UserName { get => userName; set => userName = value; }
        public string DisPlayName { get => disPlayName; set => disPlayName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }
    }
}

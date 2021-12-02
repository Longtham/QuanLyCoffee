using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace CoffeeShopManager
{
    public partial class BaoCao_Hang : Form
    {
        Ketnoi kn = new Ketnoi();
        public BaoCao_Hang()
        {
            InitializeComponent();
           
            
        }
        private DateTime ngayBatDau;
        private DateTime ngayKetThuc;


        public DateTime NgayKetThuc { get => ngayKetThuc; set => ngayKetThuc = value; }
        public DateTime NgayBatDau { get => ngayBatDau; set => ngayBatDau = value; }

        private void BaoCao_Hang_Load(object sender, EventArgs e)
        {
            string sql = "select Drink.Drinkname,Drink.DrinkID,Sum(BillInfor.count) as [Total] from Drink,Bill,BillInfor " +
                "Where Bill.BillID = BillInfor.BillID and BillInfor.DrinkID=Drink.DrinkID" +
               " and Bill.DateCheckOut >= '" + ngayBatDau.ToString("yyyy-MM-dd") 
               + "' and Bill.DateCheckOut <= '" + ngayKetThuc.ToString("yyyy-MM-dd") + "' group by Drink.Drinkname,Drink.DrinkID";
            DataTable dta = kn.Lay_Dulieu(sql);
            BaoCao_DoUong BC = new BaoCao_DoUong();
            BC.SetDataSource(dta);
            CRV_Hang.ReportSource = BC;
        }

    }
}

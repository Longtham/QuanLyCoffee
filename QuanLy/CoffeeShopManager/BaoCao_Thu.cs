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
    public partial class BaoCao_Thu : Form
    {
        Ketnoi kn = new Ketnoi();
        public BaoCao_Thu()
        {
            InitializeComponent();
        }

        private DateTime batdau;
        private DateTime ketthuc;

        
        public DateTime Ketthuc { get => ketthuc; set => ketthuc = value; }
        public DateTime Batdau { get => batdau; set => batdau = value; }

        private void BaoCao_Thu_Load(object sender, EventArgs e)
        {
            string sql= "Select DateCheckOut,Sum(Total) as[Tong] from Bill where DateCheckOut>='"+Batdau.ToString("yyyy-MM-dd")+"' " +
                " and DateCheckout <= '"+Ketthuc.ToString("yyyy-MM-dd")+"'  group by DateCheckOut";
            DataTable dta = kn.Lay_Dulieu(sql);
            BaoCao_DoanhThu BC = new BaoCao_DoanhThu();
            BC.SetDataSource(dta);
            CRP_DT.ReportSource = BC;
        }
    }
}

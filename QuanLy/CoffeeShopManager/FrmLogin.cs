using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeShopManager
{
    public partial class FrmLogin : Form
    {
        Ketnoi kn = new Ketnoi();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thongbao == DialogResult.Yes) this.Close();
        }

        
        public Account TK_User(string userName)
        {
            DataTable dta=kn.Lay_Dulieu("Select * from account where userName= N'" + userName+"'");
            foreach (DataRow item in dta.Rows )
            {
                return new Account(item);

            }
            return null;

        }
        private void btnDN_Click(object sender, EventArgs e)
        {
            kn.KetNoi_CSDL();
            string TN = txtDN.Text;
            string MK = txtMK.Text;
            string sql_login = "Select Username,PassWord from Account Where Username='" + TN + 
                "'and PassWord='" + MK+"'";
            SqlCommand cmd = new SqlCommand(sql_login, kn.cnn);
            SqlDataReader datRed = cmd.ExecuteReader();
            if (datRed.Read() == true)
            {
                Account account = TK_User(TN); 
                FrmTableManager f = new FrmTableManager(account);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show(" Vui lòng kiểm tra lại tên đăng nhập và mật khẩu! ", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

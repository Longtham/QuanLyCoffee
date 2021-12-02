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
    public partial class FrmThongtin : Form
    {
        Ketnoi kn = new Ketnoi();

        public FrmThongtin(Account acc)
        {
            InitializeComponent();
            TK = acc;   
        }

        public Account tK;
        public Account TK
        {
            get { return tK; }
            set { tK = value;TTTK(tK); }
        }
        void TTTK(Account acc)
        {
            txtTendangnhap.Text = tK.UserName;
            txtTenHienThi.Text = tK.DisPlayName;


        }
        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            if (txtMK.Text == tK.Password && txtMKMoi.Text==txtNhapLai.Text )
            {
                thongbao = MessageBox.Show("Xác nhận cập nhật thông tin .", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (thongbao == DialogResult.Yes)
                {
                    string sql = "Update Account set Displayname= N'" + txtTenHienThi.Text + "',password= N'" + txtMKMoi.Text + 
                        "' where username= N'" + txtTendangnhap.Text + "'";
                    kn.Execute(sql);
                    MessageBox.Show("Bạn đã cập nhật tài khoản thành công .\nVui lòng đăng nhập lại", "Thông báo", MessageBoxButtons.OK);
                    this.Hide();
                    FrmLogin frm = new FrmLogin();
                    frm.Show();

                   
                }
                
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin ", "Thông báo ", MessageBoxButtons.OK);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Hide();
            FrmTableManager frm = new FrmTableManager(tK);
            frm.ShowDialog();

        }

        private void FrmThongtin_Load(object sender, EventArgs e)
        {
            
        }

   
    }
}

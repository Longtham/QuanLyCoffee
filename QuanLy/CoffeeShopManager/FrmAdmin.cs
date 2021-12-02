using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager
{
    public partial class FrmAdmin : Form
    {
        Ketnoi kn = new Ketnoi();
        public FrmAdmin()
        {
            InitializeComponent();
            Load_DL_Category();
            Hienthi_DoUong();
            HienThi_LoaiDoUong();
            HienThi_Ban();
            Hienthi_TT_Ban();
            HienThi_TK();
        }

        private void fAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabDouong_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimDoUong_TextChanged(object sender, EventArgs e)
        {

        }


        //Chức năng Hiển thị danh sách hóa đơn theo ngày

        void Load_HD(DateTime Tungay,DateTime Denngay)
        {

            Grid_Doanhthu.DataSource= kn.ExecuteQuery("Exec HDTheoNgay @TuNgay , @DenNgay ", new object[] { Tungay, Denngay });
        }
        private void btnThongke_Click(object sender, EventArgs e)
        {
            Load_HD(date_Batdau.Value, date_Ketthuc.Value);
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }



        // Tab đồ uống.

        public void Hienthi_DoUong()
        {
            DataTable dta = kn.Lay_Dulieu("Select DrinkID as[Mã đồ uống] ,Drinkname as[Tên đồ uống],DCID as[Mã loại],DCname as[Loại],Price as[Giá] from v_DoUong ");
            Grid_DoUong.DataSource = dta;
            HienThi_DL_DoUong();
        }
        public void Load_DL_Category()
        { 
            cbLoaiDoUong.DataSource= kn.Lay_Dulieu("Select * from DrinkCategory");
            cbLoaiDoUong.DisplayMember = "DCname";
            cbLoaiDoUong.ValueMember = "DCName";
            cbIDCategory.DataSource=kn.Lay_Dulieu("Select * from DrinkCategory");
            cbIDCategory.DisplayMember = "DCID";
            cbIDCategory.ValueMember = "DCID";
        }
        
        public void HienThi_DL_DoUong()
        {

            txtIDDoUong.DataBindings.Clear();
            txtIDDoUong.DataBindings.Add("Text", Grid_DoUong.DataSource, "Mã đồ uống");

            txtTenDoUong.DataBindings.Clear();
            txtTenDoUong.DataBindings.Add("Text", Grid_DoUong.DataSource, "Tên đồ uống");

            cbIDCategory.DataBindings.Clear();
            cbIDCategory.DataBindings.Add("Text", Grid_DoUong.DataSource, "Mã loại");

            cbLoaiDoUong.DataBindings.Clear();
            cbLoaiDoUong.DataBindings.Add("Text", Grid_DoUong.DataSource, "Loại");

            nmGia.DataBindings.Clear();
            nmGia.DataBindings.Add("Text", Grid_DoUong.DataSource, "Giá");




        }

        private void btnTaoDoUong_Click(object sender, EventArgs e)
        {
            txtIDDoUong.Text = "";
            txtTenDoUong.Text = "";
            cbLoaiDoUong.Text = "";
            nmGia.Value = 0;
            
        }

        private void btnThemDoUong_Click(object sender, EventArgs e)
        {
            string sql="insert into Drink values(N'"+txtTenDoUong.Text+"',N'"+cbIDCategory.Text+"','"+nmGia.Value+"')";
            kn.Execute(sql);
            Hienthi_DoUong();

        }

        private void btnXoaDoUong_Click(object sender, EventArgs e)
        {
            kn.Execute("Delete Drink where DrinkID= " + txtIDDoUong.Text);
            Hienthi_DoUong();
        }

        private void btnSuaDoUong_Click(object sender, EventArgs e)
        {
            string sql = "Update Drink set Drinkname= '"+txtTenDoUong.Text+"' ,DCID= '"+cbIDCategory.Text+"' ,price= '"+nmGia.Value+"'where DrinkID='"+txtIDDoUong.Text+"'";
            kn.Execute(sql);
            Hienthi_DoUong();
        }

        private void btnTimDoUong_Click(object sender, EventArgs e)
        {
            string sql = "Select Drink.DrinkID,Drink.DCID,Drink.Drinkname,DrinkCategory.DCName,Drink.Price from Drink,DrinkCategory";
                   sql += " where Drink.DCID = DrinkCategory.DCID and Drink.Drinkname like N'%" + txtTimDoUong.Text + "%'";
            DataTable dta = kn.Lay_Dulieu(sql);
            Grid_DoUong.DataSource = dta;

        }



        //Tab Loại đồ uống

        public void HienThi_LoaiDoUong()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_Dulieu("Select DCID as[Mã loại],DCname as [Tên loại] from DrinkCategory ");
            Grid_Loai.DataSource = dta;
            HienThi_DL_LoaiDoUong();
        }
        public void HienThi_DL_LoaiDoUong()
        {
            txtIDDM.DataBindings.Clear();
            txtIDDM.DataBindings.Add("Text", Grid_Loai.DataSource, "Mã loại");

            txtTenDM.DataBindings.Clear();
            txtTenDM.DataBindings.Add("Text", Grid_Loai.DataSource, "Tên loại");
        }

        private void btnTaoDM_Click(object sender, EventArgs e)
        {
            txtIDDM.Text = "";
            txtTenDM.Text = "";

        }

        private void btnThemDM_Click(object sender, EventArgs e)
        {
            string sql = "Insert into DrinkCategory values(N'" + txtTenDM.Text + "')";
            kn.Execute(sql);
            HienThi_LoaiDoUong();
        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            kn.Execute("Delete DrinkCategory where DCID= " + txtIDDM.Text);
            HienThi_LoaiDoUong();
        }

        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            string sql = "Update DrinkCategory set DCName='" + txtTenDM.Text + "' where DCID= '" + txtIDDM.Text + "'";
            kn.Execute(sql);
            HienThi_LoaiDoUong();

        }

        private void btnTimDM_Click(object sender, EventArgs e)
        {
            string sql = "Select DCID as[Mã loại],DCName as[Tên loại] from Drinkcategory where DCname like N'%" + txtTimDM.Text + "%'";
            DataTable dta = kn.Lay_Dulieu(sql);
            Grid_Loai.DataSource = dta;


        }

        public void HienThi_Ban()
        {
            DataTable dta = kn.Lay_Dulieu("Select TbID as[Mã bàn],TbName as[Tên bàn],status as[Trạng thái] from TableDrink ");
            Grid_Ban.DataSource = dta;

            HienThi_DL_Ban();
        }
        public void Hienthi_TT_Ban()
        {
            cbTrangthai.Items.Add("Trống");
            cbTrangthai.Items.Add("Có người");
        }
        public void HienThi_DL_Ban()
        {
            txtIDBan.DataBindings.Clear();
            txtIDBan.DataBindings.Add("Text", Grid_Ban.DataSource, "Mã bàn");

            txtTenban.DataBindings.Clear();
            txtTenban.DataBindings.Add("Text", Grid_Ban.DataSource, "Tên bàn");

            cbTrangthai.DataBindings.Clear();
            cbTrangthai.DataBindings.Add("Text", Grid_Ban.DataSource, "Trạng thái");


        }

        private void btnTaoBan_Click(object sender, EventArgs e)
        {
            txtIDBan.Text = "";
            txtTenban.Text = "";
            
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            string sql = "Insert into TableDrink values('" + txtTenban.Text + "',N'Trống')";
            kn.Execute(sql);
            HienThi_Ban();
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            kn.Execute("Delete TableDrink where TbID=" + txtIDBan.Text );
            HienThi_Ban();

        }

        private void btnSuaBan_Click(object sender, EventArgs e)
        {
            string sql = "Update TableDrink set TbName='" + txtTenban.Text + "',status= '" + cbTrangthai.Text + "' where TbID= '" + txtIDBan.Text + "'";
            kn.Execute(sql);
            HienThi_Ban();

        }

        private void btnTimBan_Click(object sender, EventArgs e)
        {
            string sql = "Select TbID as[Mã bàn],TbName as[Tên bàn],status as[Trạng thái] from TableDrink where TbName= '"+txtTimban.Text+"'";
            DataTable dta = kn.Lay_Dulieu(sql);
            Grid_Ban.DataSource = dta;

        }
      
        
        public void HienThi_TK()
        {
            DataTable dta = kn.Lay_Dulieu("Select Username as[Tên ĐN],DisplayName as[Tên HT],type as[Loại TK] from Account where type=0");
            Grid_TK.DataSource = dta;
            HienThi_DL_TK();
        }
        public void HienThi_DL_TK()
        {
            txtDN.DataBindings.Clear();
            txtDN.DataBindings.Add("Text", Grid_TK.DataSource, "Tên ĐN");

            txtTenHienThi.DataBindings.Clear();
            txtTenHienThi.DataBindings.Add("Text", Grid_TK.DataSource, "Tên HT");

            
        }

        private void btnTaoTk_Click(object sender, EventArgs e)
        {
            txtDN.Text = "";
            txtTenHienThi.Text = "";
        }

        private void btnThemTk_Click(object sender, EventArgs e)
        {
            string sql = "Insert into Account values(N'" + txtDN.Text + "',N'" + txtTenHienThi.Text + "', N'1',0)";
            kn.Execute(sql);
            HienThi_TK();

        }

        private void btnXoaTk_Click(object sender, EventArgs e)
        {
            kn.Execute("Delete Account where type=0 and Username='" + txtDN.Text + "'");
            HienThi_TK();
        }

        private void btnTim_TK_Click(object sender, EventArgs e)
        {
            string sql = "Select Username as [Tên ĐN],Displayname as[Tên HT],Type as[Loại TK] from Account where DisplayName like '%" + txtTimTK.Text+"%'and Type=0";
            DataTable dta = kn.Lay_Dulieu(sql);
            Grid_TK.DataSource = dta;
        }

        private void btnDatlai_Click(object sender, EventArgs e)
        {
            string sql = "Update account set password=N'1' where Username='" + txtDN.Text + "'";
            DialogResult thongbao;
            thongbao = MessageBox.Show("Xác nhận đặt lại mật khẩu .", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thongbao == DialogResult.Yes)
            {
                kn.Execute(sql);
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }    
        }

        private void btn_HienRP1_Click(object sender, EventArgs e)
        {
            BaoCao_Hang BC = new BaoCao_Hang();
            BC.NgayBatDau = date_NgayBatdau1.Value;
            BC.NgayKetThuc = date_NgayKetthuc1.Value;
            BC.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaoCao_Thu BC = new BaoCao_Thu();
            BC.Batdau = date_NgayBatdau2.Value;
            BC.Ketthuc = date_NgayKetthuc2.Value;
            BC.Show();
            
        }
    }

}

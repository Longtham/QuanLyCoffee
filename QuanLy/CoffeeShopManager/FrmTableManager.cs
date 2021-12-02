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
    public partial class FrmTableManager : Form
    {
        Ketnoi kn = new Ketnoi();
        private Account account;
        public Account Account 
        {
            get { return account; }
            set { account = value; }
        }
        
        public FrmTableManager(Account acc)
        {
            InitializeComponent();
            this.account = acc;
            DN_Voi_TK(account.Type);
        }

        private void FrmTableManager_Load(object sender, EventArgs e)
        {
            Load_Table();
            LoadNhom();
            Load_CbTable(cbChuyenban);
        }

        public void DN_Voi_TK(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" + account.DisPlayName + ")";


        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void flpTable_Paint(object sender, PaintEventArgs e)

        {

        }


        // Load danh sách các bàn


        //Tạo 2 biến tĩnh là chiều dài, chiều rộng button
        public static int CD = 90;
        public static int CR = 90;

        public void Load_Table()
        {
            flpTable.Controls.Clear();
            DataTable dta = kn.Lay_Dulieu("Select * from TableDrink");
            List<Table1> TableList = new List<Table1>();  // Tạo 1 danh sách chứa các đối tượng được định nghĩa bởi lớp Table1
            foreach (DataRow i in dta.Rows) //Đưa dữ liệu từ CSDL vào danh sách
            {
                Table1 table = new Table1(i);
                TableList.Add(table);
            }
            foreach (Table1 item in TableList) //Hiển thị các button
            {
                Button btn = new Button() { Width = CR, Height = CD };
                btn.Text = item.Name+"\n"+item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch(item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.LightCyan;
                        break;
                    default:
                        btn.BackColor = Color.Orange;
                        break;

                }
                flpTable.Controls.Add(btn);
            }
        }



        //Hiển thị hóa đơn cho bàn

        public void HienThiHD(int ID)
        {
            lsvBill.Items.Clear();
            List<Menu1> listBillInfor = new List<Menu1>();
            String sql1 = "select Drink.Drinkname,BillInfor.count,Drink.Price,Drink.Price*BillInfor.count as totalPrice " +
                "from Drink, BillInfor, Bill where BillInfor.BillID = Bill.BillID " +
                "and BillInfor.DrinkID = Drink.DrinkID and Bill.status=0 and bill.TbID =" + ID;
            DataTable dta = kn.Lay_Dulieu(sql1);
            int Tongtien=0 ; // Biến tổng tiền sẽ được sử dụng cho chức năng thanh toán
            foreach(DataRow item in dta.Rows)
            {
                Menu1 menu = new Menu1(item);
                listBillInfor.Add(menu);

            }
            foreach(Menu1 item in listBillInfor)
            {
                ListViewItem lvItem = new ListViewItem(item.DrinkName.ToString());
                
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());
                Tongtien +=  item.TotalPrice;
                lsvBill.Items.Add(lvItem);

            }
            txtTongtien.Text = Tongtien.ToString();// Hiển thị tổng tiền cho txt.Tongtien
            

        }
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table1).ID;
            lsvBill.Tag = (sender as Button).Tag;
            HienThiHD(tableID);
        }
        
        

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //Hiển thị danh sách đồ uống theo nhóm
        //Tải lên danh sách nhóm đồ uống
        void LoadNhom() // Hàm được gọi trong FrmTableManager_Load
        {
            List<Category> listCategory = new List<Category>();
            DataTable dta = kn.Lay_Dulieu("Select * from DrinkCategory");
            foreach(DataRow item in dta.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        //Tải danh sách đồ uống theo nhóm
        void LoadLoai(int iD)
        {
            List<Drink> listDrink = new List<Drink>();
            DataTable dta = kn.Lay_Dulieu("Select * from  Drink where DCID="+iD);
            foreach(DataRow item in dta.Rows)
            {
                Drink drink = new Drink(item);
                listDrink.Add(drink);
            }
            cbDrink.DataSource = listDrink;
            cbDrink.DisplayMember = "Name";
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadLoai(id);
        }

        //Hàm lấy Bill ID theo Table ID
        public int HoaDonChuaTraTheoBan(int id)
        {
            DataTable dta = kn.Lay_Dulieu("Select * from Bill where TbID=" + id + "and status=0");
            if (dta.Rows.Count > 0)
            {
                Bill bill = new Bill(dta.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        //Thêm món cho hóa đơn

        public void ThemBill(int tbID)
        {
            string sql = "Insert into Bill(DateCheckIn,DateCheckOut,TbID,status,discount) values (Getdate(),NULL,"+ tbID+ ",0,0)";
            kn.Execute(sql);
        }
        public void ThemBillIf(int billID,int drinkID,int count)
        {
            
            kn.ExecuteNonQuery("ThemThongTinHD @billID , @drinkID , @count", new object[] { billID,drinkID,count});

        }

        public int LayBillID() //Hàm này sẽ trả về Id cao nhất đang có trong danh sách
        {
             
            try
            {
                return (int) kn.ExecuteScalar("Select max(BillID) from Bill");
            }
            catch
            {
                return 1;
            }
       
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Table1 table = lsvBill.Tag as Table1;
            int billID = HoaDonChuaTraTheoBan(table.ID);
            int drinkID = (cbDrink.SelectedItem as Drink).ID;
            int count = (int)nmDrinkCount.Value;
            if(billID==-1) // Bill chưa tồn tại
            {
                ThemBill(table.ID);
                ThemBillIf(LayBillID(), drinkID, count); 
                
            }
            else //Bill đã tồn tại
            {
                ThemBillIf(billID, drinkID, count);
            }
            HienThiHD(table.ID);
            Load_Table();//Cập nhật lại bàn
        }

     



        //Thanh toán hóa đơn
        public void CheckOut(int id,int discount,float Total)
        {
            string sql = "Update Bill  set dateCheckout=Getdate(), status = 1,discount= "+discount+" , " +
                "Total= "+Total+" where BillId= " + id;
            kn.Execute(sql);

        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            Table1 table = lsvBill.Tag as Table1;
            int BillID = HoaDonChuaTraTheoBan(table.ID);
            int dis = (int)nmGiamgia.Value;
            double TongTien =Convert.ToDouble(txtTongtien.Text.Split(',')[0]);
            double Tientra = TongTien - TongTien * dis / 100;
            if(BillID!=-1)
            {
                if(MessageBox.Show(string.Format("Xác nhận thanh toán hóa đơn cho bàn {0}\n Tổng tiền=Tổng-Tổng*Giảm giá/100={1}-{1}*{2}/100={3}\n=>Số tiền phải trả là:{3}",table.Name,TongTien,dis,Tientra)," Thông báo ", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    CheckOut(BillID,dis,(float)Tientra);
                    HienThiHD(table.ID);
                } 
                    
            }
            Load_Table();
        }


        // Chuyển bàn
        public void Chuyenban(int IDcu,int IDmoi)
        {
             
            kn.ExecuteQuery("Chuyenban @TbID_Cu , @TbID_Moi",new object[]{IDcu,IDmoi});

        }

        void Load_CbTable(ComboBox cb)
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_Dulieu("Select * from TableDrink");
            List<Table1> TableList = new List<Table1>();
            foreach (DataRow item in dta.Rows)
            {
                Table1 table = new Table1(item);
                TableList.Add(table);
            }
            cb.DataSource = TableList;
            cb.DisplayMember = "Name";  
            
        

        } 
        private void btnChuyenban_Click(object sender, EventArgs e)
        {
            int IDcu = (lsvBill.Tag as Table1).ID;
            int IDmoi = (cbChuyenban.SelectedItem as Table1).ID;

            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển từ bàn {0} sang bàn {1} không", 
                (lsvBill.Tag as Table1).Name, (cbChuyenban.SelectedItem as Table1).Name), 
                "Thông báo", MessageBoxButtons.OKCancel)==System.Windows.Forms.DialogResult.OK)
            {
                Chuyenban(IDcu, IDmoi);
                Load_Table();
            }
        }

        private void cbChuyenban_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        
        public DataTable HDTheongay(DateTime Tungay,DateTime Denngay)
        {
           return kn.ExecuteQuery("Exec HDTheoNgay @TuNgay , @DenNgay ",new object[]{Tungay,Denngay });
        }


        //Tạo form admin
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAdmin frm = new FrmAdmin();
            frm.ShowDialog();
            this.Show();

        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
               /* this.Hide();
            FmUser frm=new FmUser();
            frm.ShowDialog();
            this.Show(); */
            this.Hide();
            FrmThongtin frm = new FrmThongtin(account);
            frm.ShowDialog();
            

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }

}
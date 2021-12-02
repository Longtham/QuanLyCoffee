create database QLNhahang
use QLNhahang
Go


--Bảng Nhân viên
create table Account
(
	Username nvarchar(100) Primary Key Not Null,
	Displayname nvarchar(100)Not Null,
	PassWord nvarchar(100)Not Null,
	Type int Not Null,
)
Go

--Bảng bàn. Em để thêm Drink đằng sau vì sợ Table sẽ trùng từ khóa của code
create table TableDrink 

(
	TbID int identity Primary Key,
	TbName Nvarchar(100) Not Null,
	status Nvarchar(100)Not Null, 

)
Go


select * from TableDrink

create table DrinkCategory
(
	DCID int identity Primary key not null,
	DCName nvarchar(100) not null,
)
Go


--Bảng đồ uống
create table Drink
(
	DrinkID int identity primary key,
	Drinkname nvarchar(100) not null,
	DCID int not null,
	Price int not null,
	foreign key (DCID) references DrinkCategory(DCID),
)
Go

Select DrinkCategory.DCName from Drink,DrinkCategory where Drink.DCID=DrinkCategory.DCID and Drink.DCID=1 
Select * from DrinkCategory


--Tạo bảng hóa đơn
create table Bill
(
	BillID int identity primary key ,
	DateCheckIn Date not null,
	DateCheckOut Date ,
	TbID int not null,
	status int not null,
	Foreign Key (TbID) References TableDrink(TbID),

)
Go

--Tạo bảng TT Hóa đơn
create table BillInfor
(
	BIID int identity primary key,
	BillID int not null,
	DrinkID int,
	Foreign Key (BillID) references Bill(BillID) ,
	Foreign Key (DrinkID) references Drink(DrinkID) ,
	count int,
)
Go

--Thêm giảm giá cho ill
Alter table Bill Add discount int
--Thêm tổng tiền cho Bill
Alter table Bill add Total float




--Thêm account

insert into Account values ('admin',N'Thẩm Thành Long','123456','1')
insert into Account values ('Staff2',N'Minh Hoàng','123456','0')
insert into Account values ('Staff3',N'Hương','123456','0')
insert into Account values ('Staff4',N'Vỹ','123456','0')
insert into Account values ('Staff5',N'May','123456','0')


select * from Account



---Thêm bàn.Procedure này tạo tự động 20 bàn
Declare @i int =1
while @i<=20
Begin
	Insert into TableDrink Values(N'Bàn '+Cast(@i as nvarchar(100)),N'Trống')
	Set @i=@i+1
end




select * from TableDrink

---Thêm nhóm
Insert into DrinkCategory values(N'Coffee')
Insert into DrinkCategory values(N'Juice')
Insert into DrinkCategory values(N'Milk Tea')

Select* from DrinkCategory

---Thêm các món
Insert into Drink values (N'Cà phê đen',1,20000)
Insert into Drink values (N'Đen đá',1,20000)
Insert into Drink values (N'Cà phê sữa',1,25000)
Insert into Drink values (N'Sữa đá',1,25000)
Insert into Drink values (N'Siêu cà phê',1,250000)
Insert into Drink values (N'Nước cam',2,15000)
Insert into Drink values (N'Nước táo',2,15000)
Insert into Drink values (N'Dưa hấu',2,20000)
Insert into Drink values (N'TS Socola',3,80000)
Insert into Drink values (N'TS Caramen',3,80000)
Insert into Drink values (N'TS Matcha',3,80000)
Insert into Drink values (N'TS TC đen',3,80000)
Select * from  Drink



select * from Account




 --view đồ uống dùng trong form Admin- Đồ uống
Create view v_DoUong
as
Select Drink.DrinkID,Drink.DCID,Drink.Drinkname,DrinkCategory.DCName,Drink.Price from Drink,DrinkCategory
where Drink.DCID=DrinkCategory.DCID


-- View thống kê đồ uống
alter view v_ThongKeDoUong
as
Select  Drink.DrinkName as [name],Drink.DrinkID as [ID],Sum(count) as [Total] from BillInfor,Drink
where   BillInfor.DrinkID=Drink.DrinkID
Group by Drink.Drinkname,Drink.DrinkID

Select * from v_Thongkedouong




-- View thống kê doanh thu
Create view TongTien
as
Select DateCheckOut,Sum(Total) as[Tong] from Bill  group by DateCheckOut

select * from Tongtien









 --- Đoạn lệnh sau để thêm dl bảng thông tin hóa đơn

Create proc ThemThongTinHD
 @BillID int ,@DrinkID int, @count int
 as
 Begin
 Declare @KT int 
-- Kiểm tra hóa đơn đã tồn tại chưa
 Declare @Dem int =1
		Select @KT=BIID,@Dem= count from BillInfor as B
		where BillID=@BillID and DrinkID=@DrinkID
		if(@KT>0)
			begin
				declare @Dem2 int =@Dem+@count
				if(@dem2>0)
					update BillInfor set count=@Dem+@count where DrinkID = @DrinkID
				else
					delete BillInfor where BillID=@BillID and  DrinkID=@DrinkID
			end
		else 
			Begin
				insert BillInfor values (@BillID,@DrinkID,@count)
			End
End




--Tạo trigger cập nhật bàn theo TTHĐ

Create trigger Trg_CapNhatTheoTTHD 
on BillInfor for insert,update
as
	Begin
		Declare @BillID int
		Select @BillID = BillID from inserted
		Declare @TbID int
		Select @TbID = TbID from Bill where BillID=@BillID and status = 0
		Declare @count int
		Select @count=Count(*) from BillInfor where  BillID=@BillID    --- Đếm số BillINfor tương ứng cho Bill của bàn
		if(@count >0)     ---Nếu đã có BillInfor thì update bàn thành có người và ngược lại
			Update TableDrink set status=N'Có người' where TbID=@TbID
		else 
			Update TableDrink set status=N'Trống' where TbID=@TbID
	End
Go



Create Trigger Capnhatban
on TableDrink for Update
as
	Begin
		Declare @TbID int
		Declare @Status Nvarchar(100)
		Select @TbID = TbID , @Status= status from Inserted
		Declare @BillID int
		Select @BillID =BillID from Bill where TbID=@TbID and status=0
		Declare @countBI int
		Select @countBI = Count(*) from BillInfor where BillID=@BillID
		If(@countBI>0 and @Status <> N'Có người') -- Nếu có HĐ mà bàn ở trạng thái Trống thì chuyển thành có người
			Update TableDrink set status=N'Có người' where TbID=@TbID
		Else if(@countBI <=0 and @Status <> N'Trống')-- Và ngược lại
			Update TableDrink set status=N'Trống' where TbID=@TbID

	End
Go


Create trigger Trg_CapNhatTheoHD
on Bill for update
as
	Begin
		Declare @BillID int
		Select @BillID= BillID from inserted
		Declare @TbID int
		Select @TbID = TbID from Bill where BillID=@BillID 
		Declare @count int 
		Select @count = COUNT(*) from Bill Where TbID=@TbID and status=0
		if (@count = 0) --Nếu Bàn không có Bill thì chuyển trạng thái là trống
			Update TableDrink set status=N'Trống' where TbID = @TbID
	End
Go
D

--Procedure chuyển bàn

Create proc Chuyenban
@TbID_Cu int, @TbID_Moi int
as
	Begin
		Declare @ID_HD_cu int
		Declare @ID_HD_moi int
		Declare @HDcu int = 1
		Declare @HDmoi int =1
		Select @ID_HD_cu = BillID from Bill where TbID=@TbID_Cu and status = 0 --ID hóa đơn cũ lấy từ bảng hóa đơn có ID bàn cũ = biến @TbID_Cu 
		Select @ID_HD_moi = BillID from Bill where TbID=@TbID_Moi and status = 0 -----------mới-----------------------------mới--------@TbID_Moi
		if(@ID_HD_cu is Null) --Nếu bàn cũ chưa có hóa đơn thì insert dữ liệu cho bảng hóa đơn
			Begin
				insert into Bill values(Getdate(),NULL,@TbID_Cu,0,0,0)
				Select @ID_HD_cu= max(BillID) from Bill where  TbID = @TbID_Cu and status = 0
			End
		Select @HDcu=Count(*) from BillInfor where BillID=@ID_HD_cu --Gán biến @HDcu bằng tổng số BillInfor với điều kiện Thông tin hóa đơn = biến @ID_HD_cu

		if(@ID_HD_moi is Null)
			Begin
				insert into Bill values(Getdate(),NULL,@TbID_Moi,0,0,0)
				Select @ID_HD_moi= max(BillID) from Bill where TbID = @TbID_Moi and status = 0
			End
		--- Bước chuyển:
		Select @HDmoi=Count(*) from BillInfor where BillID=@ID_HD_moi
		Select BIID into BIID_Table from BillInfor where BillID=@ID_HD_moi
		Update BillInfor set BillID=@ID_HD_moi where BillID=@ID_HD_cu
		Update BillInfor set BillID=@ID_HD_cu where BIID in (Select * from BIID_Table)
		Drop table BIID_Table
		if(@HDcu =0)
			Update TableDrink set status=N'Trống' where TbID=@TbID_Moi
		if(@HDmoi = 0)
			Update TableDrink set status=N'Trống' where TbID=@TbID_Cu

	End
Go



--Hiển thị hóa đơn theo ngày
Create Proc HDTheoNgay
@TuNgay date,@DenNgay date
as
Begin
	Select TbName as[Tên bàn],Total as[Tổng tiền],DateCheckIn as[Ngày vào],DateCheckOut as[Ngày ra],discount as[Giảm giá]
	from Bill ,TableDrink 
	where DateCheckIn >=@TuNgay and DateCheckOut <=@DenNgay 
		and Bill.status=1 and Bill.TbID=TableDrink.TbID
End








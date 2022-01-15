
CREATE DATABASE AdsProject;
go
use AdsProject
  create TABLE Categories(
    Id int PRIMARY KEY IDENTITY(1, 1),
    Category nvarchar(100) not null
  ) 
  create table Users(
    Id int PRIMARY KEY IDENTITY(1, 1),
    [Role] int not null,
    [Login] nvarchar(50) not null unique,
    [Password] nvarchar(max) not null,
  ) 
  create table Contacts(
    Id int PRIMARY KEY FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    Name nvarchar(100) not null,	
	Mail nvarchar(30) not null,
	Phone nvarchar(15) not null
  ) 
  
  create table Ads(
    Id int PRIMARY KEY IDENTITY(1, 1),
    Name nvarchar(100) not null,
    Title nvarchar(max) not null,
    DateCreation datetime not null,
	[Image] nvarchar(max) not null,
    Category int NOT NULL FOREIGN KEY REFERENCES Categories(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    Contact int NOT NULL FOREIGN KEY REFERENCES Contacts(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    Address nvarchar(max) not null,
    [Type] int NOT NULL,
    [State] int NOT NULL
  )
  
go
--Ads
  create proc GetAds
	@Name nvarchar(100) = null,
	@Category int = null,
	@Type int = null,
	@State int = null
  as
	select Ads.Id as 'Id'
      ,Ads.Name as 'Name'
      ,[Title]
      ,[DateCreation]
      ,[Category]
	  ,[Image]
      ,Contacts.Name as 'UserName'
	  ,Contacts.Id as 'UserId'
	  ,Contacts.Phone as 'Phone'
	  ,Contacts.Mail as 'Mail'
      ,[Address]
      ,[Type]
      ,[State]
	   from Ads 
	   inner join Contacts on Ads.Contact = Contacts.Id
	   where
		(ads.Name like '%'+ISNULL(@Name,'')+'%') and
		(Category  = @Category or @Category is null)  and
		([Type]  = @Type or @Type is null) and
		([State]  = @State or @State is null) 
go
  create proc DeleteAd
	@Id int
  as
  delete from Ads
  where Id = @Id
go
  create proc ChangeAd
    @Id int,
    @Name nvarchar(100),
	@Category int,
	@Type int,	
	@State int,
	@Title nvarchar(max),
	@DateCreation datetime,
	@Address nvarchar(max),
	@Image nvarchar(max)
  as
    UPDATE Ads
		set Name = @Name,
		Address = @Address,
		Title = @Title,
		[DateCreation] = @DateCreation,
		Category = @Category,
		[Image] = @Image,
		Type = @Type,
		State = @State
	where Id = @Id
go
  create proc AddAd
	@Name nvarchar(100),
	@Category int,
	@Type int,
	@State int,
	@Title nvarchar(100),
	@DateCreation datetime,
	@Contact int,
	@Address nvarchar(max),
	@Image nvarchar(max)
  as
    insert into [Ads]
		(
		Name,
		Title,
		DateCreation,
		Category,
		Contact,
		Address,
		Type,
		State)
    values
		(
		@Name,
		@Category,
		@Type,
		@State,
		@Title,
		@DateCreation,
		@Contact,
		@Address
		)
go
  create proc CheckAd
  @Id int,
  @UserId int
  as
  select count(*) from Ads where 
					 Id = @Id and Contact = @UserId
go
  create proc GetAd
  @Id int
  as
  select Ads.Id as 'Id'
      ,Ads.Name as 'Name'
      ,[Title]
      ,[DateCreation]
      ,[Category]
	  ,[Image]
      ,Contacts.Name as 'UserName'
	  ,Contacts.Id as 'UserId'
	  ,Contacts.Phone as 'Phone'
	  ,Contacts.Mail as 'Mail'
      ,[Address] as 'Address'
      ,[Type]
      ,[State] from Ads inner join Contacts on ads.Contact = Contacts.Id
	   where Ads.Id = @Id
go
--Category
  create proc GetCategories
  as
  select Id, Category as 'Name' from Categories
go
  create proc AddCategory
  @Name nvarchar(100)
  as
  insert into Categories(
  Category
  )
  values
  (
  @Name
  )
go
  create proc DeleteCategory
  @Id int
  as
  delete from Categories
  where Id = @Id
go
  create proc ChangeCategory
    @Id int,
    @Name Nvarchar(100)
  as
  update Categories
  set Category = @Name
  where Id = @Id
go
--Contacts and Users
  create proc ChangeContact
  @Id int,
  @Name nvarchar(100),
  @Role int,
  @Login nvarchar(30),
  @Phone nvarchar(30),
  @Mail nvarchar(30)
  as
  update Contacts
  set Name = @Name,
  Phone = @Phone,
  Mail = @Mail
  where Id = @Id
  update Users
  set [Role] = @Role,
  [Login] = @Login
  where Id = @Id
go
  create proc ChangePassword
  @IdUser int,
  @Password nvarchar(max)
  as
  update Users
  set Password = @Password
  where Id = @IdUser
go

  create proc AddUser
  @Name NVARCHAR(100),
@Login NVARCHAR(50),
@Password NVARCHAR(max),
@Role int,
@Mail NVARCHAR(30),
@Phone NVARCHAR(15)
as
insert into Users 
		   ([Login]
		   ,[Password]
		   ,[Role])
	 values
		   (@Login
		   ,@Password
		   ,@Role)

INSERT INTO [dbo].[Contacts]
           ([Name]
		   ,[Id]
		   ,[Phone]
		   ,[Mail])
     VALUES
           (@Name,
		   SCOPE_IDENTITY(),
		   @Phone,
		   @Mail)

go
  create PROC [DeleteUser]
  @Id int
  as
  DELETE FROM [dbo].[Users]
      WHERE Users.Id = @Id
go
  create Proc GetUser
  @Id int
  as 
  select Contacts.Name as 'Name', Contacts.Id as 'Id' from Contacts
  where Contacts.Id = @Id
go
create Proc LoginUser
  @Login nvarchar(50),
  @Password nvarchar(max)
  as 
  select Contacts.Name as 'UserName', 
	  Contacts.Id as 'Id', 
	  Contacts.Mail as 'Mail', 
	  Contacts.Phone as 'Phone',
	  Users.Login as 'Login'
  from Contacts inner join Users on Users.Id = Contacts.Id
  where Users.Login = @Login and Users.Password = @Password
go
  create Proc GetUsers
  as 
  select Contacts.Name as 'Name', Contacts.Id as 'Id' from Contacts
go
  create proc CheckUser
  @Login nvarchar(50),
  @Password nvarchar(max)
  as
  select Count(*) from Users
  where [Login]  = @Login and [Password] = @Password
--Phone  
go
  create proc ChangePhone
  @Id int,
  @Phone nvarchar(15)
  as
  Update Contacts
  set Phone = @Phone
  where Id = @Id

go
  create proc ChangeMail
  @Id int,
  @Mail nvarchar(30)
  as
  Update Contact
  set Mail = @Mail
  where Id = @Id

  go

INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Животные')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Учеба')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Развлечение')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Технологии')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Дом')
exec AddUser "Радион Миханский","Radio@mechanik@army.by","PcjLIIxBFgGIWQeEoS91/xmkio5UMFvBIXYYIOJBuGc=",2, "Radio@yandex.by","+375216826575"
INSERT INTO [dbo].[Ads]
           ([Name]
           ,[Title]
           ,[DateCreation]
           ,[Category]
           ,[Contact]
           ,[Address]
           ,[Type]
           ,[State]
		   ,[Image])
     VALUES
           ('Котики'
           ,'много котиков'
           ,'12.03.2020'
           ,1
           ,1
           ,'Везде'
           ,1
           ,1
		   ,'Картинка')
GO


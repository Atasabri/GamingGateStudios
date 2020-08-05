create table Admins
(
ID int primary key identity(1,1) not null,
Username nvarchar(50) not null,
Admin_Type int not null
)

Create table Subscribers
(
ID int primary key identity(1,1) not null,
Email nvarchar(50) not null
)

Create table Contacts
(
ID int primary key identity(1,1) not null,
First_Name nvarchar(50) not null,
Last_Name nvarchar(50) not null,
Email nvarchar(50) not null,
Message nvarchar(max) not null
)

Create Table Users
(
ID int primary key identity(1,1) not null,
Name nvarchar(50) not null,
Email nvarchar(100) not null,
Password nvarchar(50) not null,
Phone int not null
)

Create Table Portfolio
(
ID int primary key identity(1,1) not null,
Name nvarchar(50) null
)

Create table Games
(
ID int primary key identity(1,1) not null,
Name nvarchar(100) not null,
Price float not null,
Description nvarchar(max) not null,
Android_URL nvarchar(max)  null,
Itunes_URL nvarchar(max)  null,
WindowsPhone_URL nvarchar(max) null,
Facebook_URL nvarchar(max) null,
KindleFire_URL nvarchar(max) null,
EB_URL nvarchar(max) null
)
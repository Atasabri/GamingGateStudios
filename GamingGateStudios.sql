
create proc [dbo].[ADClick]
@ID int
as
begin  
 update ADS set Click=Click+1 where ID=@ID
end
GO
/****** Object:  StoredProcedure [dbo].[ADWatch]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ADWatch]
@ID int
as
begin
 update ADS set CPM=CPM+1 where ID=@ID
end
GO
/****** Object:  StoredProcedure [dbo].[GameWatch]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GameWatch]
@ID int
AS
begin
update Games set watch=watch+1 where ID=@ID
end

GO
/****** Object:  StoredProcedure [dbo].[Rest_Users]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Rest_Users]
as
begin
Update Users set Money_Token=0 where id>=1
end
GO
/****** Object:  StoredProcedure [test].[GamerActive]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [test].[GamerActive]
@id int 
as 
begin
   update Gamers set Active=1 where id=@id
end
GO
/****** Object:  StoredProcedure [test].[GamerNotActive]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [test].[GamerNotActive]
@id int 
as
begin
WHILE 1=1
BEGIN
   WAITFOR DELAY '00:05:00' -- Wait 5 minutes

   update Gamers set Active=0 where id=@id
     break
END
end
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Admin_Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ADS]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Enable] [bit] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[CPM] [int] NOT NULL,
	[Link] [nvarchar](max) NULL,
	[Click] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ads_Clients]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ads_Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Target_CPM] [int] NOT NULL,
	[Price] [float] NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Agency_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Agencies]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agencies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Target_CPM] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Channels]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Deal_Gamers]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deal_Gamers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Gamer_ID] [int] NOT NULL,
	[Deal_ID] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Deal_Member]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deal_Member](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Deal_ID] [int] NOT NULL,
	[Member_ID] [int] NOT NULL,
	[GGB] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Deals]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name-AR] [nvarchar](100) NOT NULL,
	[Game_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gamers]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gamers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Member] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [int] NULL,
	[Points] [int] NULL,
	[Tries] [int] NULL,
	[Mony_Send] [float] NULL,
	[Money_Token] [float] NULL,
	[Active] [bit] NOT NULL,
	[Game_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Games]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name-AR] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Description-AR] [nvarchar](max) NOT NULL,
	[Android_URL] [nvarchar](100) NULL,
	[Ituens_URL] [nvarchar](100) NULL,
	[Windows_URL] [nvarchar](100) NULL,
	[Facebook_URL] [nvarchar](100) NULL,
	[KindleFire_URL] [nvarchar](100) NULL,
	[EB_URL] [nvarchar](100) NULL,
	[Game_Link] [nvarchar](100) NULL,
	[Trailer] [nvarchar](100) NULL,
	[Watch] [int] NOT NULL,
	[Start_Tries] [int] NOT NULL,
	[Start_Points] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberShip]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberShip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NULL,
	[Limit_Token_Money] [float] NULL,
	[color] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Operators]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[User_Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Portfolio]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prizes]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prizes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Name-AR] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[Third_Partner] [int] NULL,
	[Price] [float] NULL,
	[Points] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Splashes]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Splashes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Channel_ID] [int] NOT NULL,
	[Enable] [bit] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Game_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Third_Partner]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Third_Partner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[User_Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [int] NULL,
	[Face_ID] [nvarchar](100) NULL,
	[Member_ID] [int] NOT NULL,
	[Money_Token] [float] NULL,
	[GGB] [int] NULL,
	[Active] [bit] NOT NULL,
	[Pay] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Winners]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Winners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Prize_ID] [int] NOT NULL,
	[Gamer_ID] [int] NOT NULL,
	[Received] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Admins]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Admins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Categories]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Contacts]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [test].[GGBDeals]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[GGBDeals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Price] [float] NOT NULL,
	[GGB] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[News]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[News](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Head] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Date] [date] NOT NULL,
	[watch] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [test].[Payment]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pay_Code] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Programs]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Programs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Subscribers]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Subscribers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Tags]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tag_Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Tags_Videos]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Tags_Videos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tag_ID] [int] NOT NULL,
	[Video_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Users]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Face_ID] [nvarchar](max) NULL,
	[Last_Date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [test].[Users_GGBDeals]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Users_GGBDeals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[GGBDeal_ID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Pay] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [test].[Videos]    Script Date: 11/19/2019 10:03:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[Videos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Watch] [int] NOT NULL,
	[Embed_URL] [nvarchar](max) NOT NULL,
	[Num_Rate] [int] NOT NULL,
	[All_Rate] [int] NOT NULL,
	[For_Users_Only] [bit] NOT NULL,
	[Cat_ID] [int] NOT NULL,
	[Program_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

GO
INSERT [dbo].[Admins] ([ID], [Username], [Password], [Admin_Type]) VALUES (1, N'Ata Sabri', N'01142229025', 1)
GO
INSERT [dbo].[Admins] ([ID], [Username], [Password], [Admin_Type]) VALUES (2, N'Ahmed', N'12343', 0)
GO
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[ADS] ON 

GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (1, N'test', 1, 1, 800, 500, 995, N'http://localhost:60084/Splashes/Edit/1', 2)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (2, N'Ata Sabri', 1, 1, 640, 426, 0, N'https://www.alnahar.com', 0)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (3, N'sds', 1, 1, 640, 426, 0, NULL, 6)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (4, N'ads 1', 2, 1, 640, 426, 4, NULL, 1)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (5, N'Orange ADS 1', 3, 1, 1600, 800, 8, NULL, 2)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (6, N'ads 1', 3, 1, 480, 640, 6, N'test.com', 3)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (7, N'ads 1', 1, 1, 500, 189, 0, NULL, 3)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (8, N'Ads 3', 3, 0, 728, 90, 38, N'https://www.alnahar.com', 1)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (9, N'ADS 4', 2, 1, 728, 90, 82, N'https://www.youtube.com/', 3)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (11, N'ADS 6', 3, 1, 728, 90, 106, N'https://www.alnahar.com', 1)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (12, N'ADDS 8', 2, 1, 728, 90, 79, N'https://www.alnahar.com', 3)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (13, N'ADS 7', 2, 1, 728, 90, 75, N'https://www.alnahar.com', 5)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (14, N'ADS 10', 3, 1, 728, 90, 90, N'https://www.alnahar.com', 2)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (15, N'ADS 11', 3, 1, 728, 90, 101, N'https://www.alnahar.com', 2)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (16, N'Ad1 Agency 1', 5, 1, 728, 90, 43, N'https://www.alnahar.com', 2)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (18, N'Ad1 Agency 2', 6, 1, 852, 864, 901, N'https://www.alnahar.com', 0)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (19, N'AD2 Agency2', 6, 1, 1366, 728, 0, N'https://www.alnahar.com', 0)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (20, N'AD2 Agency1', 8, 1, 728, 90, 25, N'https://www.alnahar.com', 0)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (21, N'AD3 AGENCY 1', 7, 1, 728, 90, 23, N'https://www.alnahar.com', 1)
GO
INSERT [dbo].[ADS] ([ID], [Name], [Client_ID], [Enable], [Width], [Height], [CPM], [Link], [Click]) VALUES (22, N'AD 4 AGENCY1', 7, 1, 728, 90, 31, N'http://localhost:60084/Splashes/Edit/1', 0)
GO
SET IDENTITY_INSERT [dbo].[ADS] OFF
GO
SET IDENTITY_INSERT [dbo].[Ads_Clients] ON 

GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (1, N'Etisalat', 1000, 12.9, N'Etisalat', N'123', NULL)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (2, N'vodafone', 1000, 14.8, N'Vodafone', N'123', NULL)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (3, N'Orange', 3000, 30, N'Orange', N'1234', NULL)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (5, N'Client 1 For Agency 1', 1000, 20, NULL, NULL, 1)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (6, N'Client 1 For Agency 2', 1000, 30.9, NULL, NULL, 2)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (7, N'Client 2 Agency 1', 2000, 50, NULL, NULL, 1)
GO
INSERT [dbo].[Ads_Clients] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password], [Agency_ID]) VALUES (8, N'client 3 agency 1', 4000, 60, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Ads_Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Agencies] ON 

GO
INSERT [dbo].[Agencies] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password]) VALUES (1, N'Agency 1', 1, 30.9, N'Ageny1', N'Agency1')
GO
INSERT [dbo].[Agencies] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password]) VALUES (2, N'Agency 2', 2, 40.9, N'agency2', N'agency2')
GO
INSERT [dbo].[Agencies] ([ID], [Name], [Target_CPM], [Price], [UserName], [Password]) VALUES (3, N'Agency 3', 3, 50.9, N'Agency3', N'agency3')
GO
SET IDENTITY_INSERT [dbo].[Agencies] OFF
GO
SET IDENTITY_INSERT [dbo].[Channels] ON 

GO
INSERT [dbo].[Channels] ([ID], [Name], [Username], [Password]) VALUES (2, N'CBC Sofra', N'CBC', N'123455')
GO
INSERT [dbo].[Channels] ([ID], [Name], [Username], [Password]) VALUES (3, N'MBC', N'MBC', N'12345')
GO
INSERT [dbo].[Channels] ([ID], [Name], [Username], [Password]) VALUES (4, N'el nahar', N'elnahar', N'123')
GO
INSERT [dbo].[Channels] ([ID], [Name], [Username], [Password]) VALUES (5, N'Channnel 1', N'ch', N'1')
GO
SET IDENTITY_INSERT [dbo].[Channels] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

GO
INSERT [dbo].[Contacts] ([ID], [First_Name], [Last_Name], [Email], [Message]) VALUES (1, N'Ata', N'Sabri', N'ataeldon@gmail.com', N'test')
GO
INSERT [dbo].[Contacts] ([ID], [First_Name], [Last_Name], [Email], [Message]) VALUES (2, N'Ata', N'Sabri', N'ataeldon@gmail.com', N'TEST MESSAGE')
GO
INSERT [dbo].[Contacts] ([ID], [First_Name], [Last_Name], [Email], [Message]) VALUES (11, N'Ata', N'Sabri', N'ataeldon@gmail.com', N'test')
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[Deal_Gamers] ON 

GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (6, 4, 1, N'ghgh')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (7, 4, 1, N'kl')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (8, 4, 1, N'lklk')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (9, 4, 1, N'hjhjkl')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (10, 4, 1, N'LLKKLLKK')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (11, 4, 1, N'asas')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (12, 8, 1, N'jijiji')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (13, 4, 1, N'tyty')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (14, 8, 2, N'kklliinniinn')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (15, 4, 4, N'ioio')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (16, 4, 4, N'hjkl')
GO
INSERT [dbo].[Deal_Gamers] ([ID], [Gamer_ID], [Deal_ID], [Code]) VALUES (17, 4, 2, N'ghghghghg')
GO
SET IDENTITY_INSERT [dbo].[Deal_Gamers] OFF
GO
SET IDENTITY_INSERT [dbo].[Deal_Member] ON 

GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (15, 1, 2, 33)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (16, 1, 3, 55)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (17, 1, 1, 44)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (18, 2, 1, 12)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (19, 2, 2, 10)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (20, 2, 3, 5)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (21, 4, 1, 12)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (22, 4, 2, 14)
GO
INSERT [dbo].[Deal_Member] ([ID], [Deal_ID], [Member_ID], [GGB]) VALUES (23, 4, 3, 88)
GO
SET IDENTITY_INSERT [dbo].[Deal_Member] OFF
GO
SET IDENTITY_INSERT [dbo].[Deals] ON 

GO
INSERT [dbo].[Deals] ([ID], [Name], [Name-AR], [Game_ID]) VALUES (1, N'Deal 1', N'ديل 1', 1)
GO
INSERT [dbo].[Deals] ([ID], [Name], [Name-AR], [Game_ID]) VALUES (2, N'Deal 2', N'الديل الثانى', 1)
GO
INSERT [dbo].[Deals] ([ID], [Name], [Name-AR], [Game_ID]) VALUES (4, N'Ata Sabri', N'اللعبة الاولى ', 1)
GO
SET IDENTITY_INSERT [dbo].[Deals] OFF
GO
SET IDENTITY_INSERT [dbo].[Gamers] ON 

GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (4, N'01142229025', N'Mohamed Adel', N'01142229025', N'A', N'Egypt', CAST(0x46250B00 AS Date), 1, 10, 10, 0, 0, 1, 1)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (5, N'01042229025', N'sabri hosny', N'77', N'A', N'USA', CAST(0x65220B00 AS Date), 1, 12, 2, 40, 30, 0, 2)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (6, N'01242229025', N'Ata Sabri', N'99', N'A', N'Egypt', CAST(0x1D1E0B00 AS Date), 1, 30, 20, 100.8, 30.7, 0, 3)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (7, N'01052229025', N'ahmed', N'123123', N'A', N'dubai', CAST(0xB5260B00 AS Date), 1, 20, 12, 0, 0, 1, 4)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (8, N'01232229025', N'mohsen mohamed', N'said', N'A', N'Egypt', CAST(0x5D220B00 AS Date), 0, 0, 20, 20, 0, 0, 1)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (9, N'01182229025', N'Noha', N'ataahmed', N'A', N'saudi arabia', CAST(0x8A1F0B00 AS Date), 0, 20, 11, 100.8, 0, 0, 2)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (10, N'01192229025', N'maha', N'atasabri', N'b', N'usa', CAST(0xF4160B00 AS Date), 0, 2, 1, 100.8, 20, 0, 3)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (14, N'01142229025', N'MoHamed Adel', N'01142229025', N'b', N'usa', CAST(0x46250B00 AS Date), 0, 20, 12, 30.9, 20.89, 0, 2)
GO
INSERT [dbo].[Gamers] ([ID], [Phone], [UserName], [Password], [Member], [Country], [DateOfBirth], [Gender], [Points], [Tries], [Mony_Send], [Money_Token], [Active], [Game_ID]) VALUES (15, N'01102229028', N'Sabri Ata', N'sabriataahmed', NULL, N'Egypt', CAST(0x501F0B00 AS Date), 1, 11, 20, NULL, NULL, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Gamers] OFF
GO
SET IDENTITY_INSERT [dbo].[Games] ON 

GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (1, N'Game 1', N'اللعبة الاولى', N'Description for game 1', N'كلام عن اللعبة الاولى', N'url', N'url', NULL, N'url', N'url', N'url', N'url', N'https://www.youtube.com/embed/yI9o0jQ6osY', 17, 20, 11)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (2, N'Game 2', N'اللعبة الثانية', N'description for firdt game', N'كلام عن اللعبة الثانية كلام كتير متير متي رمتءب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 1, 30, 22)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (3, N'Game 3', N'اللعبة الثالثة ', N'you must be near us', N'كلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 2, 10, 30)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (4, N'Game 4', N'اللعبة الرابعة', N'Our Video services help you reach your goals and turn by passers into clients..From Idea to script to screen..we master the art and craft of writing scripts for various media platforms.We host a group of creative graphic organizers and illustrators that will help bring your visions into visuals ,Creativity is something new and somehow valuable formed, Media Gate Studios generates, develops, and communicates new ideas, where a creative idea is understood as a basic element of thought that can be visually appealing throughout developed cycle, from innovation, to development, to actualization ,Media Gate Studios will add it''s magic from a raw idea to stage designing.', N'كلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 4, 20, 20)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (5, N'Game 5', N'اللعبة الخامسة ', N'Our Video services help you reach your goals and turn by passers into clients..From Idea to script to screen..we master the art and craft of writing scripts for various media platforms.We host a group of creative graphic organizers and illustrators that will help bring your visions into visuals ,Creativity is something new and somehow valuable formed, Media Gate Studios generates, develops, and communicates new ideas, where a creative idea is understood as a basic element of thought that can be visually appealing throughout developed cycle, from innovation, to development, to actualization ,Media Gate Studios will add it''s magic from a raw idea to stage designing.', N'كلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 4, 40, 40)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (6, N'Game 6', N'اللعبة السادسة', N'Media Gate Studios provide an extensive array of creative, production, and digital media services for clients with social media platforms and channels for our clients who are looking to extend their reach into various social media platforms', N'وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب كلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 3, 10, 10)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (7, N'game 7', N'اللعبة السابعة', N'Media gate studios aim towards building a video production that will highly relate to what is happening in everyday’s streets and what satisfies people in every aspect ,we are aiming towards understanding and highlighting what meets our clients satisfaction and to create a line of art between us and possible clients.', N'وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب وكلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب كلام كتيير عن اللعبة الثالثة الحلوة فحت وردم يبتينت نبتينبت نءتبيتب متنبنينب ', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 5, 20, 30)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (8, N'Game 8', N'اللعبة الثامنة', N'http://localhost:60084/Games/Create
http://localhost:60084/Games/Create
http://localhost:60084/Games/Crea
http://localhost:60084/Games/Create', N'تيسايتاسيتاسمسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسس
تيسايتاسيتاسمسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسس
تيسايتاسيتاسمسسسسسسسسسسسسسسسسسسسسسسسسسسسس', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 1, 20, 44)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (1008, N'Game 9', N'اللعبة التاسعة', N'Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9Game 9', N'اللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعةاللعبة التاسعة', N'URL', N'URL', N'URL', N'URL', N'URL', N'URL', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 8, 30, 22)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (1009, N'Game 10', N'اللعبة العاشرة', N'Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10Game 10', N'اللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرةاللعبة العاشرة', N'http://localhost:60084/Games/Create', NULL, N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 20, 33, 55)
GO
INSERT [dbo].[Games] ([ID], [Name], [Name-AR], [Description], [Description-AR], [Android_URL], [Ituens_URL], [Windows_URL], [Facebook_URL], [KindleFire_URL], [EB_URL], [Game_Link], [Trailer], [Watch], [Start_Tries], [Start_Points]) VALUES (1010, N'Game 11', N'اللعبة الحادية عشر', N'Game 11Game 11Game 11Game 11Game 11Game 11Game 11Game 11Game 11', N'اللعبة الحادية عشر اللعبة الحادية عشر اللعبة الحادية عشر اللعبة الحادية عشر اللعبة الحادية عشر اللعبة الحادية عشر اللعبة الحادية عشر', NULL, N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'http://localhost:60084/Games/Create', N'https://ata-sabri.itch.io/test', N'https://www.youtube.com/embed/yI9o0jQ6osY', 10, 20, 20)
GO
SET IDENTITY_INSERT [dbo].[Games] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberShip] ON 

GO
INSERT [dbo].[MemberShip] ([ID], [Name], [Price], [Limit_Token_Money], [color]) VALUES (1, N'Free  - Normal', 0, 50, NULL)
GO
INSERT [dbo].[MemberShip] ([ID], [Name], [Price], [Limit_Token_Money], [color]) VALUES (2, N'Vip - Gold', 50, 100, N'#ffd633')
GO
INSERT [dbo].[MemberShip] ([ID], [Name], [Price], [Limit_Token_Money], [color]) VALUES (3, N'Vip - Diamond', 100, 200, N'#cecacd')
GO
SET IDENTITY_INSERT [dbo].[MemberShip] OFF
GO
SET IDENTITY_INSERT [dbo].[Portfolio] ON 

GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (4, N'Ahmed')
GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (5, N'TEST 1')
GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (7, N'Content Soluthion')
GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (8, N'test')
GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (9, N'test')
GO
INSERT [dbo].[Portfolio] ([ID], [Name]) VALUES (10, N'test')
GO
SET IDENTITY_INSERT [dbo].[Portfolio] OFF
GO
SET IDENTITY_INSERT [dbo].[Prizes] ON 

GO
INSERT [dbo].[Prizes] ([ID], [Name], [Name-AR], [Type], [Third_Partner], [Price], [Points]) VALUES (1, N'prize 1', N'الجائزة الاولى ', 1, 1, NULL, 30)
GO
INSERT [dbo].[Prizes] ([ID], [Name], [Name-AR], [Type], [Third_Partner], [Price], [Points]) VALUES (2, N'Prize 2', N'الجائزة الثانية', 1, 1, NULL, 23)
GO
INSERT [dbo].[Prizes] ([ID], [Name], [Name-AR], [Type], [Third_Partner], [Price], [Points]) VALUES (3, N'Prize 3', N'الجائزة الثالثة', 1, 1, 0, 30)
GO
INSERT [dbo].[Prizes] ([ID], [Name], [Name-AR], [Type], [Third_Partner], [Price], [Points]) VALUES (4, N'20 Pound', N'20 جنسه', 0, NULL, 20, 30)
GO
INSERT [dbo].[Prizes] ([ID], [Name], [Name-AR], [Type], [Third_Partner], [Price], [Points]) VALUES (5, N'Prize 4', N'الجائزة الرابعة', 1, 1, NULL, 60)
GO
SET IDENTITY_INSERT [dbo].[Prizes] OFF
GO
SET IDENTITY_INSERT [dbo].[Splashes] ON 

GO
INSERT [dbo].[Splashes] ([ID], [Name], [Channel_ID], [Enable], [Width], [Height], [Game_ID]) VALUES (1, N'test', 2, 1, 640, 426, 1)
GO
INSERT [dbo].[Splashes] ([ID], [Name], [Channel_ID], [Enable], [Width], [Height], [Game_ID]) VALUES (3, N'splash 1', 2, 1, 800, 500, 2)
GO
INSERT [dbo].[Splashes] ([ID], [Name], [Channel_ID], [Enable], [Width], [Height], [Game_ID]) VALUES (4, N'Splash 1', 4, 1, 256, 256, 3)
GO
INSERT [dbo].[Splashes] ([ID], [Name], [Channel_ID], [Enable], [Width], [Height], [Game_ID]) VALUES (5, N'Splash 4', 3, 1, 844, 1236, 4)
GO
SET IDENTITY_INSERT [dbo].[Splashes] OFF
GO
SET IDENTITY_INSERT [dbo].[Subscribers] ON 

GO
INSERT [dbo].[Subscribers] ([ID], [Email]) VALUES (1, N'ataeldon@gmail.com')
GO
INSERT [dbo].[Subscribers] ([ID], [Email]) VALUES (2, N'ataeldon@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Subscribers] OFF
GO
SET IDENTITY_INSERT [dbo].[Third_Partner] ON 

GO
INSERT [dbo].[Third_Partner] ([ID], [Name], [User_Name], [Password]) VALUES (1, N'Ata Sabri', N'test', N'123')
GO
INSERT [dbo].[Third_Partner] ([ID], [Name], [User_Name], [Password]) VALUES (2, N'Ata Sabri', N'ahmed', N'0312392')
GO
SET IDENTITY_INSERT [dbo].[Third_Partner] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (43, N'Ata Sabri', N'ataeldon@gmail.com', N'01142229025', N'01142229025', N'Egypt', CAST(0x691E0B00 AS Date), 1, NULL, 1, 0, NULL, 1, NULL)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (44, N'Ata_', N'programming@mediagatestudios.com', N'77', N'01042229025', N'Egypt', CAST(0x691E0B00 AS Date), 1, NULL, 2, 0, 876, 1, 1)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (46, N'Mohamed', N'Moahmed@mohamed.com', N'99', N'01242229025', N'Egypt', CAST(0x691E0B00 AS Date), 1, NULL, 1, 0, NULL, 1, NULL)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (47, N'Ata Ahmed', N'aa@aa.com', N'123123', N'01052229025', N'Egypt', CAST(0x590A0B00 AS Date), 1, NULL, 2, 0, NULL, 1, 2)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (63, N'Ahmed Said', N'said@gamil.com', N'said', N'01232229025', N'Egypt', CAST(0x7A1C0B00 AS Date), 1, NULL, 1, 0, NULL, 1, NULL)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (69, N'Ata Ahmed ', N'AtaAhme@ahmed.com', N'ataahmed', N'01182229025', N'Egypt', CAST(0xFA120B00 AS Date), 1, NULL, 3, 0, NULL, 1, 1)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (74, N'Ata Sabri', N'ata@ataataata.com', N'atasabri', N'01192229025', N'Egypt', CAST(0x590A0B00 AS Date), 1, N'2004118499867852', 3, 0, 212, 1, 1)
GO
INSERT [dbo].[Users] ([ID], [Name], [Email], [Password], [Phone], [Country], [DateOfBirth], [Gender], [Face_ID], [Member_ID], [Money_Token], [GGB], [Active], [Pay]) VALUES (75, N'Sabri Ata', N'sabriata@gmail.com', N'sabriataahmed', N'01102229028', N'Egypt', CAST(0x501F0B00 AS Date), 1, NULL, 3, 0, 95, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Winners] ON 

GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (19, 43, 2, 5, 1)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (20, 44, 3, 6, 1)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (23, 46, 2, 8, 1)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (24, 47, 3, 9, 1)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (27, 44, 1, 4, 1)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (28, 44, 1, 4, 0)
GO
INSERT [dbo].[Winners] ([ID], [User_ID], [Prize_ID], [Gamer_ID], [Received]) VALUES (29, 44, 1, 8, 1)
GO
SET IDENTITY_INSERT [dbo].[Winners] OFF
GO
SET IDENTITY_INSERT [test].[GGBDeals] ON 

GO
INSERT [test].[GGBDeals] ([ID], [Name], [Price], [GGB]) VALUES (1, N'GGB Deal 1', 20, 100)
GO
INSERT [test].[GGBDeals] ([ID], [Name], [Price], [GGB]) VALUES (2, N'GBB Deal 2', 30, 200)
GO
INSERT [test].[GGBDeals] ([ID], [Name], [Price], [GGB]) VALUES (3, N'GGB Deal 3', 40, 500)
GO
INSERT [test].[GGBDeals] ([ID], [Name], [Price], [GGB]) VALUES (4, N'GGB Deal 4', 50, 600)
GO
SET IDENTITY_INSERT [test].[GGBDeals] OFF
GO
SET IDENTITY_INSERT [test].[Users_GGBDeals] ON 

GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (1, 44, 2, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (2, 44, 2, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (3, 44, 2, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (4, 44, 2, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (5, 44, 4, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (6, 44, 3, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (7, 44, 4, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (8, 44, 4, 1, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (9, 44, 1, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (10, 44, 1, 1, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (11, 44, 3, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (12, 74, 1, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (13, 74, 2, 1, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (14, 74, 2, 1, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (15, 74, 2, 1, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (16, 74, 2, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (17, 74, 2, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (18, 74, 2, 1, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (19, 74, 1, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (20, 74, 1, 1, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (21, 44, 4, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (22, 75, 1, 1, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (23, 74, 4, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (24, 44, 1, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (27, 44, 1, 1, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (28, 44, 3, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (29, 44, 1, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (30, 44, 2, 0, 1)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (31, 44, 1, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (32, 44, 2, 0, 2)
GO
INSERT [test].[Users_GGBDeals] ([ID], [User_ID], [GGBDeal_ID], [Active], [Pay]) VALUES (33, 44, 2, 0, 1)
GO
SET IDENTITY_INSERT [test].[Users_GGBDeals] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Users__5C7E359E9F47EE77]    Script Date: 11/19/2019 10:03:29 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Users__A9D105340EDE7168]    Script Date: 11/19/2019 10:03:29 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ADS]  WITH CHECK ADD  CONSTRAINT [FK__ADS__Client_ID__3D5E1FD2] FOREIGN KEY([Client_ID])
REFERENCES [dbo].[Ads_Clients] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ADS] CHECK CONSTRAINT [FK__ADS__Client_ID__3D5E1FD2]
GO
ALTER TABLE [dbo].[Ads_Clients]  WITH CHECK ADD FOREIGN KEY([Agency_ID])
REFERENCES [dbo].[Agencies] ([ID])
GO
ALTER TABLE [dbo].[Deal_Gamers]  WITH CHECK ADD  CONSTRAINT [FK__Deal_Game__Deal___03F0984C] FOREIGN KEY([Deal_ID])
REFERENCES [dbo].[Deals] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deal_Gamers] CHECK CONSTRAINT [FK__Deal_Game__Deal___03F0984C]
GO
ALTER TABLE [dbo].[Deal_Gamers]  WITH CHECK ADD  CONSTRAINT [FK__Deal_Game__Gamer__08B54D69] FOREIGN KEY([Gamer_ID])
REFERENCES [dbo].[Gamers] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deal_Gamers] CHECK CONSTRAINT [FK__Deal_Game__Gamer__08B54D69]
GO
ALTER TABLE [dbo].[Deal_Member]  WITH CHECK ADD  CONSTRAINT [FK__Deal_Memb__Deal___7F2BE32F] FOREIGN KEY([Deal_ID])
REFERENCES [dbo].[Deals] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deal_Member] CHECK CONSTRAINT [FK__Deal_Memb__Deal___7F2BE32F]
GO
ALTER TABLE [dbo].[Deal_Member]  WITH CHECK ADD  CONSTRAINT [FK__Deal_Memb__Membe__00200768] FOREIGN KEY([Member_ID])
REFERENCES [dbo].[MemberShip] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deal_Member] CHECK CONSTRAINT [FK__Deal_Memb__Membe__00200768]
GO
ALTER TABLE [dbo].[Deals]  WITH CHECK ADD  CONSTRAINT [FK__Deals__Game_ID__7C4F7684] FOREIGN KEY([Game_ID])
REFERENCES [dbo].[Games] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deals] CHECK CONSTRAINT [FK__Deals__Game_ID__7C4F7684]
GO
ALTER TABLE [dbo].[Gamers]  WITH CHECK ADD  CONSTRAINT [FK_Gamers_Games] FOREIGN KEY([Game_ID])
REFERENCES [dbo].[Games] ([ID])
GO
ALTER TABLE [dbo].[Gamers] CHECK CONSTRAINT [FK_Gamers_Games]
GO
ALTER TABLE [dbo].[Prizes]  WITH CHECK ADD  CONSTRAINT [FK__Prizes__Third_Pa__6E01572D] FOREIGN KEY([Third_Partner])
REFERENCES [dbo].[Third_Partner] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prizes] CHECK CONSTRAINT [FK__Prizes__Third_Pa__6E01572D]
GO
ALTER TABLE [dbo].[Splashes]  WITH CHECK ADD  CONSTRAINT [FK__Splashes__Channe__4316F928] FOREIGN KEY([Channel_ID])
REFERENCES [dbo].[Channels] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Splashes] CHECK CONSTRAINT [FK__Splashes__Channe__4316F928]
GO
ALTER TABLE [dbo].[Splashes]  WITH CHECK ADD FOREIGN KEY([Game_ID])
REFERENCES [dbo].[Games] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__Member_ID__04E4BC85] FOREIGN KEY([Member_ID])
REFERENCES [dbo].[MemberShip] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__Member_ID__04E4BC85]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [FK__Winners__Gamer_I__0A9D95DB] FOREIGN KEY([Gamer_ID])
REFERENCES [dbo].[Gamers] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [FK__Winners__Gamer_I__0A9D95DB]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [FK__Winners__Prize_I__76969D2E] FOREIGN KEY([Prize_ID])
REFERENCES [dbo].[Prizes] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [FK__Winners__Prize_I__76969D2E]
GO
ALTER TABLE [dbo].[Winners]  WITH CHECK ADD  CONSTRAINT [FK__Winners__User_ID__75A278F5] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Winners] CHECK CONSTRAINT [FK__Winners__User_ID__75A278F5]
GO
ALTER TABLE [test].[Payment]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [test].[Users] ([ID])
GO
ALTER TABLE [test].[Tags_Videos]  WITH CHECK ADD FOREIGN KEY([Tag_ID])
REFERENCES [test].[Tags] ([ID])
GO
ALTER TABLE [test].[Tags_Videos]  WITH CHECK ADD FOREIGN KEY([Video_ID])
REFERENCES [test].[Videos] ([ID])
GO
ALTER TABLE [test].[Users_GGBDeals]  WITH CHECK ADD  CONSTRAINT [FK__Users_GGB__GGBDe__5224328E] FOREIGN KEY([GGBDeal_ID])
REFERENCES [test].[GGBDeals] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [test].[Users_GGBDeals] CHECK CONSTRAINT [FK__Users_GGB__GGBDe__5224328E]
GO
ALTER TABLE [test].[Users_GGBDeals]  WITH CHECK ADD  CONSTRAINT [FK__Users_GGB__User___55F4C372] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [test].[Users_GGBDeals] CHECK CONSTRAINT [FK__Users_GGB__User___55F4C372]
GO
ALTER TABLE [test].[Videos]  WITH CHECK ADD FOREIGN KEY([Cat_ID])
REFERENCES [test].[Categories] ([ID])
GO
ALTER TABLE [test].[Videos]  WITH CHECK ADD FOREIGN KEY([Program_ID])
REFERENCES [test].[Programs] ([ID])
GO
USE [master]
GO
ALTER DATABASE [GamingGateStudios] SET  READ_WRITE 
GO

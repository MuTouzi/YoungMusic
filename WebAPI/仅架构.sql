USE [master]
GO
/****** Object:  Database [YoungMusic]    Script Date: 2020/7/9 13:20:49 ******/
CREATE DATABASE [YoungMusic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YoungMusic', FILENAME = N'C:\web\DataBase\YoungMusic.mdf' , SIZE = 5120KB , MAXSIZE = 102400KB , FILEGROWTH = 15%)
 LOG ON 
( NAME = N'YoungMusic_log', FILENAME = N'C:\web\DataBase\YoungMusic.ldf' , SIZE = 3072KB , MAXSIZE = 2048GB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [YoungMusic] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YoungMusic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YoungMusic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YoungMusic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YoungMusic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YoungMusic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YoungMusic] SET ARITHABORT OFF 
GO
ALTER DATABASE [YoungMusic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YoungMusic] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [YoungMusic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YoungMusic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YoungMusic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YoungMusic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YoungMusic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YoungMusic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YoungMusic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YoungMusic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YoungMusic] SET  ENABLE_BROKER 
GO
ALTER DATABASE [YoungMusic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YoungMusic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YoungMusic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YoungMusic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YoungMusic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YoungMusic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YoungMusic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YoungMusic] SET RECOVERY FULL 
GO
ALTER DATABASE [YoungMusic] SET  MULTI_USER 
GO
ALTER DATABASE [YoungMusic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YoungMusic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YoungMusic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YoungMusic] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'YoungMusic', N'ON'
GO
USE [YoungMusic]
GO
/****** Object:  Table [dbo].[Album]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[A_Id] [int] IDENTITY(1,1) NOT NULL,
	[A_SingerId] [int] NOT NULL,
	[A_Name] [nvarchar](50) NOT NULL,
	[A_Info] [nvarchar](max) NOT NULL,
	[A_Song] [nvarchar](max) NOT NULL,
	[A_Time] [smalldatetime] NOT NULL,
	[A_Img] [nvarchar](100) NOT NULL,
	[A_PlayCount] [int] NULL,
	[IsDelete] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[A_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dynamic]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dynamic](
	[D_Id] [int] IDENTITY(1,1) NOT NULL,
	[D_UserId] [int] NOT NULL,
	[D_Title] [nvarchar](100) NOT NULL,
	[D_UpTime] [smalldatetime] NOT NULL,
	[D_Content] [nvarchar](max) NOT NULL,
	[D_GoodCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[D_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DynamicComment]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DynamicComment](
	[DC_Id] [int] IDENTITY(1,1) NOT NULL,
	[DC_Pid] [int] NULL,
	[DC_Dynamic] [int] NOT NULL,
	[DC_From_User] [int] NOT NULL,
	[DC_Content] [nvarchar](max) NOT NULL,
	[DC_To_User] [int] NOT NULL,
	[DC_UpTime] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DC_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuComment]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuComment](
	[SM_Id] [int] IDENTITY(1,1) NOT NULL,
	[SM_Pid] [int] NULL,
	[SM_Menu] [int] NOT NULL,
	[SM_From_User] [int] NOT NULL,
	[SM_Content] [nvarchar](max) NOT NULL,
	[SM_To_User] [int] NOT NULL,
	[SM_UpTime] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SM_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Singer]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Singer](
	[S_Id] [int] IDENTITY(1,1) NOT NULL,
	[S_Name] [nvarchar](50) NOT NULL,
	[S_Img] [nvarchar](max) NULL,
	[S_Info] [nvarchar](max) NULL,
	[S_Audit_State] [int] NULL,
	[IsDelete] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[S_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SongComment]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongComment](
	[SC_Id] [int] IDENTITY(1,1) NOT NULL,
	[SC_Pid] [int] NULL,
	[SC_Song] [int] NOT NULL,
	[SC_From_User] [int] NOT NULL,
	[SC_Content] [nvarchar](max) NOT NULL,
	[SC_To_User] [int] NOT NULL,
	[SC_UpTime] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SC_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SongList]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongList](
	[S_Id] [int] IDENTITY(1,1) NOT NULL,
	[WYID] [nvarchar](50) NULL,
	[S_Name] [nvarchar](max) NOT NULL,
	[S_Type] [int] NOT NULL,
	[S_Singer] [int] NOT NULL,
	[S_Url] [nvarchar](max) NOT NULL,
	[S_Cover] [nvarchar](100) NOT NULL,
	[S_PlayCount] [int] NULL,
	[S_CollectCount] [int] NULL,
	[S_UpTime] [smalldatetime] NOT NULL,
	[S_Lyric] [nvarchar](max) NULL,
	[S_Album] [int] NULL,
	[S_Audit_State] [int] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK__SongList__A3DFF08DA14391DC] PRIMARY KEY CLUSTERED 
(
	[S_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SongMenu]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongMenu](
	[M_Id] [int] IDENTITY(1,1) NOT NULL,
	[M_UserId] [int] NOT NULL,
	[M_Name] [nvarchar](max) NOT NULL,
	[M_Info] [nvarchar](max) NOT NULL,
	[M_SongId] [nvarchar](max) NULL,
	[M_CreatTime] [smalldatetime] NOT NULL,
	[M_Img] [nvarchar](100) NOT NULL,
	[M_Type] [int] NOT NULL,
	[M_PlayCount] [int] NULL,
	[M_CollectCount] [int] NULL,
	[IsDelete] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[M_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[T_Id] [int] IDENTITY(1,1) NOT NULL,
	[T_Name] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[T_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/7/9 13:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[U_Id] [int] IDENTITY(1,1) NOT NULL,
	[U_Name] [nvarchar](20) NOT NULL,
	[U_Pwd] [nvarchar](20) NOT NULL,
	[U_Email] [nvarchar](50) NOT NULL,
	[U_Info] [nvarchar](max) NULL,
	[U_Tell] [nvarchar](11) NULL,
	[U_Img] [nvarchar](100) NULL,
	[U_Hobby] [nvarchar](max) NULL,
	[U_Fans] [nvarchar](max) NULL,
	[U_Follow] [nvarchar](max) NULL,
	[U_CollectSongMenu] [nvarchar](max) NULL,
	[U_CreatSongMenu] [nvarchar](max) NULL,
	[U_Like] [nvarchar](max) NULL,
	[U_Gender] [nvarchar](2) NULL,
	[U_Birthday] [smalldatetime] NULL,
	[U_RegistrationTime] [smalldatetime] NULL,
	[IsAdmin] [int] NOT NULL,
	[IsDelete] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[U_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Album] ADD  DEFAULT (getdate()) FOR [A_Time]
GO
ALTER TABLE [dbo].[Album] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Dynamic] ADD  DEFAULT (getdate()) FOR [D_UpTime]
GO
ALTER TABLE [dbo].[DynamicComment] ADD  CONSTRAINT [DF_DynamicComment_DC_UpTime]  DEFAULT (getdate()) FOR [DC_UpTime]
GO
ALTER TABLE [dbo].[MenuComment] ADD  CONSTRAINT [DF_MenuComment_SM_UpTime]  DEFAULT (getdate()) FOR [SM_UpTime]
GO
ALTER TABLE [dbo].[Singer] ADD  DEFAULT ((0)) FOR [S_Audit_State]
GO
ALTER TABLE [dbo].[Singer] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SongComment] ADD  CONSTRAINT [DF_SongComment_SC_UpTime]  DEFAULT (getdate()) FOR [SC_UpTime]
GO
ALTER TABLE [dbo].[SongList] ADD  CONSTRAINT [DF__SongList__WYID__5AEE82B9]  DEFAULT ((0)) FOR [WYID]
GO
ALTER TABLE [dbo].[SongList] ADD  CONSTRAINT [DF__SongList__S_UpTi__2C3393D0]  DEFAULT (getdate()) FOR [S_UpTime]
GO
ALTER TABLE [dbo].[SongList] ADD  CONSTRAINT [DF__SongList__S_Audi__2E1BDC42]  DEFAULT ((0)) FOR [S_Audit_State]
GO
ALTER TABLE [dbo].[SongList] ADD  CONSTRAINT [DF__SongList__IsDele__2F10007B]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SongMenu] ADD  DEFAULT (getdate()) FOR [M_CreatTime]
GO
ALTER TABLE [dbo].[SongMenu] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT (getdate()) FOR [U_RegistrationTime]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD FOREIGN KEY([A_SingerId])
REFERENCES [dbo].[Singer] ([S_Id])
GO
ALTER TABLE [dbo].[Dynamic]  WITH CHECK ADD FOREIGN KEY([D_UserId])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[DynamicComment]  WITH CHECK ADD FOREIGN KEY([DC_Dynamic])
REFERENCES [dbo].[Dynamic] ([D_Id])
GO
ALTER TABLE [dbo].[DynamicComment]  WITH CHECK ADD FOREIGN KEY([DC_From_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[DynamicComment]  WITH CHECK ADD FOREIGN KEY([DC_To_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[MenuComment]  WITH CHECK ADD FOREIGN KEY([SM_From_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[MenuComment]  WITH CHECK ADD FOREIGN KEY([SM_Menu])
REFERENCES [dbo].[SongMenu] ([M_Id])
GO
ALTER TABLE [dbo].[MenuComment]  WITH CHECK ADD FOREIGN KEY([SM_To_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[SongComment]  WITH CHECK ADD FOREIGN KEY([SC_From_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[SongComment]  WITH CHECK ADD  CONSTRAINT [FK__SongComme__SC_So__31EC6D26] FOREIGN KEY([SC_Song])
REFERENCES [dbo].[SongList] ([S_Id])
GO
ALTER TABLE [dbo].[SongComment] CHECK CONSTRAINT [FK__SongComme__SC_So__31EC6D26]
GO
ALTER TABLE [dbo].[SongComment]  WITH CHECK ADD FOREIGN KEY([SC_To_User])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[SongList]  WITH NOCHECK ADD  CONSTRAINT [FK__SongList__S_Albu__2D27B809] FOREIGN KEY([S_Album])
REFERENCES [dbo].[Album] ([A_Id])
GO
ALTER TABLE [dbo].[SongList] NOCHECK CONSTRAINT [FK__SongList__S_Albu__2D27B809]
GO
ALTER TABLE [dbo].[SongList]  WITH CHECK ADD  CONSTRAINT [FK__SongList__S_Sing__2B3F6F97] FOREIGN KEY([S_Singer])
REFERENCES [dbo].[Singer] ([S_Id])
GO
ALTER TABLE [dbo].[SongList] CHECK CONSTRAINT [FK__SongList__S_Sing__2B3F6F97]
GO
ALTER TABLE [dbo].[SongList]  WITH CHECK ADD  CONSTRAINT [FK__SongList__S_Type__2A4B4B5E] FOREIGN KEY([S_Type])
REFERENCES [dbo].[Type] ([T_Id])
GO
ALTER TABLE [dbo].[SongList] CHECK CONSTRAINT [FK__SongList__S_Type__2A4B4B5E]
GO
ALTER TABLE [dbo].[SongMenu]  WITH CHECK ADD FOREIGN KEY([M_Type])
REFERENCES [dbo].[Type] ([T_Id])
GO
ALTER TABLE [dbo].[SongMenu]  WITH CHECK ADD FOREIGN KEY([M_UserId])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
USE [master]
GO
ALTER DATABASE [YoungMusic] SET  READ_WRITE 
GO

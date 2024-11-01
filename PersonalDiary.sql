USE [master]
GO
/****** Object:  Database [PersonalDiary]    Script Date: 10/31/2024 1:54:42 PM ******/
CREATE DATABASE [PersonalDiary]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PersonalDiary', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\PersonalDiary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PersonalDiary_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\PersonalDiary_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PersonalDiary] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonalDiary].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonalDiary] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PersonalDiary] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PersonalDiary] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PersonalDiary] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PersonalDiary] SET ARITHABORT OFF 
GO
ALTER DATABASE [PersonalDiary] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PersonalDiary] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PersonalDiary] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PersonalDiary] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PersonalDiary] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PersonalDiary] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PersonalDiary] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PersonalDiary] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PersonalDiary] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PersonalDiary] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PersonalDiary] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PersonalDiary] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PersonalDiary] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PersonalDiary] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PersonalDiary] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PersonalDiary] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PersonalDiary] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PersonalDiary] SET RECOVERY FULL 
GO
ALTER DATABASE [PersonalDiary] SET  MULTI_USER 
GO
ALTER DATABASE [PersonalDiary] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PersonalDiary] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PersonalDiary] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PersonalDiary] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PersonalDiary] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PersonalDiary] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PersonalDiary', N'ON'
GO
ALTER DATABASE [PersonalDiary] SET QUERY_STORE = ON
GO
ALTER DATABASE [PersonalDiary] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PersonalDiary]
GO
/****** Object:  Table [dbo].[Like]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Like](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[postId] [int] NOT NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[createdAt] [datetime] NULL,
	[privacy] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTag]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTag](
	[postId] [int] NOT NULL,
	[tagId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[postId] ASC,
	[tagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[postId] [int] NOT NULL,
	[reason] [nvarchar](255) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Share]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Share](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[postId] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tagName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/31/2024 1:54:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[fullname] [nvarchar](100) NOT NULL,
	[dob] [date] NULL,
	[number] [nvarchar](15) NULL,
	[createdAt] [datetime] NULL,
	[isBlock] [bit] NULL,
	[roleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Like] ON 

INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (1, 2, 1, CAST(N'2024-10-31T13:50:28.650' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (2, 3, 1, CAST(N'2024-10-31T13:50:28.650' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (3, 4, 2, CAST(N'2024-10-31T13:50:28.650' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (4, 5, 3, CAST(N'2024-10-31T13:50:28.650' AS DateTime))
SET IDENTITY_INSERT [dbo].[Like] OFF
GO
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (1, 1, N'This is the first post by admin.', CAST(N'2024-10-31T13:48:31.223' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (2, 2, N'This is a post by user one.', CAST(N'2024-10-31T13:48:31.223' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (3, 3, N'This is a post by user two.', CAST(N'2024-10-31T13:48:31.223' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (4, 4, N'This is a post by user three.', CAST(N'2024-10-31T13:48:31.223' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (5, 5, N'This is a post by user four.', CAST(N'2024-10-31T13:48:31.223' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (6, 1, N'This is the first post by admin.', CAST(N'2024-10-31T13:50:03.120' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (7, 2, N'This is a post by user one.', CAST(N'2024-10-31T13:50:03.120' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (8, 3, N'This is a post by user two.', CAST(N'2024-10-31T13:50:03.120' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (9, 4, N'This is a post by user three.', CAST(N'2024-10-31T13:50:03.120' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [createdAt], [privacy]) VALUES (10, 5, N'This is a post by user four.', CAST(N'2024-10-31T13:50:03.120' AS DateTime), N'public')
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
INSERT [dbo].[PostTag] ([postId], [tagId]) VALUES (1, 1)
INSERT [dbo].[PostTag] ([postId], [tagId]) VALUES (2, 2)
INSERT [dbo].[PostTag] ([postId], [tagId]) VALUES (3, 3)
INSERT [dbo].[PostTag] ([postId], [tagId]) VALUES (4, 1)
INSERT [dbo].[PostTag] ([postId], [tagId]) VALUES (5, 2)
GO
SET IDENTITY_INSERT [dbo].[Report] ON 

INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (1, 2, 1, N'Inappropriate content', CAST(N'2024-10-31T13:48:40.900' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (2, 3, 2, N'Spam', CAST(N'2024-10-31T13:48:40.900' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (3, 2, 1, N'Inappropriate content', CAST(N'2024-10-31T13:48:48.687' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (4, 3, 2, N'Spam', CAST(N'2024-10-31T13:48:48.687' AS DateTime))
SET IDENTITY_INSERT [dbo].[Report] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id], [roleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Share] ON 

INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (1, 1, 2, CAST(N'2024-10-31T13:50:51.403' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (2, 2, 3, CAST(N'2024-10-31T13:50:51.403' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (3, 3, 4, CAST(N'2024-10-31T13:50:51.403' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (4, 4, 5, CAST(N'2024-10-31T13:50:51.403' AS DateTime))
SET IDENTITY_INSERT [dbo].[Share] OFF
GO
SET IDENTITY_INSERT [dbo].[Tag] ON 

INSERT [dbo].[Tag] ([id], [tagName]) VALUES (1, N'hangngay')
INSERT [dbo].[Tag] ([id], [tagName]) VALUES (2, N'cuocsong')
INSERT [dbo].[Tag] ([id], [tagName]) VALUES (3, N'game')
SET IDENTITY_INSERT [dbo].[Tag] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId]) VALUES (1, N'admin', N'admin@gmail.com', N'admin', N'admin', CAST(N'1990-01-01' AS Date), N'0123456789', CAST(N'2024-10-31T13:48:27.210' AS DateTime), 0, 1)
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId]) VALUES (2, N'user1', N'user1@gmail.com', N'123', N'user', CAST(N'1992-02-02' AS Date), N'0123456790', CAST(N'2024-10-31T13:48:27.210' AS DateTime), 0, 2)
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId]) VALUES (3, N'user2', N'user2@gmail.com', N'456', N'user', CAST(N'1993-03-03' AS Date), N'0123456791', CAST(N'2024-10-31T13:48:27.210' AS DateTime), 0, 2)
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId]) VALUES (4, N'user3', N'user3@gmail.com', N'789', N'user', CAST(N'1994-04-04' AS Date), N'0123456792', CAST(N'2024-10-31T13:48:27.210' AS DateTime), 0, 2)
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId]) VALUES (5, N'user4', N'user4@gmail.com', N'111', N'User user', CAST(N'1995-05-05' AS Date), N'0123456793', CAST(N'2024-10-31T13:48:27.210' AS DateTime), 0, 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Like] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Post] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Report] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Share] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [isBlock]
GO
ALTER TABLE [dbo].[Like]  WITH CHECK ADD FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[Like]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[PostTag]  WITH CHECK ADD FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[PostTag]  WITH CHECK ADD FOREIGN KEY([tagId])
REFERENCES [dbo].[Tag] ([id])
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Share]  WITH CHECK ADD FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[Share]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD CHECK  (([privacy]='private' OR [privacy]='public'))
GO
USE [master]
GO
ALTER DATABASE [PersonalDiary] SET  READ_WRITE 
GO

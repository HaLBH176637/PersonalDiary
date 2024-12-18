USE [PersonalDiary]
GO
/****** Object:  Table [dbo].[Like]    Script Date: 11/2/2024 5:58:37 PM ******/
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
/****** Object:  Table [dbo].[Post]    Script Date: 11/2/2024 5:58:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[tag] [nvarchar](50) NOT NULL,
	[createdAt] [datetime] NULL,
	[privacy] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 11/2/2024 5:58:37 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 11/2/2024 5:58:37 PM ******/
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
/****** Object:  Table [dbo].[Share]    Script Date: 11/2/2024 5:58:37 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 11/2/2024 5:58:37 PM ******/
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
	[privatePassword] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Like] ON 

INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (1, 1, 1, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (2, 2, 1, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (3, 1, 2, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (4, 3, 2, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (5, 2, 3, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (6, 4, 3, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (7, 5, 4, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (8, 6, 5, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (9, 7, 6, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
INSERT [dbo].[Like] ([id], [userId], [postId], [createdAt]) VALUES (10, 8, 7, CAST(N'2024-11-02T17:11:13.550' AS DateTime))
SET IDENTITY_INSERT [dbo].[Like] OFF
GO
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (1, 1, N'This is the first post', N'tag1', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (2, 2, N'This is the second post', N'tag2', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (3, 3, N'This is the third post', N'tag1,tag2', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (4, 4, N'This is the fourth post', N'tag3', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (5, 5, N'This is the fifth post', N'tag1', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (6, 6, N'This is the sixth post', N'tag2,tag3', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (7, 7, N'This is the seventh post', N'tag3', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (8, 8, N'This is the eighth post', N'tag1', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'private')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (9, 9, N'This is the ninth post', N'tag2', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'public')
INSERT [dbo].[Post] ([id], [userId], [content], [tag], [createdAt], [privacy]) VALUES (10, 10, N'This is the tenth post', N'tag3', CAST(N'2024-11-02T17:10:12.750' AS DateTime), N'private')
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
SET IDENTITY_INSERT [dbo].[Report] ON 

INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (1, 1, 1, N'Inappropriate content', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (2, 2, 2, N'Spam', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (3, 3, 3, N'Harassment', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (4, 4, 4, N'Inappropriate content', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (5, 5, 5, N'Spam', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (6, 6, 6, N'Inappropriate content', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (7, 7, 7, N'Harassment', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (8, 8, 8, N'Inappropriate content', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (9, 9, 9, N'Spam', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
INSERT [dbo].[Report] ([id], [userId], [postId], [reason], [createdAt]) VALUES (10, 10, 10, N'Harassment', CAST(N'2024-11-02T17:11:26.287' AS DateTime))
SET IDENTITY_INSERT [dbo].[Report] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id], [roleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Share] ON 

INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (1, 1, 1, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (2, 2, 2, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (3, 3, 3, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (4, 4, 4, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (5, 5, 5, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (6, 6, 6, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (7, 7, 7, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (8, 8, 8, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (9, 9, 9, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
INSERT [dbo].[Share] ([id], [postId], [userId], [createdAt]) VALUES (10, 10, 10, CAST(N'2024-11-02T17:11:18.363' AS DateTime))
SET IDENTITY_INSERT [dbo].[Share] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (1, N'user1', N'leha104203@gmail.com', N'leha104203', N'User One', CAST(N'1990-01-01' AS Date), N'0123456789', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'123')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (2, N'user2', N'baha104203@gmail.com', N'123', N'User Two', CAST(N'1991-02-02' AS Date), N'0123456788', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'111')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (3, N'user3', N'test@gmail.com', N'123', N'User Three', CAST(N'1992-03-03' AS Date), N'0123456787', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 2, N'222')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (4, N'user4', N'test', N'123', N'User Four', CAST(N'1993-04-04' AS Date), N'0123456786', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 2, N'333')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (5, N'user5', N'user@gmail.com', N'user', N'User Five', CAST(N'1994-05-05' AS Date), N'0123456785', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'444')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (6, N'user6', N'admin@gmail.com', N'admin', N'User Six', CAST(N'1995-06-06' AS Date), N'0123456784', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 2, N'555')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (7, N'user7', N'user', N'user', N'User Seven', CAST(N'1996-07-07' AS Date), N'0123456783', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'666')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (8, N'user8', N'admin', N'admin', N'User Eight', CAST(N'1997-08-08' AS Date), N'0123456782', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'777')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (9, N'user9', N'user9@example.com', N'password9', N'User Nine', CAST(N'1998-09-09' AS Date), N'0123456781', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 2, N'888')
INSERT [dbo].[User] ([id], [username], [email], [password], [fullname], [dob], [number], [createdAt], [isBlock], [roleId], [privatePassword]) VALUES (10, N'user10', N'user10@example.com', N'password10', N'User Ten', CAST(N'1999-10-10' AS Date), N'0123456780', CAST(N'2024-11-02T17:09:04.747' AS DateTime), 0, 1, N'999')
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

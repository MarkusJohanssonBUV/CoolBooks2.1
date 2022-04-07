USE [master]
GO
/****** Object:  Database [CoolBooks]    Script Date: 2022-04-07 11:57:16 ******/
DROP DATABASE [CoolBooks]
GO
CREATE DATABASE [CoolBooks]
GO
USE [CoolBooks]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 2022-04-07 11:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [Pk_AuthorID] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 2022-04-07 11:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BooksID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[ISBN] [varchar](30) NULL,
	[ImagePath] [varchar](450) NULL,
	[IsDeleted] [varchar](20) NULL,
	[Created] [datetime] NULL,
	[GenerID] [int] NOT NULL,
 CONSTRAINT [Pk_BooksID] PRIMARY KEY CLUSTERED 
(
	[BooksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 2022-04-07 11:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](1000) NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [Pk_GenerID] PRIMARY KEY CLUSTERED 
(
	[GenerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 2022-04-07 11:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewsID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [varchar](50) NULL,
	[Text] [varchar](500) NULL,
	[Rating] [varchar](50) NULL,
	[IdDeleted] [varchar](10) NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [Pk_ReviewsID] PRIMARY KEY CLUSTERED 
(
	[ReviewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2022-04-07 11:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserInfoID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ReviewsID] [int] NOT NULL,
	[BooksID] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Adress] [varchar](50) NULL,
	[Created] [varchar](50) NULL,
 CONSTRAINT [_UserID] PRIMARY KEY CLUSTERED 
(
	[UserInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 
GO
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Created]) VALUES (1, N'Ryan', N'North', CAST(N'2022-04-06T11:38:00.000' AS DateTime))
GO
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Created]) VALUES (2, N'Stephen', N'King', CAST(N'2022-04-06T12:51:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 
GO
INSERT [dbo].[Books] ([BooksID], [UserID], [AuthorID], [Title], [Description], [ISBN], [ImagePath], [IsDeleted], [Created], [GenerID]) VALUES (1, 1, 2, N'The Shining', N'Jack Torrance''s new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he''ll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote . . . and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old.', N'9780345806789', N'The-Shining-King-Stephen.webp', N'False', CAST(N'2022-04-06T13:21:00.000' AS DateTime), 6)
GO
INSERT [dbo].[Books] ([BooksID], [UserID], [AuthorID], [Title], [Description], [ISBN], [ImagePath], [IsDeleted], [Created], [GenerID]) VALUES (2, 1, 2, N'It', N'It: Chapter Two --now a major motion picture Stephen King''s terrifying, classic #1 New York Times bestseller, "a landmark in American literature" ( Chicago Sun-Times )--about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers...an evil without a name: It . Welcome to Derry, Maine. It''s a small city, a place as hauntingly familiar as your own hometown. Only in Derry the haunting is real. They were seven teenagers when they first stumbled upon', N' 9781982127794', N'it.bild', N'False', CAST(N'2022-04-06T14:48:00.000' AS DateTime), 6)
GO
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (1, N'Science', N'Science (from the Latin scientia, meaning “knowledge”) is the effort to discover, and increase human understanding of how the physical world works. Through controlled methods, science uses observable physical evidence of natural phenomena to collect data, and analyzes this information to explain what and how things work.', CAST(N'2022-04-06T11:39:00.000' AS DateTime))
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (2, N'Fantasy', N'Fantasy is a genre that uses magic and other supernatural forms as a primary element of plot, theme, and/or setting. Fantasy is generally distinguished from science fiction and horror by the expectation that it steers clear of technological and macabre themes, respectively, though there is a great deal of overlap between the three (collectively known as speculative fiction or science fiction/fantasy)  In its broadest sense, fantasy comprises works by many writers, artists, filmmakers, and musicians, from ancient myths and legends to many recent works embraced by a wide audience today, including young adults, most of whom are represented by the works below.', CAST(N'2022-04-06T11:40:00.000' AS DateTime))
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (3, N'Science Fiction', N'Science fiction (abbreviated SF or sci-fi with varying punctuation and capitalization) is a broad genre of fiction that often involves speculations based on current or future science or technology. Science fiction is found in books, art, television, films, games, theatre, and other media. In organizational or marketing contexts, science fiction can be synonymous with the broader definition of speculative fiction, encompassing creative works incorporating imaginative elements not found in contemporary reality; this includes fantasy, horror and related genres.  Although the two genres are often conflated as science fiction/fantasy, science fiction differs from fantasy in that, within the context of the story, its imaginary elements are largely possible within scientifically established or scientifically postulated laws of nature (though some elements in a story might still be pure imaginative speculation). Exploring the consequences of such differences is the traditional purpose of scien', CAST(N'2022-04-06T11:41:00.000' AS DateTime))
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (4, N'Fiction', N'Fiction is the telling of stories which are not real. More specifically, fiction is an imaginative form of narrative, one of the four basic rhetorical modes. Although the word fiction is derived from the Latin fingo, fingere, finxi, fictum, "to form, create", works of fiction need not be entirely imaginary and may include real people, places, and events. Fiction may be either written or oral. Although not all fiction is necessarily artistic, fiction is largely perceived as a form of art or entertainment. The ability to create fiction and other artistic works is considered to be a fundamental aspect of human culture, one of the defining characteristics of humanity. ', CAST(N'2022-04-06T11:42:00.000' AS DateTime))
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (5, N'Classics', N'A classic stands the test of time. The work is usually considered to be a representation of the period in which it was written; and the work merits lasting recognition. In other words, if the book was published in the recent past, the work is not a classic.  A classic has a certain universal appeal. Great works of literature touch us to our very core beings--partly because they integrate themes that are understood by readers from a wide range of backgrounds and levels of experience. Themes of love, hate, death, life, and faith touch upon some of our most basic emotional responses.  Although the term is often associated with the Western canon, it can be applied to works of literature from all traditions, such as the Chinese classics or the Indian Vedas.', CAST(N'2022-04-06T11:43:00.000' AS DateTime))
GO
INSERT [dbo].[Genres] ([GenerID], [Name], [Description], [Created]) VALUES (6, N'Horror', N'Horror fiction is fiction in any medium intended to scare, unsettle, or horrify the audience. Historically, the cause of the "horror" experience has often been the intrusion of a supernatural element into everyday human experience. Since the 1960s, any work of fiction with a morbid, gruesome, surreal, or exceptionally suspenseful or frightening theme has come to be called "horror". Horror fiction often overlaps science fiction or fantasy, all three of which categories are sometimes placed under the umbrella classification speculative fiction. ', CAST(N'2022-04-06T14:49:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Reviews] ON 
GO
INSERT [dbo].[Reviews] ([ReviewsID], [BookID], [UserID], [Title], [Text], [Rating], [IdDeleted], [Created]) VALUES (1, 1, 1, N'The Shining', N'Läskig', N'R', NULL, CAST(N'2022-04-06T14:09:00.000' AS DateTime))
GO
INSERT [dbo].[Reviews] ([ReviewsID], [BookID], [UserID], [Title], [Text], [Rating], [IdDeleted], [Created]) VALUES (3, 1, 1, N'The Shining', N'Fortfarande Läskig', N'5', NULL, CAST(N'2022-04-06T14:42:00.000' AS DateTime))
GO
INSERT [dbo].[Reviews] ([ReviewsID], [BookID], [UserID], [Title], [Text], [Rating], [IdDeleted], [Created]) VALUES (4, 2, 1, N'It', N'Läskig', N'4', NULL, CAST(N'2022-04-06T15:30:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Reviews] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [Fk_AuthorID] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [Fk_AuthorID]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [Fk_Genres] FOREIGN KEY([GenerID])
REFERENCES [dbo].[Genres] ([GenerID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [Fk_Genres]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [Fk_User_Books] FOREIGN KEY([BooksID])
REFERENCES [dbo].[Books] ([BooksID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [Fk_User_Books]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [Fk_UserID] FOREIGN KEY([ReviewsID])
REFERENCES [dbo].[Reviews] ([ReviewsID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [Fk_UserID]
GO
USE [master]
GO
ALTER DATABASE [CoolBooks] SET  READ_WRITE 
GO

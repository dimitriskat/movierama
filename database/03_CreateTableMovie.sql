USE [MovieRama]
GO

/****** Object:  Table [dbo].[Movie]    Script Date: 9/16/2018 4:16:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[User] [int] NULL,
	[PublicationDate] [date] NULL,
	[Likes] [int] NOT NULL,
	[Hates] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Movie] ADD  CONSTRAINT [DF_Movie_CreationTime]  DEFAULT (getutcdate()) FOR [CreationTime]
GO

ALTER TABLE [dbo].[Movie] ADD  CONSTRAINT [DF_Movie_LastUpdateTime]  DEFAULT (getutcdate()) FOR [LastUpdateTime]
GO



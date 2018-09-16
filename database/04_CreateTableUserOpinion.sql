USE [MovieRama]
GO

/****** Object:  Table [dbo].[UserOpinion]    Script Date: 9/16/2018 4:16:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserOpinion](
	[User] [int] NOT NULL,
	[Movie] [int] NOT NULL,
	[Opinion] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserOpinion] PRIMARY KEY CLUSTERED 
(
	[User] ASC,
	[Movie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



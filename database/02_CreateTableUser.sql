USE [MovieRama]
GO

/****** Object:  Table [dbo].[User]    Script Date: 9/16/2018 4:14:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[Username] [nvarchar](250) NOT NULL,
	[PasswordHash] [binary](64) NOT NULL,
	[PasswordSalt] [binary](128) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreationTime]  DEFAULT (getutcdate()) FOR [CreationTime]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_LastUpdateTime]  DEFAULT (getutcdate()) FOR [LastUpdateTime]
GO



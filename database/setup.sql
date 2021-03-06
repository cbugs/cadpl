IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignTransactions]'))
ALTER TABLE [dbo].[ProCampaignTransactions] DROP CONSTRAINT [FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignStatus_dbo.Entries_EntryId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignStatus]'))
ALTER TABLE [dbo].[ProCampaignStatus] DROP CONSTRAINT [FK_dbo.ProCampaignStatus_dbo.Entries_EntryId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ParticipantDataCaches]'))
ALTER TABLE [dbo].[ParticipantDataCaches] DROP CONSTRAINT [FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] DROP CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] DROP CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] DROP CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_dbo.Entries_dbo.Participants_ParticipantId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient3Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient2Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient1Id]
GO
/****** Object:  Table [dbo].[ProCampaignTransactions]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProCampaignTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[ProCampaignTransactions]
GO
/****** Object:  Table [dbo].[ProCampaignStatus]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProCampaignStatus]') AND type in (N'U'))
DROP TABLE [dbo].[ProCampaignStatus]
GO
/****** Object:  Table [dbo].[Participants]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participants]') AND type in (N'U'))
DROP TABLE [dbo].[Participants]
GO
/****** Object:  Table [dbo].[ParticipantDataCaches]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParticipantDataCaches]') AND type in (N'U'))
DROP TABLE [dbo].[ParticipantDataCaches]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ingredients]') AND type in (N'U'))
DROP TABLE [dbo].[Ingredients]
GO
/****** Object:  Table [dbo].[ExistingBarCombinations]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]') AND type in (N'U'))
DROP TABLE [dbo].[ExistingBarCombinations]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 6/27/2018 3:38:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entries]') AND type in (N'U'))
DROP TABLE [dbo].[Entries]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entries]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Entries](
	[Id] [uniqueidentifier] NOT NULL,
	[ParticipantId] [uniqueidentifier] NOT NULL,
	[ParticipantDataCacheId] [uniqueidentifier] NOT NULL,
	[BarName] [nvarchar](50) NOT NULL,
	[BarDescription] [nvarchar](500) NULL,
	[BarColour] [nvarchar](50) NOT NULL,
	[Ingredient1Id] [uniqueidentifier] NULL,
	[Ingredient2Id] [uniqueidentifier] NULL,
	[Ingredient3Id] [uniqueidentifier] NULL,
	[RejectedIngredients] [text] NULL,
	[CompositedImage] [nvarchar](500) NULL,
	[CompositedImageShare] [nvarchar](500) NULL,
	[Colour] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.Entries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ExistingBarCombinations]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ExistingBarCombinations](
	[BarName] [nvarchar](200) NOT NULL,
	[Ingredient1Id] [uniqueidentifier] NULL,
	[Ingredient2Id] [uniqueidentifier] NULL,
	[Ingredient3Id] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.ExistingBarCombinations] PRIMARY KEY CLUSTERED 
(
	[BarName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ingredients]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ingredients](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Colour] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](100) NOT NULL,
	[PackImagePath] [nvarchar](400) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ParticipantDataCaches]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParticipantDataCaches]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ParticipantDataCaches](
	[Id] [uniqueidentifier] NOT NULL,
	[ParticipantId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[MobileNumber] [nvarchar](100) NOT NULL,
	[CountryCode] [nvarchar](10) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.ParticipantDataCaches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Participants]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participants]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Participants](
	[Id] [uniqueidentifier] NOT NULL,
	[EmailHash] [nvarchar](64) NOT NULL,
	[ConsumerId] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.Participants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProCampaignStatus]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProCampaignStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProCampaignStatus](
	[Id] [uniqueidentifier] NOT NULL,
	[EntryId] [uniqueidentifier] NOT NULL,
	[IsSuccessful] [bit] NOT NULL,
	[ResponseCode] [int] NOT NULL,
	[ResponseText] [text] NULL,
	[HttpStatusCode] [int] NOT NULL,
	[HttpStatusMessage] [text] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.ProCampaignStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProCampaignTransactions]    Script Date: 6/27/2018 3:38:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProCampaignTransactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProCampaignTransactions](
	[Id] [uniqueidentifier] NOT NULL,
	[ProCampaignStatusId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Database] [nvarchar](100) NOT NULL,
	[TransactionObject] [text] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.ProCampaignTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Chips Ahoy ', N'298f1a8e-4846-4b22-9b40-d0e669b3a67f', NULL, NULL, CAST(N'2018-06-12T13:20:04.503' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Cola Pretzel Honeycomb', N'0d81cbf8-b3b4-4737-b421-118ddd9bca9c', N'd16d997c-7803-497f-a2ff-59fd26286b12', N'2f1ff116-13b9-4bc9-a672-b9fd2a6af6fe', CAST(N'2018-06-12T13:20:04.490' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Crunchie', N'2f1ff116-13b9-4bc9-a672-b9fd2a6af6fe', NULL, NULL, CAST(N'2018-06-12T13:20:04.483' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Daim', N'259687bf-71fb-4553-be59-16f93080d8b3', NULL, NULL, CAST(N'2018-06-12T13:20:04.487' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Fruit & Nut', N'e9a34932-9dbc-4f73-8720-d53e048ff95e', N'2f9de534-e376-4c46-a88a-ac5781e9ac1e', NULL, CAST(N'2018-06-12T13:20:04.360' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Oreo', N'25fa4c2f-6e51-4580-ab07-afb4dec9335a', NULL, NULL, CAST(N'2018-06-12T13:20:04.487' AS DateTime), NULL)
INSERT [dbo].[ExistingBarCombinations] ([BarName], [Ingredient1Id], [Ingredient2Id], [Ingredient3Id], [CreatedOn], [UpdatedOn]) VALUES (N'CDM Wholenut', N'2fe2763d-fd0f-4b01-852a-b0ebded76141', NULL, NULL, CAST(N'2018-06-12T13:20:04.477' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'b385e773-c1a2-42a5-bf72-01ea875844a2', N'Lemon green tea', N'AMBER', N'Drinks', N'ingredients/drinks/lemongreentea.png', CAST(N'2018-06-12T13:20:04.050' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'18cc5c5f-cf66-4651-9ad5-07085de40e3b', N'Lychee', N'GREEN', N'Fruity', N'ingredients/fruity/lychee.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2eaa45cc-8401-4cda-b950-09842088133c', N'Melon', N'AMBER', N'Fruity', N'ingredients/fruity/melon.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2c821124-efbe-4aa6-aeae-0a6188798d56', N'Digestive biscuit', N'AMBER', N'Crunchy', N'ingredients/crunchy/digestivebiscuit.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'56b18480-e789-41b1-a462-10291bd84cf0', N'Blueberry', N'GREEN', N'Fruity', N'ingredients/fruity/blueberry.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'8ebf5245-ffaf-491a-b281-1108786659ad', N'Oats', N'GREEN', N'Crunchy', N'ingredients/crunchy/oats.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'0d81cbf8-b3b4-4737-b421-118ddd9bca9c', N'Cola jellies', N'GREEN', N'Chewy', N'ingredients/chewy/colajellies.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'259687bf-71fb-4553-be59-16f93080d8b3', N'Daim', N'GREEN', N'Crunchy', N'ingredients/crunchy/daim.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'5fa8d21b-57c7-4cdc-a1ae-1dcc60ba2e92', N'Pineapple', N'AMBER', N'Fruity', N'ingredients/fruity/pineapple.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'239ab8e8-785c-4803-961b-244167fd9a13', N'Crunchy corn', N'GREEN', N'Crunchy', N'ingredients/crunchy/crunchycorn.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'c62a028e-8232-4c11-98bd-25fd0ad676eb', N'Orange', N'GREEN', N'Fruity', N'ingredients/fruity/orange.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'e164713e-a779-4427-8225-2af0cbd3a30b', N'Tomato', N'AMBER', N'Wildcard', N'ingredients/wildcard/tomato.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'c41f969f-be49-4d02-a7cf-2b00f90a3ded', N'Cornflakes', N'GREEN', N'Crunchy', N'ingredients/crunchy/cornflakes.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'e80ad58b-249e-4cf7-98c4-3369ce13c50a', N'Dijon mustard', N'AMBER', N'Wildcard', N'ingredients/wildcard/dijonmustard.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'8207164e-f36b-42cd-8aa8-342e996d956b', N'Ginger', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/ginger.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'4eb5ffe5-9995-44f4-8e59-37373a1207b4', N'Rice crisps', N'GREEN', N'Crunchy', N'ingredients/crunchy/ricecrisps.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'73ac01f8-60c2-4ba8-9b44-37ba150f59e0', N'Grapefruit', N'GREEN', N'Fruity', N'ingredients/fruity/grapefruit.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'33763769-69a9-4759-ab54-3930d77e894a', N'Toblerone', N'GREEN', N'Wildcard', N'ingredients/wildcard/toblerone.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd5b6cfa2-68e0-4e4c-b75c-39e3a2c2d36e', N'Black pepper', N'RED', N'Herbs and Spices', N'ingredients/herbsandspices/blackpepper.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'f7fe5ade-6b31-42bf-ae44-3c34ab2d5fd1', N'Coconut', N'GREEN', N'Creamy', N'ingredients/creamy/coconut.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'35c558ea-8dc9-4b09-861d-3c4eb559ea15', N'Brownie', N'GREEN', N'Chewy', N'ingredients/chewy/brownie.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2781f6fb-0b3a-4eb2-8735-3eacd211997a', N'Yogurt Berry Granola', N'GREEN', N'Wildcard', N'ingredients/wildcard/yogurtberrygranola.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'1efc7daf-b98c-4aa9-98d7-40471ee63c12', N'Ice cream', N'GREEN', N'Creamy', N'ingredients/creamy/icecream.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'721cc745-90ad-4275-ab5c-409f457cf2aa', N'Peach', N'GREEN', N'Fruity', N'ingredients/fruity/peach.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd2ce0fa9-a4da-43ff-a1c9-43cfb7fafe2b', N'Shortcake', N'GREEN', N'Crunchy', N'ingredients/crunchy/shortcake.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'33c8e4c7-b6e7-42e9-b1f6-489d600327f9', N'Gluten free biscuit', N'GREEN', N'Crunchy', N'ingredients/crunchy/glutenfreebiscuit.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'708cb5bd-3cc7-451a-9ec0-4e684f48d218', N'Clove', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/clove.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'5a9b8e03-72f6-487e-bca3-50e3aa8a4ec2', N'Spearmint Tea', N'AMBER', N'Drinks', N'ingredients/drinks/spearminttea.png', CAST(N'2018-06-12T13:20:04.050' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd16d997c-7803-497f-a2ff-59fd26286b12', N'Pretzel', N'GREEN', N'Crunchy', N'ingredients/crunchy/pretzel.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd595fb73-f43d-4240-9bd2-5bbafd5f9e33', N'Liquorice', N'GREEN', N'Wildcard', N'ingredients/wildcard/liquorice.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'158ce9d3-0a6f-4c9f-82e0-5fe9833e1c8a', N'Pear', N'AMBER', N'Fruity', N'ingredients/fruity/pear.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'6516aaee-a781-4ec0-b62e-60ae472b9600', N'Pecans', N'GREEN', N'Nuts', N'ingredients/nuts/pecans.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'8293ba71-621e-4cbf-afb2-61eefdbde79f', N'Vanilla', N'GREEN', N'Creamy', N'ingredients/creamy/vanilla.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'b5c98d77-e771-4889-a234-6c1d76dc4b69', N'Tropical', N'AMBER', N'Fruity', N'ingredients/fruity/tropical.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'95aa119b-5966-4e01-9fef-6fd84edf76ad', N'Strawberry', N'AMBER', N'Fruity', N'ingredients/fruity/strawberry.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'47c1507e-ad36-40d5-8697-71958240abfc', N'Coffee', N'GREEN', N'Drinks', N'ingredients/drinks/coffee.png', CAST(N'2018-06-12T13:20:04.050' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'5a120c83-833e-4655-aebc-7511dabd1781', N'Mint', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/mint.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'0d83f892-8e70-413b-932d-754f69b5d7e3', N'Wafer', N'GREEN', N'Crunchy', N'ingredients/crunchy/wafer.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'fe008f83-1ce0-439f-8730-783009069c8c', N'Apricot', N'GREEN', N'Fruity', N'ingredients/fruity/apricot.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'cc37e4f1-b4fe-4cba-9686-7af8e7aa82c8', N'Popcorn', N'GREEN', N'Crunchy', N'ingredients/crunchy/popcorn.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'58977a2a-c598-48a3-ae0e-7bd607d517fd', N'Basil', N'AMBER', N'Herbs and Spices', N'ingredients/herbsandspices/basil.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'5ece16e0-6112-4456-bb42-83eb96d27ac6', N'Cherry', N'GREEN', N'Fruity', N'ingredients/fruity/cherry.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'71e5a457-d5d9-4885-accc-8639431751dd', N'Crunchy Caramel', N'GREEN', N'Crunchy', N'ingredients/crunchy/crunchycaramel.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'419b4748-ab5f-4af5-9d71-8cc5615be0a4', N'Sprinkles ', N'GREEN', N'Crunchy', N'ingredients/crunchy/sprinkles.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'77b6cdad-63b3-4d70-883c-8d60f15b2c7e', N'Apple', N'GREEN', N'Fruity', N'ingredients/fruity/apple.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'bc653c0d-2a8e-4063-84d7-8e238e14a0c3', N'Passion fruit', N'AMBER', N'Fruity', N'ingredients/fruity/passionfruit.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'49fff55d-6cad-410b-a198-93deece96b17', N'Rosemary', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/rosemary.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'48934e73-9a2f-4481-910c-95b8ff6c451f', N'Lime', N'GREEN', N'Fruity', N'ingredients/fruity/lime.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd8e84a85-af0f-425d-b766-9acd57bdaf60', N'Cashews', N'GREEN', N'Nuts', N'ingredients/nuts/cashews.png', CAST(N'2018-06-12T13:20:04.037' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'a2c286ab-c8a7-49b4-9e28-9b468935d841', N'Bubblegum', N'GREEN', N'Wildcard', N'ingredients/wildcard/bubblegum.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'adf510f0-3674-400b-8eaa-9f01b7f3545a', N'Cinnamon', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/cinnamon.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'22c6c695-8226-4e4a-a91f-a1a711ffc7a5', N'Speculoos', N'GREEN', N'Crunchy', N'ingredients/crunchy/speculoos.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'c556a046-a097-4559-aedf-a9c5645df5b6', N'Rose', N'GREEN', N'Wildcard', N'ingredients/wildcard/rose.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd06e6aaf-0b8d-473f-9a8c-ab7dae5025a6', N'Toffee', N'GREEN', N'Chewy', N'ingredients/chewy/toffee.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'aca2cd73-5656-420e-b3c1-ac1c4187e1e1', N'Mixed berry', N'GREEN', N'Fruity', N'ingredients/fruity/mixedberry.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2f9de534-e376-4c46-a88a-ac5781e9ac1e', N'Almonds', N'GREEN', N'Nuts', N'ingredients/nuts/almonds.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'c57ecd59-9ee8-4a28-a8c6-ad717ab57231', N'Salted caramel chips', N'GREEN', N'Chewy', N'ingredients/chewy/saltedcaramelchips.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'25fa4c2f-6e51-4580-ab07-afb4dec9335a', N'Oreo ', N'GREEN', N'Crunchy', N'ingredients/crunchy/oreo.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'defb3382-b389-43ed-8df6-b08b2c688ccb', N'Pistachio', N'GREEN', N'Nuts', N'ingredients/nuts/pistachio.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2fe2763d-fd0f-4b01-852a-b0ebded76141', N'Hazelnuts', N'GREEN', N'Nuts', N'ingredients/nuts/hazelnuts.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2beb075f-1414-4619-b9e6-b3b2bbac8585', N'Mint pieces', N'AMBER', N'Crunchy', N'ingredients/crunchy/mintpieces.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2f1ff116-13b9-4bc9-a672-b9fd2a6af6fe', N'Honeycomb', N'GREEN', N'Crunchy', N'ingredients/crunchy/honeycomb.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'2ba357ff-4429-4129-8cda-bdadb8290340', N'Popping candy', N'GREEN', N'Wildcard', N'ingredients/wildcard/poppingcandy.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'cc4c9922-fcb6-4163-92a1-bfefd8bba1dd', N'Caramel nuggets', N'GREEN', N'Chewy', N'ingredients/chewy/caramelnuggets.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'8188000b-79cc-4434-bb56-c100a5b9eada', N'Animal jellies', N'GREEN', N'Chewy', N'ingredients/chewy/animaljellies.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'5387abd0-553c-4e94-848f-c36dd28e1ce7', N'Chilli', N'AMBER', N'Herbs and Spices', N'ingredients/herbsandspices/chilli.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'760e402a-7894-4342-8e08-c6ba711d4d72', N'Elderflower', N'AMBER', N'Wildcard', N'ingredients/wildcard/elderflower.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'a7de1816-8cc1-465e-a296-c8677c111641', N'Raspberry Fudge', N'GREEN', N'Chewy', N'ingredients/chewy/raspberryfudge.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'ee14902f-7b09-4eef-b70c-c88b87d8a891', N'Cookie dough', N'AMBER', N'Chewy', N'ingredients/chewy/cookiedough.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'bcbe6b0f-1534-43de-9443-c916da766434', N'Lemon grass', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/lemongrass.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'10d961a3-7612-44e1-941e-c9c8fd972590', N'Marshmallow', N'RED', N'Chewy', N'ingredients/chewy/marshmallow.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'e339c97c-b54c-4618-93d3-cb570bc3267e', N'Fig', N'AMBER', N'Fruity', N'ingredients/fruity/fig.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'907f6371-1446-4225-8042-cb67074b881c', N'Sea salt', N'RED', N'Herbs and Spices', N'ingredients/herbsandspices/seasalt.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'298f1a8e-4846-4b22-9b40-d0e669b3a67f', N'Cookie', N'GREEN', N'Crunchy', N'ingredients/crunchy/cookie.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'e9a34932-9dbc-4f73-8720-d53e048ff95e', N'Raisins', N'GREEN', N'Fruity', N'ingredients/fruity/raisins.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'009f42ed-1017-4d7a-a01f-d5d85001d046', N'Watermelon', N'GREEN', N'Fruity', N'ingredients/fruity/watermelon.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'c0b38303-2d35-49cb-be22-d6cccd5af48a', N'Tarragon', N'GREEN', N'Herbs and Spices', N'ingredients/herbsandspices/tarragon.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'69b06d0b-da62-4aba-934f-d9cfce601c5d', N'Cranberries', N'GREEN', N'Fruity', N'ingredients/fruity/cranberries.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'659e7166-07bc-4f79-8313-dba574d6e6d4', N'White choc chips', N'GREEN', N'Creamy', N'ingredients/creamy/whitechocchips.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'26db757b-9c19-4904-a2ad-e69ebe0733c1', N'Raspberry', N'GREEN', N'Fruity', N'ingredients/fruity/raspberry.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'407deaca-012b-473b-8c55-ea2e1c78712f', N'Banana', N'GREEN', N'Fruity', N'ingredients/fruity/banana.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'd276a881-5df9-4442-b38c-f19511aec431', N'Lemon and Ginger Tea', N'AMBER', N'Drinks', N'ingredients/drinks/lemonandgingertea.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'6c2c5c91-1a27-4aa3-8aaa-f61d2663ca2b', N'Lemon', N'AMBER', N'Fruity', N'ingredients/fruity/lemon.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
INSERT [dbo].[Ingredients] ([Id], [Name], [Colour], [Category], [PackImagePath], [CreatedOn], [UpdatedOn]) VALUES (N'a577048d-c0a5-4898-8f71-faebe5f54a87', N'Fudge', N'GREEN', N'Chewy', N'ingredients/chewy/fudge.png', CAST(N'2018-06-12T13:20:04.047' AS DateTime), NULL)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient1Id] FOREIGN KEY([Ingredient1Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient1Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient2Id] FOREIGN KEY([Ingredient2Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient2Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient3Id] FOREIGN KEY([Ingredient3Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_dbo.Entries_dbo.Ingredients_Ingredient3Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId] FOREIGN KEY([ParticipantDataCacheId])
REFERENCES [dbo].[ParticipantDataCaches] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_dbo.Entries_dbo.ParticipantDataCaches_ParticipantDataCacheId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Entries_dbo.Participants_ParticipantId] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Participants] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Entries_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entries]'))
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_dbo.Entries_dbo.Participants_ParticipantId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id] FOREIGN KEY([Ingredient1Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] CHECK CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient1Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id] FOREIGN KEY([Ingredient2Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] CHECK CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient2Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id] FOREIGN KEY([Ingredient3Id])
REFERENCES [dbo].[Ingredients] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExistingBarCombinations]'))
ALTER TABLE [dbo].[ExistingBarCombinations] CHECK CONSTRAINT [FK_dbo.ExistingBarCombinations_dbo.Ingredients_Ingredient3Id]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ParticipantDataCaches]'))
ALTER TABLE [dbo].[ParticipantDataCaches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Participants] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ParticipantDataCaches]'))
ALTER TABLE [dbo].[ParticipantDataCaches] CHECK CONSTRAINT [FK_dbo.ParticipantDataCaches_dbo.Participants_ParticipantId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignStatus_dbo.Entries_EntryId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignStatus]'))
ALTER TABLE [dbo].[ProCampaignStatus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProCampaignStatus_dbo.Entries_EntryId] FOREIGN KEY([EntryId])
REFERENCES [dbo].[Entries] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignStatus_dbo.Entries_EntryId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignStatus]'))
ALTER TABLE [dbo].[ProCampaignStatus] CHECK CONSTRAINT [FK_dbo.ProCampaignStatus_dbo.Entries_EntryId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignTransactions]'))
ALTER TABLE [dbo].[ProCampaignTransactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId] FOREIGN KEY([ProCampaignStatusId])
REFERENCES [dbo].[ProCampaignStatus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProCampaignTransactions]'))
ALTER TABLE [dbo].[ProCampaignTransactions] CHECK CONSTRAINT [FK_dbo.ProCampaignTransactions_dbo.ProCampaignStatus_ProCampaignStatusId]
GO

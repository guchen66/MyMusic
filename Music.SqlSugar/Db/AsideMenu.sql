/*
 Navicat Premium Data Transfer

 Source Server         : liuxin
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : .:1433
 Source Catalog        : MusicDB
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 20/03/2024 21:22:46
*/


-- ----------------------------
-- Table structure for AsideMenu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AsideMenu]') AND type IN ('U'))
	DROP TABLE [dbo].[AsideMenu]
GO

CREATE TABLE [dbo].[AsideMenu] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Icon] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [Content] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [NameSpace] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AsideMenu] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of AsideMenu
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AsideMenu] ON
GO

INSERT INTO [dbo].[AsideMenu] ([Id], [Icon], [Content], [NameSpace]) VALUES (N'1', N'Home', N'首页', N'HomeView')
GO

INSERT INTO [dbo].[AsideMenu] ([Id], [Icon], [Content], [NameSpace]) VALUES (N'2', N'Favorite', N'收藏', N'FavorView')
GO

INSERT INTO [dbo].[AsideMenu] ([Id], [Icon], [Content], [NameSpace]) VALUES (N'3', N'DownLoad', N'下载', N'DownLoadView')
GO

INSERT INTO [dbo].[AsideMenu] ([Id], [Icon], [Content], [NameSpace]) VALUES (N'4', N'History', N'最近播放', N'RecentView')
GO

INSERT INTO [dbo].[AsideMenu] ([Id], [Icon], [Content], [NameSpace]) VALUES (N'5', N'Settings', N'设置', N'SetView')
GO

SET IDENTITY_INSERT [dbo].[AsideMenu] OFF
GO


-- ----------------------------
-- Auto increment value for AsideMenu
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[AsideMenu]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table AsideMenu
-- ----------------------------
ALTER TABLE [dbo].[AsideMenu] ADD CONSTRAINT [PK_AsideMenu_Id] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


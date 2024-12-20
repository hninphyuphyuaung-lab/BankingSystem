USE [master]
GO
/****** Object:  Database [BankingDb]    Script Date: 8/12/2024 5:32:43 pm ******/
CREATE DATABASE [BankingDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BankingDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BankingDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BankingDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BankingDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BankingDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankingDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankingDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankingDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankingDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankingDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankingDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankingDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BankingDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankingDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankingDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankingDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankingDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankingDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankingDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankingDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankingDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BankingDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankingDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankingDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankingDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankingDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankingDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BankingDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankingDb] SET RECOVERY FULL 
GO
ALTER DATABASE [BankingDb] SET  MULTI_USER 
GO
ALTER DATABASE [BankingDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankingDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankingDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankingDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BankingDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BankingDb] SET QUERY_STORE = OFF
GO
USE [BankingDb]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 8/12/2024 5:32:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [nvarchar](50) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterestRules]    Script Date: 8/12/2024 5:32:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterestRules](
	[Date] [date] NOT NULL,
	[RuleId] [nvarchar](50) NOT NULL,
	[Rate] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 8/12/2024 5:32:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[AccountId] [nvarchar](50) NOT NULL,
	[Type] [char](1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([AccountId], [Balance]) VALUES (N'AC001', CAST(300.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([AccountId], [Balance]) VALUES (N'AC002', CAST(200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[InterestRules] ([Date], [RuleId], [Rate]) VALUES (CAST(N'2023-06-15' AS Date), N'RULE03', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[InterestRules] ([Date], [RuleId], [Rate]) VALUES (CAST(N'2023-07-15' AS Date), N'RULE03', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[InterestRules] ([Date], [RuleId], [Rate]) VALUES (CAST(N'2023-12-01' AS Date), N'RULE01', CAST(2.50 AS Decimal(5, 2)))
GO
INSERT [dbo].[Transactions] ([TransactionId], [Date], [AccountId], [Type], [Amount]) VALUES (N'20230626-01', CAST(N'2023-06-26' AS Date), N'AC002', N'D', CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Transactions] ([TransactionId], [Date], [AccountId], [Type], [Amount]) VALUES (N'20230626-02', CAST(N'2023-06-26' AS Date), N'AC001', N'W', CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Transactions] ([TransactionId], [Date], [AccountId], [Type], [Amount]) VALUES (N'20230626-03', CAST(N'2023-06-26' AS Date), N'AC001', N'D', CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Transactions] ([TransactionId], [Date], [AccountId], [Type], [Amount]) VALUES (N'20231201-01', CAST(N'2023-06-01' AS Date), N'AC001', N'D', CAST(150.00 AS Decimal(18, 2)))
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[InterestRules]  WITH CHECK ADD CHECK  (([Rate]>(0) AND [Rate]<(100)))
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD CHECK  (([Type]='W' OR [Type]='D'))
GO
USE [master]
GO
ALTER DATABASE [BankingDb] SET  READ_WRITE 
GO

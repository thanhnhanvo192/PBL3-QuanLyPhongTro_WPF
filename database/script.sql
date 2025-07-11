USE [master]
GO
/****** Object:  Database QuanLyPhongTro    Script Date: 6/9/2025 12:32:08 PM ******/
CREATE DATABASE [QuanLyPhongTro]
 CONTAINMENT = NONE

 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE QuanLyPhongTro SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC QuanLyPhongTro.[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyPhongTro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyPhongTro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyPhongTro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyPhongTro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyPhongTro] SET ARITHABORT OFF 
GO
ALTER DATABASE QuanLyPhongTro SET AUTO_CLOSE ON 
GO
ALTER DATABASE QuanLyPhongTro SET AUTO_SHRINK OFF 
GO
ALTER DATABASE QuanLyPhongTro SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE QuanLyPhongTro SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE QuanLyPhongTro SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE QuanLyPhongTro SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE QuanLyPhongTro SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE QuanLyPhongTro SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE QuanLyPhongTro SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE QuanLyPhongTro SET  ENABLE_BROKER 
GO
ALTER DATABASE QuanLyPhongTro SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE QuanLyPhongTro SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE QuanLyPhongTro SET TRUSTWORTHY OFF 
GO
ALTER DATABASE QuanLyPhongTro SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE QuanLyPhongTro SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE QuanLyPhongTro SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE QuanLyPhongTro SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE QuanLyPhongTro SET RECOVERY SIMPLE 
GO
ALTER DATABASE QuanLyPhongTro SET  MULTI_USER 
GO
ALTER DATABASE QuanLyPhongTro SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE QuanLyPhongTro SET DB_CHAINING OFF 
GO
ALTER DATABASE QuanLyPhongTro SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE QuanLyPhongTro SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE QuanLyPhongTro SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE QuanLyPhongTro SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE QuanLyPhongTro SET QUERY_STORE = ON
GO
ALTER DATABASE QuanLyPhongTro SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE QuanLyPhongTro
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractCode] [nvarchar](50) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Deposit] [decimal](18, 2) NOT NULL,
	[RoomId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fixes]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fixes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NOT NULL,
	[TenantID] [int] NULL,
	[FixDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[InvoiceId] [int] NULL,
	[WhoFault] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Fixes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_details]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ServiceId] [int] NULL,
	[Quantity] [decimal](10, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Notes] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Invoice_details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceCode] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[AmountPaid] [decimal](18, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[ContractId] [int] NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeterReadings]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeterReadings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[ReadingDate] [datetime2](7) NOT NULL,
	[ReadingValue] [decimal](10, 2) NOT NULL,
	[Notes] [nvarchar](255) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MeterReadings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Occupants]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Occupants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CCCD] [nvarchar](450) NOT NULL,
	[Birthday] [datetime2](7) NULL,
	[Sex] [int] NULL,
	[PermanentAddress] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Occupants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Punishes]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Punishes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NOT NULL,
	[PunishDate] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[InvoiceId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Punishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [nvarchar](20) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Area] [decimal](10, 2) NULL,
	[MaxOccupants] [int] NULL,
	[Floor] [int] NULL,
	[Utilities] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[UnitId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[CCCD] [nvarchar](20) NOT NULL,
	[Birthday] [datetime2](7) NULL,
	[Sex] [int] NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[PermanentAddress] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 6/9/2025 12:32:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250507074806_InitialCreate', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250507125243_AddAccountTable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513090224_MakeRoomUtilitiesAndDescriptionNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513113855_ConvertStatusToEnum', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513152032_MakeTenatPhoneAndEmailAndPermanentAddressNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513164837_MakeTenantSexNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250513171328_MakeTenantSexNotNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514062258_MakeNotesOfMeterReadingNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514070445_UnitCode', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514110755_CreateEnumFaultType', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514111813_ChangeEnumFaultType', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250517092234_CreateContractStatusEnum', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518012346_UpdateOccupantModel', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518015941_OccupantModelInheritedBaseViewModel', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518020214_OccupantModelUpdate', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518092715_InitialInvoiceStatusEnum', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518143041_DeletePeriodInInvoice', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250519045104_UpdateInvoice_detailModel', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250519050241_UpdateInvoice_detail', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250519101128_ServiceIdInInvoice_detailNullable', N'9.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250602030055_IsDeleted', N'9.0.5')
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [UserName], [PasswordHash]) VALUES (1, N'admin', N'db69fc039dcbd2962cb4d28f5891aae1')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Contracts] ON 

INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (1, N'HD001', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-31T00:00:00.0000000' AS DateTime2), CAST(5000000.00 AS Decimal(18, 2)), 1, 1, 0, N'Hợp đồng thuê phòng P101 bởi Nguyễn Văn An', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (2, N'HD002', CAST(N'2024-03-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-14T00:00:00.0000000' AS DateTime2), CAST(6000000.00 AS Decimal(18, 2)), 3, 3, 0, N'Hợp đồng thuê phòng P201 bởi Lê Văn Cường', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (3, N'HD003', CAST(N'2023-06-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-31T00:00:00.0000000' AS DateTime2), CAST(4000000.00 AS Decimal(18, 2)), 2, 2, 0, N'Hợp đồng cũ phòng P102 bởi Trần Thị Bình - đã hết hạn', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (4, N'HD004', CAST(N'2025-01-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-09T00:00:00.0000000' AS DateTime2), CAST(2000000.00 AS Decimal(18, 2)), 4, 4, 0, N'Hợp đồng thuê phòng P202 bởi Phạm Thị Mỹ Dung - 6 tháng', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (7, N'HD005', CAST(N'2025-05-17T21:37:49.6163244' AS DateTime2), CAST(N'2026-05-17T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 1, 1, 1, N'Hợp đồn tháng 5, anh An, thời hạn 1 năm', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (9, N'HD006', CAST(N'2025-05-17T21:52:12.3386933' AS DateTime2), CAST(N'2025-06-17T21:52:12.3387177' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 2, 2, 0, N'Chị Bình thuê 1 tháng', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (10, N'HD007', CAST(N'2025-05-17T21:56:38.9120033' AS DateTime2), CAST(N'2025-06-17T21:56:38.9120520' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 3, 3, 1, N'Anh Tèo điên', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (11, N'HD008', CAST(N'2025-05-18T21:42:41.4763874' AS DateTime2), CAST(N'2025-06-18T21:42:41.4764034' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 2, 9, 1, N'Thuê cho vui', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (12, N'HD_P204_6/2025', CAST(N'2025-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2026-06-09T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 21, 13, 0, N'Hợp đồng phòng 204 tháng 6/2025', 1)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (13, N'HD_P202_6/2025', CAST(N'2025-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-09T00:00:00.0000000' AS DateTime2), CAST(2000000.00 AS Decimal(18, 2)), 4, 10, 1, N'Hợp đồng thuê phòng P202 bởi Đặng Quang Sơn', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (14, N'HD_P203_1/2025', CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-01T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 15, 14, 1, N'Hợp đồng anh Nguyễn Quốc Việt', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (15, N'HD_P103_1/2025', CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-01T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 16, 17, 1, N'Hợp đồng anh Nguyễn Văn Cao', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (16, N'HD_P205_1/2025', CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-01T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 22, 12, 1, N'Hợp đồng anh Đặng Thành Nam - tháng 1/2025', 0)
INSERT [dbo].[Contracts] ([Id], [ContractCode], [StartDate], [EndDate], [Deposit], [RoomId], [TenantId], [Status], [Notes], [IsDeleted]) VALUES (17, N'HD00005', CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-01T00:00:00.0000000' AS DateTime2), CAST(500000.00 AS Decimal(18, 2)), 18, 16, 1, N'HD', 0)
SET IDENTITY_INSERT [dbo].[Contracts] OFF
GO
SET IDENTITY_INSERT [dbo].[Fixes] ON 

INSERT [dbo].[Fixes] ([Id], [RoomID], [TenantID], [FixDate], [Description], [Cost], [InvoiceId], [WhoFault], [IsDeleted]) VALUES (1, 1, 1, CAST(N'2024-02-10T00:00:00.0000000' AS DateTime2), N'Sửa vòi nước bị rò rỉ do khách làm hỏng', CAST(130000.00 AS Decimal(18, 2)), 1, 0, 0)
INSERT [dbo].[Fixes] ([Id], [RoomID], [TenantID], [FixDate], [Description], [Cost], [InvoiceId], [WhoFault], [IsDeleted]) VALUES (2, 3, 3, CAST(N'2024-04-05T00:00:00.0000000' AS DateTime2), N'Sơn lại tường bị ẩm mốc (bảo trì chung)', CAST(500000.00 AS Decimal(18, 2)), 2, 0, 0)
INSERT [dbo].[Fixes] ([Id], [RoomID], [TenantID], [FixDate], [Description], [Cost], [InvoiceId], [WhoFault], [IsDeleted]) VALUES (3, 3, 3, CAST(N'2023-10-15T00:00:00.0000000' AS DateTime2), N'Thay bóng đèn cháy', CAST(80000.00 AS Decimal(18, 2)), 2, 0, 0)
INSERT [dbo].[Fixes] ([Id], [RoomID], [TenantID], [FixDate], [Description], [Cost], [InvoiceId], [WhoFault], [IsDeleted]) VALUES (4, 1, 1, CAST(N'2025-05-19T00:00:00.0000000' AS DateTime2), N'Sửa ống nước do khách làm gãy.', CAST(125000.00 AS Decimal(18, 2)), 9, 0, 0)
SET IDENTITY_INSERT [dbo].[Fixes] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice_details] ON 

INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (2, 1, 3, CAST(1.00 AS Decimal(10, 2)), CAST(2500000.00 AS Decimal(18, 2)), N'Tiền phòng P101 tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (3, 1, 1, CAST(100.00 AS Decimal(10, 2)), CAST(3500.00 AS Decimal(18, 2)), N'Điện tháng 5 (100 kWh)', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (4, 1, 2, CAST(10.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(18, 2)), N'Nước tháng 5 (10 m3)', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (5, 1, 4, CAST(1.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(18, 2)), N'Internet tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (6, 1, 5, CAST(1.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(18, 2)), N'Vệ sinh tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (7, 2, 3, CAST(1.00 AS Decimal(10, 2)), CAST(3000000.00 AS Decimal(18, 2)), N'Tiền phòng P201 tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (8, 2, 1, CAST(120.00 AS Decimal(10, 2)), CAST(3500.00 AS Decimal(18, 2)), N'Điện tháng 5 (120 kWh)', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (9, 2, 2, CAST(15.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(18, 2)), N'Nước tháng 5 (15 m3)', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (10, 2, 4, CAST(1.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(18, 2)), N'Internet tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (11, 2, 5, CAST(1.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(18, 2)), N'Vệ sinh tháng 5', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (12, 9, 1, CAST(40.00 AS Decimal(10, 2)), CAST(3500.00 AS Decimal(18, 2)), N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (13, 9, 2, CAST(40.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(18, 2)), N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (14, 10, 1, CAST(30.00 AS Decimal(10, 2)), CAST(3500.00 AS Decimal(18, 2)), N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (15, 10, 2, CAST(15.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(18, 2)), N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (16, 9, 16, CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(18, 2)), N'Chi phí sửa chữa', 0)
INSERT [dbo].[Invoice_details] ([Id], [InvoiceId], [ServiceId], [Quantity], [UnitPrice], [Notes], [IsDeleted]) VALUES (20, 9, NULL, CAST(1.00 AS Decimal(10, 2)), CAST(90000.00 AS Decimal(18, 2)), N'Hoá đơn xử phạt: Làm hư thùng rác chung, Ngày phạt: 19/05/2025', 0)
SET IDENTITY_INSERT [dbo].[Invoice_details] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (1, N'INV001', CAST(N'2025-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), CAST(3200000.00 AS Decimal(18, 2)), CAST(3200000.00 AS Decimal(18, 2)), 1, 1, N'Hóa đơn tiền nhà tháng 5/2024 phòng P101', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (2, N'INV002', CAST(N'2025-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), CAST(3845000.00 AS Decimal(18, 2)), CAST(3850000.00 AS Decimal(18, 2)), 1, 2, N'Hóa đơn tiền nhà tháng 5/2024 phòng P201', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (3, N'INV003', CAST(N'2025-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2025-06-10T00:00:00.0000000' AS DateTime2), CAST(3250000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 3, N'Hóa đơn tiền nhà tháng 6/2024 phòng P102 - chưa thanh toán', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (4, N'INV004', CAST(N'2025-04-02T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-10T00:00:00.0000000' AS DateTime2), CAST(3150000.00 AS Decimal(18, 2)), CAST(3150000.00 AS Decimal(18, 2)), 1, 1, N'Hóa đơn tiền nhà tháng 4/2024 phòng P101', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (9, N'HD20250518185842_7', CAST(N'2025-05-18T18:58:42.1285360' AS DateTime2), CAST(N'2025-06-17T18:58:42.1285361' AS DateTime2), CAST(2740000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 7, N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (10, N'HD20250518185853_10', CAST(N'2025-05-18T18:58:53.1471804' AS DateTime2), CAST(N'2025-06-17T18:58:53.1471804' AS DateTime2), CAST(2830000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 10, N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (12, N'INV005', CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), CAST(N'2025-06-17T00:00:00.0000000' AS DateTime2), CAST(2000000.00 AS Decimal(18, 2)), CAST(1700000.00 AS Decimal(18, 2)), 0, 10, N'Tiền vay', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (13, N'HD20250601211127_7', CAST(N'2025-06-01T21:11:27.4627236' AS DateTime2), CAST(N'2025-07-01T21:11:27.4627237' AS DateTime2), CAST(2000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 7, N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (14, N'HD20250601211127_10', CAST(N'2025-06-01T21:11:27.5492150' AS DateTime2), CAST(N'2025-07-01T21:11:27.5492151' AS DateTime2), CAST(2500000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 10, N'Hóa đơn tự động tạo', 0)
INSERT [dbo].[Invoices] ([Id], [InvoiceCode], [CreateDate], [DueDate], [TotalAmount], [AmountPaid], [Status], [ContractId], [Notes], [IsDeleted]) VALUES (15, N'HD20250601211127_11', CAST(N'2025-06-01T21:11:27.5518589' AS DateTime2), CAST(N'2025-07-01T21:11:27.5518590' AS DateTime2), CAST(2200000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 11, N'Hóa đơn tự động tạo', 0)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
GO
SET IDENTITY_INSERT [dbo].[MeterReadings] ON 

INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (1, 1, 1, CAST(N'2024-04-30T00:00:00.0000000' AS DateTime2), CAST(1250.00 AS Decimal(10, 2)), N'Chỉ số điện cuối tháng 4 P101', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (2, 1, 2, CAST(N'2024-04-30T00:00:00.0000000' AS DateTime2), CAST(350.00 AS Decimal(10, 2)), N'Chỉ số nước cuối tháng 4 P101', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (3, 1, 1, CAST(N'2024-05-31T00:00:00.0000000' AS DateTime2), CAST(1260.00 AS Decimal(10, 2)), N'Chỉ số điện cuối tháng 5 P101 (Sử dụng 100kWh)', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (4, 2, 2, CAST(N'2024-05-31T00:00:00.0000000' AS DateTime2), CAST(360.00 AS Decimal(10, 2)), N'Chỉ số nước cuối tháng 5 P101 (Sử dụng 10m3)', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (5, 3, 1, CAST(N'2024-04-30T00:00:00.0000000' AS DateTime2), CAST(800.00 AS Decimal(10, 2)), N'Chỉ số điện cuối tháng 4 P201', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (6, 3, 2, CAST(N'2024-04-30T00:00:00.0000000' AS DateTime2), CAST(220.00 AS Decimal(10, 2)), N'Chỉ số nước cuối tháng 4 P201', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (7, 3, 1, CAST(N'2024-05-31T00:00:00.0000000' AS DateTime2), CAST(920.00 AS Decimal(10, 2)), N'Chỉ số điện cuối tháng 5 P201 (Sử dụng 120kWh)', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (8, 3, 2, CAST(N'2024-05-31T00:00:00.0000000' AS DateTime2), CAST(235.00 AS Decimal(10, 2)), N'Chỉ số nước cuối tháng 5 P201 (Sử dụng 15m3)', 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (12, 1, 1, CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), CAST(1300.00 AS Decimal(10, 2)), NULL, 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (13, 1, 2, CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), CAST(400.00 AS Decimal(10, 2)), NULL, 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (14, 3, 1, CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), CAST(950.00 AS Decimal(10, 2)), NULL, 0)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (15, 2, 2, CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), CAST(251.00 AS Decimal(10, 2)), N'vừa thay đổi', 1)
INSERT [dbo].[MeterReadings] ([Id], [RoomId], [ServiceId], [ReadingDate], [ReadingValue], [Notes], [IsDeleted]) VALUES (16, 4, 2, CAST(N'2025-06-08T00:00:00.0000000' AS DateTime2), CAST(100000.00 AS Decimal(10, 2)), N'thêm cho vui', 1)
SET IDENTITY_INSERT [dbo].[MeterReadings] OFF
GO
SET IDENTITY_INSERT [dbo].[Occupants] ON 

INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (1, 1, N'Lan', N'Hoàng Thị', N'001200000555', CAST(N'2000-08-10T00:00:00.0000000' AS DateTime2), 1, N'144 Nguyễn Lương Bằng', N'032942424', 0)
INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (2, 2, N'Minh', N'Đặng Quang', N'001200000666', CAST(N'2001-02-25T00:00:00.0000000' AS DateTime2), 0, N'Hải Châu, Đà Nẵng', N'043242424', 0)
INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (3, 2, N'Tú', N'Nguyễn Anh', N'001200000777', CAST(N'1998-12-01T00:00:00.0000000' AS DateTime2), 0, N'Sơn Trà, Đà Nẵng', N'063563654', 0)
INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (4, 1, N'Nhân', N'Võ Thành', N'045205005842', CAST(N'2005-02-19T00:00:00.0000000' AS DateTime2), 0, N'Triệu Phong, Quảng Trị', N'0363092603', 0)
INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (5, 2, N'Nân', N'Võ Thành', N'045205005841', CAST(N'2005-02-19T00:00:00.0000000' AS DateTime2), 0, N'Triệu Phong, Quảng Trị', N'0363092604', 1)
INSERT [dbo].[Occupants] ([Id], [ContractId], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [PermanentAddress], [Phone], [IsDeleted]) VALUES (6, 2, N'Văn Dũng', N'Trịnh ', N'02314234324234', CAST(N'2004-07-16T00:00:00.0000000' AS DateTime2), 0, N'255 Lê Thạch - Liên Chiểu - Đà Nẵng', N'048393432', 0)
SET IDENTITY_INSERT [dbo].[Occupants] OFF
GO
SET IDENTITY_INSERT [dbo].[Punishes] ON 

INSERT [dbo].[Punishes] ([Id], [ContractID], [PunishDate], [Reason], [Amount], [InvoiceId], [IsDeleted]) VALUES (1, 1, CAST(N'2024-03-15T00:00:00.0000000' AS DateTime2), N'Nộp tiền nhà trễ hạn tháng 02/2024', CAST(100000.00 AS Decimal(18, 2)), NULL, 0)
INSERT [dbo].[Punishes] ([Id], [ContractID], [PunishDate], [Reason], [Amount], [InvoiceId], [IsDeleted]) VALUES (2, 2, CAST(N'2024-05-20T00:00:00.0000000' AS DateTime2), N'Làm ồn sau 22h, vi phạm quy định', CAST(200000.00 AS Decimal(18, 2)), NULL, 0)
INSERT [dbo].[Punishes] ([Id], [ContractID], [PunishDate], [Reason], [Amount], [InvoiceId], [IsDeleted]) VALUES (7, 7, CAST(N'2025-05-18T00:00:00.0000000' AS DateTime2), N'Làm hư thùng rác chung của tầng 1', CAST(95000.00 AS Decimal(18, 2)), NULL, 1)
SET IDENTITY_INSERT [dbo].[Punishes] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (1, N'P101', CAST(2500000.00 AS Decimal(18, 2)), CAST(25.50 AS Decimal(10, 2)), 2, 1, N'Điều hòa, Nóng lạnh, WC riêng, Bếp nhỏ', 2, N'Phòng tầng 1, view sân vườn', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (2, N'P102', CAST(2200000.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(10, 2)), 2, 1, N'Nóng lạnh, WC riêng', 2, N'Phòng tầng 1, yên tĩnh', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (3, N'P201', CAST(2500000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 2, N'Điều hòa, Nóng lạnh, Ban công', 2, N'Phòng tầng 2, có ban công thoáng mát, có máy giặt', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (4, N'P202', CAST(2000000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 2, N'WC riêng', 2, N'Phòng tầng 2, phù hợp cho 1 người ở', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (15, N'P203', CAST(2000000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 2, N'WC riêng', 2, N'Phòng tầng 2, phù hợp cho 1 người ở', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (16, N'P103', CAST(2000000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 1, N'WC riêng, điều hoà', 2, N'Phù hợp 2 người ở, sạch sẽ, thoáng mát.', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (17, N'P104', CAST(2000000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 1, N'WC riêng, điều hoà', 1, N'Phù hợp 2 người ở, 3 cửa sổ thoáng mát, sạch sẽ.', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (18, N'P105', CAST(2500000.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(10, 2)), 3, 1, N'Điều hòa, Nóng lạnh, WC riêng, Bếp nhỏ', 2, N'Phòng tầng 1, có thể ở 3 người.', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (21, N'P204', CAST(2000000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 2, N'WC riêng', 1, N'Phòng tầng 2, phù hợp cho 1 người ở', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (22, N'P205', CAST(3000000.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(10, 2)), 3, 2, N'WC riêng', 2, N'Phòng tầng 2, rỗng rãi, thoáng mát.', 0)
INSERT [dbo].[Rooms] ([Id], [RoomNumber], [Price], [Area], [MaxOccupants], [Floor], [Utilities], [Status], [Description], [IsDeleted]) VALUES (23, N'P206', CAST(2500000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(10, 2)), 2, 2, N'Điều hòa, Nóng lạnh, Ban công', 1, N'Phòng tầng 2, có máy giặt, có view đẹp, ban công thoáng mát.', 1)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (1, N'Tiền điện', CAST(3500.00 AS Decimal(18, 2)), 1, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (2, N'Tiền nước sinh hoạt', CAST(15000.00 AS Decimal(18, 2)), 2, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (3, N'Tiền phòng 22m2', CAST(2200000.00 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (4, N'Internet', CAST(180000.00 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (5, N'Phí vệ sinh chung', CAST(50000.00 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (14, N'Tiền phòng 20m2', CAST(2000000.00 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (15, N'Tiền phòng 30m2', CAST(3000000.00 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[Services] ([Id], [DisplayName], [Price], [UnitId], [IsDeleted]) VALUES (16, N'Tiền phạt', CAST(0.00 AS Decimal(18, 2)), 3, 1)
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Tenants] ON 

INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (1, N'An', N'Nguyễn Văn', N'001200000111', CAST(N'2000-05-15T00:00:00.0000000' AS DateTime2), 0, N'0905111222', N'an.nguyen@email.com', N'123 Đường ABC, Quận Hải Châu, Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (2, N'Bình', N'Trần Thị', N'001200000222', CAST(N'1999-11-20T00:00:00.0000000' AS DateTime2), 1, N'0905222333', N'binh.tran@email.com', N'456 Đường XYZ, Quận Sơn Trà, Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (3, N'Tèo', N'Lê ', N'001200000331', CAST(N'1995-01-19T00:00:00.0000000' AS DateTime2), 2, N'0905333443', N'teo.le@email.com', N'144 Nguyễn Lương Bằng, Quận Liên Chiểu, Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (4, N'Dung', N'Phạm Thị Mỹ', N'001200000444', CAST(N'2002-07-07T00:00:00.0000000' AS DateTime2), 1, N'0905444555', N'dung.pham@email.com', N'101 Đường PQR, Quận Liên Chiểu, Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (9, N'Nhân', N'Võ ', N'05345354353', CAST(N'2025-02-07T00:00:00.0000000' AS DateTime2), 0, N'044872843', N'vn@gmail.com', N'DN', 1)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (10, N'Quang Sơn', N'Đặng', N'03842382354', CAST(N'2005-01-05T00:00:00.0000000' AS DateTime2), 0, N'03923942', N'sondang@gmail.com', N'Nghệ An', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (11, N'Thành Nhân', N'Võ ', N'03423423423424', CAST(N'2005-02-19T00:00:00.0000000' AS DateTime2), 0, N'03424234324', N'vonhan@gmail.com', N'Quảng Trị', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (12, N'Thành Nam', N'Đặng', N'0423423424', CAST(N'2003-01-14T00:00:00.0000000' AS DateTime2), 0, N'0545435345', N'thanhnam@gmail.com', N'Hà Tĩnh', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (13, N'Văn Vũ', N'Nguyễn', N'023423424342', CAST(N'2000-02-16T00:00:00.0000000' AS DateTime2), 0, N'054399531', N'vanvu@gmail.com', N'Sơn Trà - Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (14, N'Quốc Việt', N'Nguyễn', N'04234242523432', CAST(N'2006-06-22T00:00:00.0000000' AS DateTime2), 0, N'0986545443', N'quocviet@gmail.com', N'Ngũ Hành Sơn, Đà Nẵng', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (15, N'Quốc Bảo', N'Trần ', N'042342429342', CAST(N'1996-02-17T00:00:00.0000000' AS DateTime2), 0, N'0432944234', N'quocbao@gmail.com', N'Quảng Nam', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (16, N'Văn Luyện', N'Lê', N'0234425656443', CAST(N'2001-07-14T00:00:00.0000000' AS DateTime2), 0, N'0657546343', N'levanluyen@gmail.com', N'Bình Dương', 0)
INSERT [dbo].[Tenants] ([Id], [FirstName], [LastName], [CCCD], [Birthday], [Sex], [Phone], [Email], [PermanentAddress], [IsDeleted]) VALUES (17, N'Văn Cao', N'Nguyễn', N'0234324554534', CAST(N'2007-06-13T00:00:00.0000000' AS DateTime2), 0, N'0434254642', N'vancao@gmail.com', N'Hải Phòng', 0)
SET IDENTITY_INSERT [dbo].[Tenants] OFF
GO
SET IDENTITY_INSERT [dbo].[Units] ON 

INSERT [dbo].[Units] ([UnitId], [DisplayName], [Code], [IsDeleted]) VALUES (1, N'kWh', N'KWH', 0)
INSERT [dbo].[Units] ([UnitId], [DisplayName], [Code], [IsDeleted]) VALUES (2, N'm3', N'M3', 0)
INSERT [dbo].[Units] ([UnitId], [DisplayName], [Code], [IsDeleted]) VALUES (3, N'Phòng/Tháng', N'PER_ROOM_MONTH', 0)
INSERT [dbo].[Units] ([UnitId], [DisplayName], [Code], [IsDeleted]) VALUES (4, N'Người/Tháng', N'PER_PERSON_MONTH', 0)
INSERT [dbo].[Units] ([UnitId], [DisplayName], [Code], [IsDeleted]) VALUES (7, N'Nhân/Tháng', N'KWH', 1)
SET IDENTITY_INSERT [dbo].[Units] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Accounts_UserName]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts_UserName] ON [dbo].[Accounts]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Contracts_ContractCode]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Contracts_ContractCode] ON [dbo].[Contracts]
(
	[ContractCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contracts_RoomId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contracts_RoomId] ON [dbo].[Contracts]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contracts_TenantId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contracts_TenantId] ON [dbo].[Contracts]
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fixes_InvoiceId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Fixes_InvoiceId] ON [dbo].[Fixes]
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fixes_RoomID]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Fixes_RoomID] ON [dbo].[Fixes]
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fixes_TenantID]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Fixes_TenantID] ON [dbo].[Fixes]
(
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoice_details_InvoiceId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoice_details_InvoiceId] ON [dbo].[Invoice_details]
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoice_details_ServiceId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoice_details_ServiceId] ON [dbo].[Invoice_details]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_ContractId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_ContractId] ON [dbo].[Invoices]
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Invoices_InvoiceCode]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Invoices_InvoiceCode] ON [dbo].[Invoices]
(
	[InvoiceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MeterReadings_RoomId_ServiceId_ReadingDate]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MeterReadings_RoomId_ServiceId_ReadingDate] ON [dbo].[MeterReadings]
(
	[RoomId] ASC,
	[ServiceId] ASC,
	[ReadingDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MeterReadings_ServiceId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_MeterReadings_ServiceId] ON [dbo].[MeterReadings]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Occupants_CCCD]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Occupants_CCCD] ON [dbo].[Occupants]
(
	[CCCD] ASC
)
WHERE ([CCCD] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Occupants_ContractId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Occupants_ContractId] ON [dbo].[Occupants]
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Punishes_ContractID]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Punishes_ContractID] ON [dbo].[Punishes]
(
	[ContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Punishes_InvoiceId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Punishes_InvoiceId] ON [dbo].[Punishes]
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Rooms_RoomNumber]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Rooms_RoomNumber] ON [dbo].[Rooms]
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Services_UnitId]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_Services_UnitId] ON [dbo].[Services]
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tenants_CCCD]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tenants_CCCD] ON [dbo].[Tenants]
(
	[CCCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tenants_Email]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tenants_Email] ON [dbo].[Tenants]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tenants_Phone]    Script Date: 6/9/2025 12:32:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tenants_Phone] ON [dbo].[Tenants]
(
	[Phone] ASC
)
WHERE ([Phone] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contracts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Fixes] ADD  DEFAULT ((0)) FOR [WhoFault]
GO
ALTER TABLE [dbo].[Fixes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Invoice_details] ADD  DEFAULT ((0.0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[Invoice_details] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MeterReadings] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Occupants] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Punishes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Services] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tenants] ADD  DEFAULT ((0)) FOR [Sex]
GO
ALTER TABLE [dbo].[Tenants] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Units] ADD  DEFAULT (N'') FOR [Code]
GO
ALTER TABLE [dbo].[Units] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Rooms_RoomId]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Tenants_TenantId]
GO
ALTER TABLE [dbo].[Fixes]  WITH CHECK ADD  CONSTRAINT [FK_Fixes_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[Fixes] CHECK CONSTRAINT [FK_Fixes_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[Fixes]  WITH CHECK ADD  CONSTRAINT [FK_Fixes_Rooms_RoomID] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Fixes] CHECK CONSTRAINT [FK_Fixes_Rooms_RoomID]
GO
ALTER TABLE [dbo].[Fixes]  WITH CHECK ADD  CONSTRAINT [FK_Fixes_Tenants_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenants] ([Id])
GO
ALTER TABLE [dbo].[Fixes] CHECK CONSTRAINT [FK_Fixes_Tenants_TenantID]
GO
ALTER TABLE [dbo].[Invoice_details]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_details_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoice_details] CHECK CONSTRAINT [FK_Invoice_details_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[Invoice_details]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_details_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Invoice_details] CHECK CONSTRAINT [FK_Invoice_details_Services_ServiceId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Contracts_ContractId] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Contracts_ContractId]
GO
ALTER TABLE [dbo].[MeterReadings]  WITH CHECK ADD  CONSTRAINT [FK_MeterReadings_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MeterReadings] CHECK CONSTRAINT [FK_MeterReadings_Rooms_RoomId]
GO
ALTER TABLE [dbo].[MeterReadings]  WITH CHECK ADD  CONSTRAINT [FK_MeterReadings_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MeterReadings] CHECK CONSTRAINT [FK_MeterReadings_Services_ServiceId]
GO
ALTER TABLE [dbo].[Occupants]  WITH CHECK ADD  CONSTRAINT [FK_Occupants_Contracts_ContractId] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Occupants] CHECK CONSTRAINT [FK_Occupants_Contracts_ContractId]
GO
ALTER TABLE [dbo].[Punishes]  WITH CHECK ADD  CONSTRAINT [FK_Punishes_Contracts_ContractID] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Punishes] CHECK CONSTRAINT [FK_Punishes_Contracts_ContractID]
GO
ALTER TABLE [dbo].[Punishes]  WITH CHECK ADD  CONSTRAINT [FK_Punishes_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[Punishes] CHECK CONSTRAINT [FK_Punishes_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Units_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([UnitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Units_UnitId]
GO
USE [master]
GO
ALTER DATABASE QuanLyPhongTro SET  READ_WRITE 
GO

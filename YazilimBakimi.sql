USE [master]
GO
/****** Object:  Database [YazilimBakimi]    Script Date: 7.12.2019 11:01:14 ******/
CREATE DATABASE [YazilimBakimi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YazilimBakimi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\YazilimBakimi.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'YazilimBakimi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\YazilimBakimi_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [YazilimBakimi] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YazilimBakimi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YazilimBakimi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YazilimBakimi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YazilimBakimi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YazilimBakimi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YazilimBakimi] SET ARITHABORT OFF 
GO
ALTER DATABASE [YazilimBakimi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YazilimBakimi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YazilimBakimi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YazilimBakimi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YazilimBakimi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YazilimBakimi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YazilimBakimi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YazilimBakimi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YazilimBakimi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YazilimBakimi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YazilimBakimi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YazilimBakimi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YazilimBakimi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YazilimBakimi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YazilimBakimi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YazilimBakimi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YazilimBakimi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YazilimBakimi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YazilimBakimi] SET  MULTI_USER 
GO
ALTER DATABASE [YazilimBakimi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YazilimBakimi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YazilimBakimi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YazilimBakimi] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [YazilimBakimi] SET DELAYED_DURABILITY = DISABLED 
GO
USE [YazilimBakimi]
GO
/****** Object:  Table [dbo].[havale_virman]    Script Date: 7.12.2019 11:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[havale_virman](
	[islemID] [int] IDENTITY(1,1) NOT NULL,
	[islemTarihi] [datetime] NULL,
	[turu] [nvarchar](10) NULL,
	[tutar] [decimal](18, 2) NULL,
	[aliciHesapNo] [nvarchar](13) NULL,
	[gondericiHesapNo] [nvarchar](13) NULL,
	[platform] [nvarchar](15) NULL,
 CONSTRAINT [PK_havale_virman] PRIMARY KEY CLUSTERED 
(
	[islemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hesap]    Script Date: 7.12.2019 11:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hesap](
	[hesapNo] [nvarchar](13) NOT NULL,
	[musteriNo] [bigint] NULL,
	[bakiye] [decimal](18, 2) NULL,
	[aktiflikDurumu] [bit] NULL,
 CONSTRAINT [PK_hesap] PRIMARY KEY CLUSTERED 
(
	[hesapNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HGS_Banka]    Script Date: 7.12.2019 11:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HGS_Banka](
	[HGSNumarası] [nvarchar](13) NOT NULL,
	[hgsBakiyesi] [decimal](18, 2) NULL,
	[HGSMusteriNumarasi] [bigint] NULL,
 CONSTRAINT [PK_HGS_Banka_1] PRIMARY KEY CLUSTERED 
(
	[HGSNumarası] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HGS_Kurum]    Script Date: 7.12.2019 11:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HGS_Kurum](
	[HGSNO] [nvarchar](13) NOT NULL,
	[hgsBakiye] [decimal](18, 2) NULL,
	[musteriNumarasi] [bigint] NULL,
 CONSTRAINT [PK_HGS_Banka] PRIMARY KEY CLUSTERED 
(
	[HGSNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[kullanici]    Script Date: 7.12.2019 11:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kullanici](
	[musteriNo] [bigint] IDENTITY(111222333,1) NOT NULL,
	[ad] [nvarchar](20) NOT NULL,
	[soyad] [nvarchar](20) NOT NULL,
	[TCKN] [nvarchar](11) NOT NULL,
	[sifre] [nvarchar](6) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[adres] [text] NOT NULL,
	[telefon] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_kullanici] PRIMARY KEY CLUSTERED 
(
	[musteriNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[havale_virman] ON 

INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (15, CAST(N'2019-10-26 20:40:58.263' AS DateTime), N'Virman', CAST(99.44 AS Decimal(18, 2)), N'1112223351001', N'1112223351002', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (16, CAST(N'2019-11-05 16:33:35.263' AS DateTime), N'Virman', CAST(190.00 AS Decimal(18, 2)), N'1112223351002', N'1112223351001', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (17, CAST(N'2019-11-06 13:50:42.850' AS DateTime), N'Virman', CAST(12.50 AS Decimal(18, 2)), N'1112223351001', N'1112223351002', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (18, CAST(N'2019-11-07 15:50:16.573' AS DateTime), N'Virman', CAST(25.00 AS Decimal(18, 2)), N'1112223351004', N'1112223351003', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (19, CAST(N'2019-11-07 15:51:18.750' AS DateTime), N'Havale', CAST(100.00 AS Decimal(18, 2)), N'1112223341003', N'1112223351002', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (20, CAST(N'2019-11-13 16:26:14.600' AS DateTime), N'Havale', CAST(50.00 AS Decimal(18, 2)), N'1112223351002', N'1112223361003', N'Web')
INSERT [dbo].[havale_virman] ([islemID], [islemTarihi], [turu], [tutar], [aliciHesapNo], [gondericiHesapNo], [platform]) VALUES (21, CAST(N'2019-11-13 16:27:09.900' AS DateTime), N'Virman', CAST(50.00 AS Decimal(18, 2)), N'1112223361004', N'1112223361003', N'Web')
SET IDENTITY_INSERT [dbo].[havale_virman] OFF
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223341001', 111222334, CAST(750.55 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223341002', 111222334, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223341003', 111222334, CAST(100.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223341004', 111222334, NULL, 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223351001', 111222335, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223351002', 111222335, CAST(227.50 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223351003', 111222335, CAST(25.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223351004', 111222335, CAST(50.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223351005', 111222335, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223361001', 111222336, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223361002', 111222336, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223361003', 111222336, CAST(150.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223361004', 111222336, CAST(50.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223361005', 111222336, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223371001', 111222337, CAST(1388888888992889.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[hesap] ([hesapNo], [musteriNo], [bakiye], [aktiflikDurumu]) VALUES (N'1112223411001', 111222341, CAST(55555555555555.50 AS Decimal(18, 2)), 1)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223351', CAST(500.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223352', CAST(50.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223353', CAST(475.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223354', CAST(5.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223355', CAST(26.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223361', CAST(275.00 AS Decimal(18, 2)), 111222336)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223362', CAST(500.00 AS Decimal(18, 2)), 111222336)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223371', CAST(699999975.00 AS Decimal(18, 2)), 111222337)
INSERT [dbo].[HGS_Banka] ([HGSNumarası], [hgsBakiyesi], [HGSMusteriNumarasi]) VALUES (N'1112223411', CAST(641.00 AS Decimal(18, 2)), 111222341)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223351', CAST(500.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223352', CAST(50.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223353', CAST(475.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223354', CAST(5.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223355', CAST(26.00 AS Decimal(18, 2)), 111222335)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223361', CAST(275.00 AS Decimal(18, 2)), 111222336)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223362', CAST(500.00 AS Decimal(18, 2)), 111222336)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223371', CAST(699999975.00 AS Decimal(18, 2)), 111222337)
INSERT [dbo].[HGS_Kurum] ([HGSNO], [hgsBakiye], [musteriNumarasi]) VALUES (N'1112223411', CAST(641.00 AS Decimal(18, 2)), 111222341)
SET IDENTITY_INSERT [dbo].[kullanici] ON 

INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222334, N'azer', N'çelikten', N'36709999999', N'123456', N'azer@gmail.com', N'sşdksş323', N'55555555555')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222335, N'Mehmet', N'AYDIN', N'11111111111', N'123456', N'maydn@gmail.com', N'Turgutlu/MANİSA', N'05331111111')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222336, N'Hasan', N'Karataş', N'22222222222', N'123456', N'hasankaratas@gmail.com', N'Kıbrıs', N'05336669977')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222337, N'doruk', N'odabaşı', N'41414141414', N'414141', N'dorukbey@hotmail.com', N'kocaeli', N'05414144141')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222341, N'azer', N'çelikten', N'36709999998', N'123456', N'azer4@gmail.com', N'sdcdf', N'5340000000')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222346, N'ali2', N'ağaoğlu2', N'11111111222', N'123456', N'ali2@gmail.com', N'asd2/asd2', N'05367859647')
INSERT [dbo].[kullanici] ([musteriNo], [ad], [soyad], [TCKN], [sifre], [email], [adres], [telefon]) VALUES (111222347, N'ali', N'ağaoğlu', N'11111113222', N'123456', N'ali@gmail.com', N'asd/asd', N'05367859645')
SET IDENTITY_INSERT [dbo].[kullanici] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UC_Mail]    Script Date: 7.12.2019 11:01:14 ******/
ALTER TABLE [dbo].[kullanici] ADD  CONSTRAINT [UC_Mail] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UC_TCKN]    Script Date: 7.12.2019 11:01:14 ******/
ALTER TABLE [dbo].[kullanici] ADD  CONSTRAINT [UC_TCKN] UNIQUE NONCLUSTERED 
(
	[TCKN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[havale_virman]  WITH CHECK ADD  CONSTRAINT [FK_havale_virman_hesap] FOREIGN KEY([gondericiHesapNo])
REFERENCES [dbo].[hesap] ([hesapNo])
GO
ALTER TABLE [dbo].[havale_virman] CHECK CONSTRAINT [FK_havale_virman_hesap]
GO
ALTER TABLE [dbo].[havale_virman]  WITH CHECK ADD  CONSTRAINT [FK_havale_virman_hesap1] FOREIGN KEY([aliciHesapNo])
REFERENCES [dbo].[hesap] ([hesapNo])
GO
ALTER TABLE [dbo].[havale_virman] CHECK CONSTRAINT [FK_havale_virman_hesap1]
GO
ALTER TABLE [dbo].[hesap]  WITH CHECK ADD  CONSTRAINT [FK_hesap_kullanici] FOREIGN KEY([musteriNo])
REFERENCES [dbo].[kullanici] ([musteriNo])
GO
ALTER TABLE [dbo].[hesap] CHECK CONSTRAINT [FK_hesap_kullanici]
GO
ALTER TABLE [dbo].[HGS_Banka]  WITH CHECK ADD  CONSTRAINT [FK_HGS_Banka_kullanici] FOREIGN KEY([HGSMusteriNumarasi])
REFERENCES [dbo].[kullanici] ([musteriNo])
GO
ALTER TABLE [dbo].[HGS_Banka] CHECK CONSTRAINT [FK_HGS_Banka_kullanici]
GO
ALTER TABLE [dbo].[havale_virman]  WITH CHECK ADD  CONSTRAINT [CC_Tutar] CHECK  (([tutar]>(0)))
GO
ALTER TABLE [dbo].[havale_virman] CHECK CONSTRAINT [CC_Tutar]
GO
ALTER TABLE [dbo].[hesap]  WITH CHECK ADD  CONSTRAINT [CC_Bakiye] CHECK  (([bakiye]>=(0)))
GO
ALTER TABLE [dbo].[hesap] CHECK CONSTRAINT [CC_Bakiye]
GO
ALTER TABLE [dbo].[kullanici]  WITH CHECK ADD  CONSTRAINT [CC_UzunlukKullanici] CHECK  ((len([ad])<=(20) AND len([soyad])<=(20) AND len([TCKN])=(11) AND len([sifre])=(6) AND len([email])<=(50) AND len([telefon])<=(11)))
GO
ALTER TABLE [dbo].[kullanici] CHECK CONSTRAINT [CC_UzunlukKullanici]
GO
USE [master]
GO
ALTER DATABASE [YazilimBakimi] SET  READ_WRITE 
GO

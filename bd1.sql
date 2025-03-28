USE [master]
GO
/****** Object:  Database [Spirt]    Script Date: 25.03.2025 18:19:57 ******/
CREATE DATABASE [Spirt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Spirt', FILENAME = N'H:\SQL\MSSQL16.MSSQLSERVER\MSSQL\DATA\Spirt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Spirt_log', FILENAME = N'H:\SQL\MSSQL16.MSSQLSERVER\MSSQL\DATA\Spirt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Spirt] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Spirt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Spirt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Spirt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Spirt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Spirt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Spirt] SET ARITHABORT OFF 
GO
ALTER DATABASE [Spirt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Spirt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Spirt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Spirt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Spirt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Spirt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Spirt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Spirt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Spirt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Spirt] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Spirt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Spirt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Spirt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Spirt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Spirt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Spirt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Spirt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Spirt] SET RECOVERY FULL 
GO
ALTER DATABASE [Spirt] SET  MULTI_USER 
GO
ALTER DATABASE [Spirt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Spirt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Spirt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Spirt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Spirt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Spirt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Spirt', N'ON'
GO
ALTER DATABASE [Spirt] SET QUERY_STORE = ON
GO
ALTER DATABASE [Spirt] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Spirt]
GO
/****** Object:  Table [dbo].[Sports]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sports](
	[SportID] [int] IDENTITY(1,1) NOT NULL,
	[SportName] [nvarchar](100) NOT NULL,
	[UnitOfMeasurement] [nvarchar](50) NOT NULL,
	[WorldRecord] [decimal](10, 2) NOT NULL,
	[WorldRecordDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Athletes]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Athletes](
	[AthleteID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[BirthYear] [int] NOT NULL,
	[Team] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AthleteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participations]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participations](
	[ParticipationsID] [int] IDENTITY(1,1) NOT NULL,
	[SportID] [int] NOT NULL,
	[AthleteID] [int] NOT NULL,
	[CompetitionID] [int] NOT NULL,
	[Result] [decimal](10, 2) NOT NULL,
	[Place] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ParticipationsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RecordBreakingAthletes]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RecordBreakingAthletes] AS
SELECT 
    A.FirstName,A.MiddleName, A.LastName, P.Result, S.WorldRecord, S.SportName
FROM 
    Participations P
JOIN 
    Athletes A ON P.AthleteID = A.AthleteID
JOIN 
    Sports S ON P.SportID = S.SportID
WHERE 
    P.Result < S.WorldRecord;
GO
/****** Object:  Table [dbo].[Competitions]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Competitions](
	[CompetitionID] [int] IDENTITY(1,1) NOT NULL,
	[CompetitionName] [nvarchar](100) NOT NULL,
	[CompetitionDate] [date] NOT NULL,
	[SportLocation] [nvarchar](100) NOT NULL,
	[SportID] [int] NOT NULL,
 CONSTRAINT [PK__Competit__8F32F4F3A9027239] PRIMARY KEY CLUSTERED 
(
	[CompetitionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CompetitionPlaces]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CompetitionPlaces] AS
SELECT 
    A.FirstName, A.LastName, P.Place, C.CompetitionName, C.SportLocation, C.CompetitionDate, S.SportName
FROM 
    Participations P
JOIN 
    Athletes A ON P.AthleteID = A.AthleteID
JOIN 
    Competitions C ON P.CompetitionID = C.CompetitionID
JOIN 
    Sports S ON P.SportID = S.SportID
WHERE 
    C.CompetitionName = 'Открытый чемпионат' 
    AND C.SportLocation = 'Киев' 
    AND S.SportName = 'Шахматы' 
    AND YEAR(C.CompetitionDate) = 2000;
GO
/****** Object:  View [dbo].[Top3ResultsOfKaravaevInRunning]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Top3ResultsOfKaravaevInRunning] AS
SELECT 
    A.FirstName, A.LastName, P.Result AS BestResult, S.SportName
FROM 
    Participations P
JOIN 
    Athletes A ON P.AthleteID = A.AthleteID
JOIN 
    Sports S ON P.SportID = S.SportID
WHERE 
    A.LastName = 'Караваев'
    AND S.SportName = 'бег'
ORDER BY 
    P.Result ASC -- сортировка по возрастанию результата
OFFSET 0 ROWS FETCH NEXT 3 ROWS ONLY; -- ограничение до 3 записей

GO
/****** Object:  View [dbo].[AthletesInMultipleSports]    Script Date: 25.03.2025 18:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AthletesInMultipleSports] AS
SELECT 
    A.FirstName, A.MiddleName, A.LastName, COUNT(DISTINCT P.SportID) AS SportsCount
FROM 
    Participations P
JOIN 
    Athletes A ON P.AthleteID = A.AthleteID
GROUP BY 
    A.FirstName, A.LastName, A.MiddleName
HAVING 
    COUNT(DISTINCT P.SportID) > 3;

GO
SET IDENTITY_INSERT [dbo].[Athletes] ON 

INSERT [dbo].[Athletes] ([AthleteID], [FirstName], [MiddleName], [LastName], [BirthYear], [Team], [Category]) VALUES (1, N'Иван', N'Сергеевич', N'Караваев', 1985, N'Сборная России', N'Мастер спорта')
INSERT [dbo].[Athletes] ([AthleteID], [FirstName], [MiddleName], [LastName], [BirthYear], [Team], [Category]) VALUES (2, N'Олег', N'Петрович', N'Иванов', 1990, N'Сборная Казахстана', N'Мастер спорта')
INSERT [dbo].[Athletes] ([AthleteID], [FirstName], [MiddleName], [LastName], [BirthYear], [Team], [Category]) VALUES (3, N'Алексей', N'Николаевич', N'Петров', 1987, N'Сборная России', N'Первый разряд')
INSERT [dbo].[Athletes] ([AthleteID], [FirstName], [MiddleName], [LastName], [BirthYear], [Team], [Category]) VALUES (4, N'Сергей', N'Алексеевич', N'Сидоров', 1992, N'Сборная Беларуси', N'Мастер спорта')
INSERT [dbo].[Athletes] ([AthleteID], [FirstName], [MiddleName], [LastName], [BirthYear], [Team], [Category]) VALUES (5, N'Дмитрий', N'Владимирович', N'Ковалев', 1995, N'Сборная России', N'Второй разряд')
SET IDENTITY_INSERT [dbo].[Athletes] OFF
GO
SET IDENTITY_INSERT [dbo].[Competitions] ON 

INSERT [dbo].[Competitions] ([CompetitionID], [CompetitionName], [CompetitionDate], [SportLocation], [SportID]) VALUES (1, N'Открытый чемпионат', CAST(N'2000-06-15' AS Date), N'Киев', 1)
INSERT [dbo].[Competitions] ([CompetitionID], [CompetitionName], [CompetitionDate], [SportLocation], [SportID]) VALUES (2, N'Международный марафон', CAST(N'2020-09-20' AS Date), N'Москва', 2)
INSERT [dbo].[Competitions] ([CompetitionID], [CompetitionName], [CompetitionDate], [SportLocation], [SportID]) VALUES (3, N'Чемпионат мира', CAST(N'2022-03-14' AS Date), N'Нью-Йорк', 4)
INSERT [dbo].[Competitions] ([CompetitionID], [CompetitionName], [CompetitionDate], [SportLocation], [SportID]) VALUES (4, N'Европейский турнир', CAST(N'2021-05-10' AS Date), N'Берлин', 3)
INSERT [dbo].[Competitions] ([CompetitionID], [CompetitionName], [CompetitionDate], [SportLocation], [SportID]) VALUES (5, N'международная велогонка', CAST(N'2019-07-22' AS Date), N'Париж', 5)
SET IDENTITY_INSERT [dbo].[Competitions] OFF
GO
SET IDENTITY_INSERT [dbo].[Participations] ON 

INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (1, 1, 1, 1, CAST(8.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (2, 1, 2, 1, CAST(7.50 AS Decimal(10, 2)), 2)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (3, 1, 3, 1, CAST(7.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (4, 2, 1, 2, CAST(9.70 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (5, 2, 2, 2, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (6, 2, 4, 2, CAST(9.85 AS Decimal(10, 2)), 3)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (7, 3, 4, 4, CAST(21.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (8, 3, 5, 4, CAST(20.85 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (9, 4, 1, 3, CAST(2.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (10, 4, 3, 3, CAST(3.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (11, 5, 2, 5, CAST(25.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (12, 5, 4, 5, CAST(23.50 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (13, 5, 5, 5, CAST(24.50 AS Decimal(10, 2)), 2)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (14, 3, 1, 4, CAST(21.50 AS Decimal(10, 2)), 5)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (15, 4, 2, 3, CAST(2.50 AS Decimal(10, 2)), 4)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (16, 1, 4, 1, CAST(7.20 AS Decimal(10, 2)), 4)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (17, 2, 1, 3, CAST(9.85 AS Decimal(10, 2)), 1)
INSERT [dbo].[Participations] ([ParticipationsID], [SportID], [AthleteID], [CompetitionID], [Result], [Place]) VALUES (18, 2, 1, 4, CAST(9.90 AS Decimal(10, 2)), 4)
SET IDENTITY_INSERT [dbo].[Participations] OFF
GO
SET IDENTITY_INSERT [dbo].[Sports] ON 

INSERT [dbo].[Sports] ([SportID], [SportName], [UnitOfMeasurement], [WorldRecord], [WorldRecordDate]) VALUES (1, N'Шахматы', N'Очки', CAST(10.00 AS Decimal(10, 2)), CAST(N'1990-07-10' AS Date))
INSERT [dbo].[Sports] ([SportID], [SportName], [UnitOfMeasurement], [WorldRecord], [WorldRecordDate]) VALUES (2, N'Бег', N'Секунды', CAST(9.58 AS Decimal(10, 2)), CAST(N'2009-08-16' AS Date))
INSERT [dbo].[Sports] ([SportID], [SportName], [UnitOfMeasurement], [WorldRecord], [WorldRecordDate]) VALUES (3, N'Плавание', N'Секунды', CAST(20.91 AS Decimal(10, 2)), CAST(N'2020-07-24' AS Date))
INSERT [dbo].[Sports] ([SportID], [SportName], [UnitOfMeasurement], [WorldRecord], [WorldRecordDate]) VALUES (4, N'Теннис', N'Сеты', CAST(3.00 AS Decimal(10, 2)), CAST(N'2003-06-30' AS Date))
INSERT [dbo].[Sports] ([SportID], [SportName], [UnitOfMeasurement], [WorldRecord], [WorldRecordDate]) VALUES (5, N'Велоспорт', N'Минуты', CAST(24.00 AS Decimal(10, 2)), CAST(N'2012-07-28' AS Date))
SET IDENTITY_INSERT [dbo].[Sports] OFF
GO
ALTER TABLE [dbo].[Competitions]  WITH CHECK ADD  CONSTRAINT [FK__Competiti__Sport__3B75D760] FOREIGN KEY([SportID])
REFERENCES [dbo].[Sports] ([SportID])
GO
ALTER TABLE [dbo].[Competitions] CHECK CONSTRAINT [FK__Competiti__Sport__3B75D760]
GO
ALTER TABLE [dbo].[Participations]  WITH CHECK ADD FOREIGN KEY([AthleteID])
REFERENCES [dbo].[Athletes] ([AthleteID])
GO
ALTER TABLE [dbo].[Participations]  WITH CHECK ADD  CONSTRAINT [FK__Participa__Compe__3F466844] FOREIGN KEY([CompetitionID])
REFERENCES [dbo].[Competitions] ([CompetitionID])
GO
ALTER TABLE [dbo].[Participations] CHECK CONSTRAINT [FK__Participa__Compe__3F466844]
GO
ALTER TABLE [dbo].[Participations]  WITH CHECK ADD FOREIGN KEY([SportID])
REFERENCES [dbo].[Sports] ([SportID])
GO
USE [master]
GO
ALTER DATABASE [Spirt] SET  READ_WRITE 
GO

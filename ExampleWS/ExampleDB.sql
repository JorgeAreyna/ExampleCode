USE [master]
GO
/****** Object:  Database [ExampleDB]    Script Date: 6/9/2020 9:15:03 PM ******/
CREATE DATABASE [ExampleDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExampleDB', FILENAME = N'E:\BD\ExampleDB\ExampleDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ExampleDB_log', FILENAME = N'E:\BD\ExampleDB\ExampleDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExampleDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExampleDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExampleDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExampleDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExampleDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExampleDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExampleDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExampleDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ExampleDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExampleDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExampleDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExampleDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExampleDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExampleDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExampleDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExampleDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExampleDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExampleDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExampleDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExampleDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExampleDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExampleDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExampleDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExampleDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExampleDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ExampleDB] SET  MULTI_USER 
GO
ALTER DATABASE [ExampleDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExampleDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExampleDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExampleDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ExampleDB]
GO
/****** Object:  User [ExampleDB]    Script Date: 6/9/2020 9:15:03 PM ******/
CREATE USER [ExampleDB] FOR LOGIN [ExampleDB] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE [db_owner] ADD MEMBER [ExampleDB]
GO
/****** Object:  Schema [CORE]    Script Date: 6/9/2020 9:15:03 PM ******/
CREATE SCHEMA [CORE]
GO
/****** Object:  Schema [History]    Script Date: 6/9/2020 9:15:03 PM ******/
CREATE SCHEMA [History]
GO
/****** Object:  StoredProcedure [dbo].[SP_deleteCustomer]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Reyna 
-- Create date: 06/09/2020
-- Description:	delete  Customer.
-- =============================================
CREATE PROCEDURE [dbo].[SP_deleteCustomer]
	
@CustomerID int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	delete from  [dbo].[Customers] where CustomerID  =  @CustomerID 

END


GO
/****** Object:  StoredProcedure [dbo].[SP_GetContries]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Reyna
-- Create date: 06/09/2020
-- Description:stored procedure to get the list of countries
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetContries]
	
AS
BEGIN
	
	SET NOCOUNT ON;
		SELECT [CountryID],[CountryDescription]FROM [dbo].[Country]
  
END


GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomers]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure  [dbo].[SP_GetCustomers]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT  [CustomerID]
      ,[CompanyName]
      ,[ContactName]
      ,[Address]
      ,[City]
      ,[PostalCode]
      ,[Country]
      ,[Phone]
  FROM [ExampleDB].[dbo].[Customers]

  end


GO
/****** Object:  StoredProcedure [dbo].[SP_insertCustomer]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Reyna 
-- Create date: 06/09/2020
-- Description:	Insert new Customer.
-- =============================================
CREATE PROCEDURE [dbo].[SP_insertCustomer]
	
 @CompanyName varchar(250),   @ContactName  nvarchar(50), @Address nvarchar(60), @City nvarchar(15), @PostalCode nvarchar(10),
 @Country nvarchar(15),  @Phone nvarchar(24)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @CustomerID int
	select @CustomerID = isnull( max(CustomerID),1) from  [dbo].[Customers]


		INSERT INTO [dbo].[Customers]
		 ([CustomerID] ,[CompanyName] ,[ContactName] ,[Address] ,[City] ,[PostalCode] ,[Country]
		 ,[Phone])
		 VALUES
		 (@CustomerID, @CompanyName,  @ContactName,  @Address, @City, @PostalCode, @Country, @Phone)


END


GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomer]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Reyna 
-- Create date: 06/09/2020
-- Description:	update  Customer .
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateCustomer]
	@CustomerID int, 
 @CompanyName varchar(250),   @ContactName  nvarchar(50), @Address nvarchar(60), @City nvarchar(15), @PostalCode nvarchar(10),
 @Country nvarchar(15),  @Phone nvarchar(24)

AS
BEGIN
	
	SET NOCOUNT ON;	
	UPDATE [dbo].[Customers]
	   SET
		   [CompanyName] =@CompanyName
		  ,[ContactName] = @ContactName
		  ,[Address] = @Address
		  ,[City] = @City
		  ,[PostalCode] = @PostalCode
		  ,[Country] = @Country
		  ,[Phone] = @Phone
	 WHERE Customerid = @CustomerID
END


GO
/****** Object:  Table [dbo].[Country]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [int] NOT NULL,
	[CountryDescription] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY NONCLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 6/9/2020 9:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [nchar](5) NOT NULL,
	[CompanyName] [varchar](250) NOT NULL,
	[ContactName] [nvarchar](50) NULL,
	[Address] [nvarchar](60) NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[Phone] [nvarchar](24) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [ExampleDB] SET  READ_WRITE 
GO

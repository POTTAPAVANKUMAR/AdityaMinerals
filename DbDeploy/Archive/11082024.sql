USE [master]
GO
/****** Object:  Database [Adityaminerals]    Script Date: 11-08-2024 11:06:48 ******/
CREATE DATABASE [Adityaminerals]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Adityaminerals', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Adityaminerals.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Adityaminerals_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Adityaminerals_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Adityaminerals] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Adityaminerals].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Adityaminerals] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Adityaminerals] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Adityaminerals] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Adityaminerals] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Adityaminerals] SET ARITHABORT OFF 
GO
ALTER DATABASE [Adityaminerals] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Adityaminerals] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Adityaminerals] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Adityaminerals] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Adityaminerals] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Adityaminerals] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Adityaminerals] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Adityaminerals] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Adityaminerals] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Adityaminerals] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Adityaminerals] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Adityaminerals] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Adityaminerals] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Adityaminerals] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Adityaminerals] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Adityaminerals] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Adityaminerals] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Adityaminerals] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Adityaminerals] SET  MULTI_USER 
GO
ALTER DATABASE [Adityaminerals] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Adityaminerals] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Adityaminerals] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Adityaminerals] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Adityaminerals] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Adityaminerals] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Adityaminerals] SET QUERY_STORE = OFF
GO
USE [Adityaminerals]
GO
/****** Object:  User [pavan]    Script Date: 11-08-2024 11:06:48 ******/
CREATE USER [pavan] FOR LOGIN [pavan] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[ADM_L_BILLINGPART1]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADM_L_BILLINGPART1](
	[InvoiceNo] [int] IDENTITY(1,1) NOT NULL,
	[State_VC] [nvarchar](max) NULL,
	[StateCode] [int] NULL,
	[DateOfIssue] [datetime] NULL,
	[BP_Name_VC] [nvarchar](max) NULL,
	[BP_Address_VC] [nvarchar](max) NULL,
	[BP_Gstin_VC] [nvarchar](max) NULL,
	[BP_State_VC] [nvarchar](max) NULL,
	[BP_StateCode_VC] [nvarchar](max) NULL,
	[SP_Name_VC1] [nvarchar](max) NULL,
	[SP_Address_VC1] [nvarchar](max) NULL,
	[SP_Gstin_VC1] [nvarchar](max) NULL,
	[SP_State_VC1] [nvarchar](max) NULL,
	[SP_StateCode_VC1] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ADM_L_BILLINGPART2]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADM_L_BILLINGPART2](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [int] NULL,
	[ProductionDescription_VC] [varchar](max) NULL,
	[HSNCODE_VC] [nchar](10) NULL,
	[UOM_VC] [nchar](10) NULL,
	[QTY] [int] NULL,
	[Rate] [int] NULL,
	[Amount] [int] NULL,
	[Discount] [int] NULL,
	[ValueofSupply] [int] NULL,
	[Sno] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ADM_M_BILLINGPRODUCTS]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADM_M_BILLINGPRODUCTS](
	[Sno] [int] IDENTITY(1,1) NOT NULL,
	[ProductDescription] [varchar](50) NULL,
	[UOM_Id] [int] NULL,
	[UOM_Name] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Sno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ADM_M_UOM]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADM_M_UOM](
	[UOM_ID] [int] IDENTITY(1,1) NOT NULL,
	[UOMName_VC] [varchar](max) NULL,
	[UOMDesc_VC] [varchar](max) NULL,
	[SINGLEQTY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UOM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ADM_M_USER]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADM_M_USER](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ADM_L_BILLINGPART2]  WITH CHECK ADD FOREIGN KEY([InvoiceNo])
REFERENCES [dbo].[ADM_L_BILLINGPART1] ([InvoiceNo])
GO
/****** Object:  StoredProcedure [dbo].[ADM_ADDEDITBILLINGPRODUCTS]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create stored procedures
CREATE PROCEDURE [dbo].[ADM_ADDEDITBILLINGPRODUCTS]
    @SNO INT,
    @name VARCHAR(MAX),
    @uomID INT
AS
BEGIN
    IF @SNO <1
    BEGIN
        INSERT INTO dbo.ADM_M_BILLINGPRODUCTS (ProductDescription, UOM_Id)
        VALUES (@name, @uomID);
    END
    ELSE
    BEGIN
        UPDATE dbo.ADM_M_BILLINGPRODUCTS
        SET ProductDescription = @name, UOM_Id = @uomID
        WHERE Sno = @SNO;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_ADDEDITUOM]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_ADDEDITUOM]
    @uomid INT,
    @uomname VARCHAR(MAX),
    @uomdesc VARCHAR(MAX),
    @singleqty INT
AS
BEGIN
    IF @uomid <1
    BEGIN
        INSERT INTO dbo.ADM_M_UOM (UOMName_VC, UOMDesc_VC, SINGLEQTY)
        VALUES (@uomname, @uomdesc, @singleqty);
    END
    ELSE
    BEGIN
        UPDATE dbo.ADM_M_UOM
        SET UOMName_VC = @uomname, UOMDesc_VC = @uomdesc, SINGLEQTY = @singleqty
        WHERE UOM_ID = @uomid;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_CHART1]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ADM_CHART1]
AS
BEGIN
   select QTY as QTY , ProductionDescription_VC as PD from ADM_L_BILLINGPART2;
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_DELINVOICE]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_DELINVOICE]
    @invoiceid INT
AS
BEGIN
    DELETE FROM dbo.ADM_L_BILLINGPART2 WHERE InvoiceNo = @invoiceid;
    DELETE FROM dbo.ADM_L_BILLINGPART1 WHERE InvoiceNo = @invoiceid;
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_DELSUBINVOICE]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_DELSUBINVOICE]
    @invoiceid INT,
    @id INT
AS
BEGIN
    DELETE FROM dbo.ADM_L_BILLINGPART2 WHERE InvoiceNo = @invoiceid AND ID = @id;
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_SAVEBP1]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_SAVEBP1]
    @state VARCHAR(MAX),
    @statecode INT,
    @date DATE,
    @bpname VARCHAR(MAX),
    @bpaddress VARCHAR(MAX),
    @bpgstin VARCHAR(MAX),
    @bpstate VARCHAR(MAX),
    @bpstatecode INT,
    @spname VARCHAR(MAX),
    @spaddress VARCHAR(MAX),
    @spgstin VARCHAR(MAX),
    @spstate VARCHAR(MAX),
    @spstatecode INT
AS
BEGIN
    INSERT INTO dbo.ADM_L_BILLINGPART1 (State_VC, StateCode, DateOfIssue, BP_Name_VC, BP_Address_VC, BP_Gstin_VC, BP_State_VC, BP_StateCode_VC, SP_Name_VC1, SP_Address_VC1, SP_Gstin_VC1, SP_State_VC1, SP_StateCode_VC1)
    VALUES (@state, @statecode, @date, @bpname, @bpaddress, @bpgstin, @bpstate, @bpstatecode, @spname, @spaddress, @spgstin, @spstate, @spstatecode);
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_SAVEBP1EDIT]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_SAVEBP1EDIT]
    @invoiceNo INT,
    @state VARCHAR(MAX),
    @statecode INT,
    @date DATE,
    @bpname VARCHAR(MAX),
    @bpaddress VARCHAR(MAX),
    @bpgstin VARCHAR(MAX),
    @bpstate VARCHAR(MAX),
    @bpstatecode INT,
    @spname VARCHAR(MAX),
    @spaddress VARCHAR(MAX),
    @spgstin VARCHAR(MAX),
    @spstate VARCHAR(MAX),
    @spstatecode INT
AS
BEGIN
    UPDATE dbo.ADM_L_BILLINGPART1
    SET State_VC = @state, StateCode = @statecode, DateOfIssue = @date, BP_Name_VC = @bpname, BP_Address_VC = @bpaddress, BP_Gstin_VC = @bpgstin, BP_State_VC = @bpstate, BP_StateCode_VC = @bpstatecode, SP_Name_VC1 = @spname, SP_Address_VC1 = @spaddress, SP_Gstin_VC1 = @spgstin, SP_State_VC1 = @spstate, SP_StateCode_VC1 = @spstatecode
    WHERE InvoiceNo = @invoiceNo;
END;

GO
/****** Object:  StoredProcedure [dbo].[ADM_SAVEBP2]    Script Date: 11-08-2024 11:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADM_SAVEBP2]
    @InvoiceNo INT,
    @proddesc VARCHAR(MAX),
    @hsncode VARCHAR(MAX),
    @uom VARCHAR(MAX),
    @qty INT,
    @rate INT,
    @amount INT,
    @discount INT,
    @valueofsupply INT,
    @prodid INT
AS
BEGIN
    IF @prodid = 0
    BEGIN
        INSERT INTO dbo.ADM_L_BILLINGPART2 (InvoiceNo, ProductionDescription_VC, HSNCODE_VC, UOM_VC, QTY, Rate, Amount, Discount, ValueofSupply)
        VALUES (@InvoiceNo, @proddesc, @hsncode, @uom, @qty, @rate, @amount, @discount, @valueofsupply);
    END
    ELSE
    BEGIN
        UPDATE dbo.ADM_L_BILLINGPART2
        SET ProductionDescription_VC = @proddesc, HSNCODE_VC = @hsncode, UOM_VC = @uom, QTY = @qty, Rate = @rate, Amount = @amount, Discount = @discount, ValueofSupply = @valueofsupply
        WHERE ID = @prodid;
    END
END;

GO
USE [master]
GO
ALTER DATABASE [Adityaminerals] SET  READ_WRITE 
GO

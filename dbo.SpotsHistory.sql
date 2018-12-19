CREATE TABLE [dbo].[SpotsHistory] (
    [Timestamp] NVARCHAR (50) NULL,
    [Name]      NVARCHAR (50) NULL,
    [Value]     NVARCHAR (50) NULL,
    [Hour]      NVARCHAR (50) NULL,
    [Minute]    NVARCHAR (50) NULL,
    [Key]       INT           IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Key] ASC)
);


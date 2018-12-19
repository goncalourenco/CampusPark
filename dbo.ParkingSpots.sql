CREATE TABLE [dbo].[ParkingSpots] (
    [Name]          NVARCHAR (50) NOT NULL,
    [Park_Id]       NVARCHAR (50) NULL,
    [Location]      NVARCHAR (50) NULL,
    [BatteryStatus] INT           NULL,
    [Timestamp]     NVARCHAR (50) NULL,
    [Value]         NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Name] ASC)
);


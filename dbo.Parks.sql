CREATE TABLE [dbo].[Parks] (
    [Id]                   NVARCHAR (50)  NOT NULL,
    [Description]          NVARCHAR (MAX) NULL,
    [NumberOfSpecialSpots] INT            NULL,
    [OperatingHours]       NVARCHAR (50)  NULL,
    [GeoLocationFile]      NVARCHAR (50)  NULL,
    [NumberOfSpots]        INT            NULL,
    [Timestamp]            NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


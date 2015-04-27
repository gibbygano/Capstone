CREATE TABLE [dbo].[EventType] (
    [EventTypeID] INT           IDENTITY (100, 1) NOT NULL,
    [EventName]   VARCHAR (255) NOT NULL,
	[Active] BIT DEFAULT 1,

    CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED ([EventTypeID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [EventTypeIndex]
    ON [dbo].[EventType]([EventTypeID] ASC, [EventName] ASC);
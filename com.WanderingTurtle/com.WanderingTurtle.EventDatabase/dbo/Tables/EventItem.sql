CREATE TABLE [dbo].[EventItem] (
    [EventItemID]      INT           IDENTITY (100, 1) NOT NULL,
    [EventItemName]    VARCHAR (255) NOT NULL,
    [EventTypeID]      INT           NOT NULL,
    [EventOnsite]      BIT           NOT NULL,
    [Transportation]   BIT           NOT NULL,
    [EventDescription] VARCHAR (255) NULL,
    [Active]           BIT           NOT NULL,
    CONSTRAINT [PK_EventItem] PRIMARY KEY CLUSTERED ([EventItemID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [EventItemIndex]
    ON [dbo].[EventItem]([EventItemID] ASC, [EventItemName] ASC, [EventTypeID] ASC);
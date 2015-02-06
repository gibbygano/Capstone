CREATE TABLE [dbo].[EventItem](
	[EventItemID] [int] NOT NULL IDENTITY(100,1),
	[EventItemName] [varchar](255) NOT NULL,
	[EventStartTime] [datetime2] NOT NULL,
	[EventEndTime] [datetime2] NOT NULL,
	[CurrentNumberOfGuests] [int] NOT NULL DEFAULT(0),
	[MaxNumberOfGuests] [int] NOT NULL,
	[MinNumberOfGuests] [int],
	[EventTypeID] [int] NOT NULL,
	[PricePerPerson] [money] NOT NULL,
	[EventOnsite] [bit] NOT NULL,
	[Transportation] [bit] NOT NULL,
	[EventDescription] [varchar](255),
	[Active] [bit] NOT NULL
 CONSTRAINT [PK_EventItem] PRIMARY KEY CLUSTERED 
(
	[EventItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE INDEX EventItemIndex
ON [EventItem] (EventItemID, EventItemName, EventTypeID)
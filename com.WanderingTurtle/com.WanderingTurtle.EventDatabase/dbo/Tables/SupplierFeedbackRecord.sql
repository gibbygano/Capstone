CREATE TABLE [dbo].[SupplierFeedbackRecord] (
    [RatingID]   INT           IDENTITY (100, 1) NOT NULL,
    [SupplierID] INT           NOT NULL,
    [EmployeeID] INT           NOT NULL,
    [Rating]     INT           NOT NULL,
    [Notes]      VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_SupplierFeedbackRecord] PRIMARY KEY CLUSTERED ([RatingID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [SupplierFeedbackRecordIndex]
    ON [dbo].[SupplierFeedbackRecord]([RatingID] ASC, [Rating] ASC);
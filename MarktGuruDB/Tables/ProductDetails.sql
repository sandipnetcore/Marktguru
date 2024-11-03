CREATE TABLE [dbo].[ProductDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductDescription] [nvarchar](200) NOT NULL,
	[Availability] BIT NOT NULL DEFAULT 0,
	[Price] [decimal](10, 3) NOT NULL DEFAULT 0,
	[DateCreated] [datetime] NOT NULL DEFAULT GETDATE(),
	[ModifiedOn] [datetime] NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_ProductDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
),
CONSTRAINT [UK_Product_ID] UNIQUE ([ProductID]),
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetails_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO

ALTER TABLE [dbo].[ProductDetails] CHECK CONSTRAINT [FK_ProductDetails_Product]
GO



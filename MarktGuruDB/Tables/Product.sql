CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT GETDATE(),
	[ModifiedOn] [datetime] NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	),
CONSTRAINT [UK_Product_Name] UNIQUE ([Name]),
) ON [PRIMARY]

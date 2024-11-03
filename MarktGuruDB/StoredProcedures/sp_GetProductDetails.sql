CREATE PROCEDURE [dbo].[sp_GetProductDetails]
	@productId INT
AS BEGIN 
	SELECT 
			p.ID as ProductId
			,p.[Name] AS ProductName
			,pd.[Availability] AS ProductAvailability
			,pd.Price AS ProductPrice
			,pd.ProductDescription as ProductDescription
			FROM Product p 
			JOIN ProductDetails pd
			ON p.ID = pd.ProductID
			WHERE p.ID = @productId;
END
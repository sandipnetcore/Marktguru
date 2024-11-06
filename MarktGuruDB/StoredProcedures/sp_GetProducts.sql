CREATE PROCEDURE [dbo].[sp_GetProducts]
	@PageNumber INT = 1,
	@PageSize INT = 100
AS BEGIN
	DECLARE @EndRecordsCount INT = @PageNumber * @PageSize;
	DECLARE @StartingRecord INT = ((@PageNumber - 1) * @PageSize) + 1 ;

	WITH ProductCTE AS 
	(
		SELECT 
			 ROW_NUMBER() OVER (ORDER BY ID ASC) As RowNumbers 
			,ID AS ProductId
			,[Name] AS ProductName  
		FROM Product
	)

	
	SELECT 
	 p.ProductId 
	,p.ProductName 
	,pd.Availability AS ProductAvailability
	,pd.Price AS ProductPrice 
	FROM 
	ProductCTE p JOIN ProductDetails pd
	ON p.ProductId = pd.ProductID
	WHERE RowNumbers 
	BETWEEN @StartingRecord AND @EndRecordsCount 
	ORDER BY RowNumbers;
END

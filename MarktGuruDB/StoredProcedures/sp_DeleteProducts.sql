CREATE PROCEDURE [dbo].[sp_DeleteProducts]
	@productId int
AS BEGIN
	DELETE FROM ProductDetails WHERE ProductID = @productId
	DELETE FROM Product WHERE ID=@productId
END

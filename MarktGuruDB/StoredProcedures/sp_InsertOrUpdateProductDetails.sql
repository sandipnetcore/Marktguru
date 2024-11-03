CREATE PROCEDURE sp_InsertOrUpdateProductDetails
@ProductID INT,
@ProdDesc NVARCHAR(200),
@Availibility BIT,
@Price DECIMAL(10,3)

AS BEGIN
	IF NOT EXISTS(SELECT 1 FROM ProductDetails WHERE ProductID=@ProductID)
		BEGIN
			INSERT INTO ProductDetails (ProductID, ProductDescription, [Availability], Price)
			VALUES
			(@ProductID, @ProdDesc, @Availibility, @Price)
		END
	ELSE
		BEGIN 
			UPDATE ProductDetails 
			SET ProductDescription  = @ProdDesc, 
			[Availability]			= @Availibility, 
			Price					= @Price,
			ModifiedOn				= GETDATE()
			WHERE ProductID = @ProductID;
		END
END
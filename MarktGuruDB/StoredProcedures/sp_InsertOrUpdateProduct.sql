CREATE PROCEDURE sp_InsertOrUpdateProduct
@ProductID INT,
@Name NVARCHAR(100),
@ProdDesc NVARCHAR(200),
@Availibility BIT,
@Price DECIMAL(10,3)

AS BEGIN
	IF(@ProductID = 0)
		BEGIN
			INSERT INTO Product([Name]) VALUES (@Name);
			SELECT @ProductID = SCOPE_IDENTITY();
		END
	ELSE
		BEGIN 
			UPDATE Product 
			SET [Name] = @Name,
			ModifiedOn = GETDATE()
			WHERE ID = @ProductID;
		END

	EXECUTE sp_InsertOrUpdateProductDetails @ProductID, @ProdDesc, @Availibility, @Price
END
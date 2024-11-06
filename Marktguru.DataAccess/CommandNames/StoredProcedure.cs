namespace Marktguru.DataAccess.CommandNames
{
    /// <summary>
    /// Maintain the name conventions as Table_ProcedureName
    /// Contains all the stored procedure.
    /// </summary>
    public sealed class StoredProcedure
    {
        /// <summary>
        /// Stored Procedure to Get Products
        /// OutPut Single Table
        /// </summary>
        public const string Product_sp_GetProducts = "sp_GetProducts";

        /// <summary>
        /// Get the product details
        /// </summary>
        public const string Product_sp_GetProductDetails = "sp_GetProductDetails";
        
        /// <summary>
        /// Creates or Updates Products and Product Details
        /// </summary>
        public const string Product_sp_InsertOrUpdateProduct = "sp_InsertOrUpdateProduct";
        
        /// <summary>
        /// Hard Delete the product
        /// </summary>
        public const string Product_sp_DeleteProducts = "sp_DeleteProducts";
    }
}

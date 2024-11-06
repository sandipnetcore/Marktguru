namespace MarktguruAssignment.DataModels.Product
{
    /// <summary>
    /// Product Data representation
    /// The data model should be independent.
    /// Datamodel can be used in any project.
    /// Datamodel should be mapped witg the database objects (DataTable)
    /// </summary>
    public class ProductDataModel
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Availability
        /// </summary>
        public bool Availability { get; set; }
        
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

    }
}

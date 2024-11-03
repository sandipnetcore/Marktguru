namespace MarktguruAssignment.DataModels.Product
{
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

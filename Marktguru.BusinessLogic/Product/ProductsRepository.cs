using Marktguru.BusinessLogic.Configurations;
using Marktguru.DataAccess;
using Marktguru.DataAccess.CommandNames;
using MarktguruAssignment.DataModels.ModelExtension;
using MarktguruAssignment.DataModels.Product;
using Microsoft.Extensions.Options;

namespace Marktguru.BusinessLogic.Product
{
    /// <summary>
    /// Product Respository - Fetches all data for the Product
    /// Inserts product and Product details
    /// Updates Product
    /// Delete Products
    /// All Crud Operations related to Product are done here
    /// </summary>
    public class ProductsRepository
    {
        private ConnectionStringConfiguration _ConfigurationSettings { get; set; }
        public ProductsRepository(IOptions<ConnectionStringConfiguration> config) 
        {
            _ConfigurationSettings = config.Value;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="PageNumber">int</param>
        /// <param name="PageSize">int</param>
        /// <returns><see cref="List{ProductDataModel}"/></returns>
        public async Task<List<ProductDataModel>> GetProducts(int PageNumber = 1, int PageSize = 100) 
        { 
            DBOperations operations = new DBOperations(_ConfigurationSettings.ConnectionString);
            
            var parameters = new Dictionary<string, dynamic>()
            {
                { "PageNumber", PageNumber },
                { "PageSize", PageSize },
            };

            var result = await Task.FromResult<List<ProductDataModel>>(
                                    operations.GetDataSet(
                                        StoredProcedure.Product_sp_GetProducts, parameters
                                        ).Tables[0].
                                        ConvertToProductDataModelFromDataTable());


            return result;
        }

        /// <summary>
        /// Get Product Details Product Details 
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ProductDetailDataModel</returns>
        public async Task<ProductDetailDataModel> GetProductDetails(int id)
        {
            DBOperations operations = new DBOperations(_ConfigurationSettings.ConnectionString);

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@productId", id },
            };

            return await Task.FromResult<ProductDetailDataModel>(
                operations.GetDataSet(
                    StoredProcedure.Product_sp_GetProductDetails, parameters
                    ).Tables[0].
                    ConvertToProductDetailsDataModelFromDataTable());
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="newProduct">ProductDetailDataModel</param>
        /// <returns>Boolean</returns>
        public async Task<Boolean> CreateProduct(ProductDetailDataModel newProduct)
        {
            DBOperations operations = new DBOperations(_ConfigurationSettings.ConnectionString);

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@ProductID", 0 },
                {"@Name",  newProduct.ProductName},
                {"@ProdDesc", newProduct.ProductDescription },
                {"@Availibility", newProduct.Availability },
                {"@Price", newProduct.Price},
            };

            return await Task.FromResult<Boolean>(
                operations.InsertOrUpdateRecord(StoredProcedure.Product_sp_InsertOrUpdateProduct, parameters));
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="product">ProductDetailDataModel</param>
        /// <returns>Boolean</returns>
        public async Task<Boolean> UpdateProduct(ProductDetailDataModel product)
        {
            DBOperations operations = new DBOperations(_ConfigurationSettings.ConnectionString);

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@ProductID", product.ProductId },
                {"@Name",  product.ProductName},
                {"@ProdDesc", product.ProductDescription },
                {"@Availibility", product.Availability },
                {"@Price", product.Price},
            };

            return await Task.FromResult<Boolean>(
                operations.InsertOrUpdateRecord(StoredProcedure.Product_sp_InsertOrUpdateProduct, parameters));
        }

        /// <summary>
        /// Delete the product
        /// </summary>
        /// <param name="ProductId">int</param>
        /// <returns>Boolean</returns>
        public async Task<Boolean> DeleteProduct(int ProductId)
        {
            DBOperations operations = new DBOperations(_ConfigurationSettings.ConnectionString);

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@ProductID", ProductId },
            };

            return await Task.FromResult<Boolean>(
                operations.InsertOrUpdateRecord(StoredProcedure.Product_sp_DeleteProducts, parameters));
        }
    }
}

using Marktguru.BusinessLogic.Configurations;
using Marktguru.BusinessLogic.Product;
using Marktguru.BusinessLogic.Users;
using MarktguruAttributes = MarktguruAssignment.API.Attributes;
using MarktguruAssignment.DataModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarktguruAssignment.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IOptions<ConnectionStringConfiguration> _Options { get; }

        public ProductController(IOptions<ConnectionStringConfiguration> option)
        {
            _Options = option;
        }

        /// <summary>
        /// GET All the products
        /// </summary>
        /// <param name="PageNumber">int</param>
        /// <param name="PageSize">int</param>
        /// <returns><see cref="List{ProductDataModel}"/></returns>
        [MarktguruAttributes.Authorize(Roles.AdminRole)]
        [HttpGet("AllProducts/{PageNumber}/{PageSize}")]
        public async Task<IActionResult> Get(int PageNumber, int PageSize)
        {
            ProductsRepository productsRepository = new ProductsRepository(_Options);
            return Ok(await productsRepository.GetProducts(PageNumber, PageSize));
        }

        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ProductDetailDataModel</returns>
        [HttpGet("ProductDetails/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProductsRepository productsRepository = new ProductsRepository(_Options);
            return Ok(await productsRepository.GetProductDetails(id));
        }


        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <param name="product">ProductDetailDataModel</param>
        /// <returns>String</returns>
        [MarktguruAttributes.Authorize(Roles.AdminRole)]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Post([FromBody] ProductDetailDataModel product)
        {
            if (product == null)
            {
                return StatusCode(403, "Product specs are not given");
            }

            ProductsRepository productsRepository = new ProductsRepository(_Options);
            var result = await productsRepository.CreateProduct(product);

            return Ok("Product added successfully!");
        }


        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="product">ProductDetailDataModel</param>
        /// <returns></returns>
        [MarktguruAttributes.Authorize(Roles.AdminRole)]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Put([FromBody] ProductDetailDataModel product)
        {
            if (product == null)
            {
                return StatusCode(403, "Product specs are not given");
            }

            ProductsRepository productsRepository = new ProductsRepository(_Options);
            var result = await productsRepository.UpdateProduct(product);

            return Ok("Product updated successfully!");
        }


        /// <summary>
        /// Deletes the product
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        [MarktguruAttributes.Authorize(Roles.AdminRole)]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ProductsRepository productsRepository = new ProductsRepository(_Options);
            var result = await productsRepository.DeleteProduct(id);

            return Ok("Product deleted successfully!");
        }
    }
}

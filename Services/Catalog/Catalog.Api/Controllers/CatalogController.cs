using Amazon.Runtime.Internal.Util;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepo productRepo, ILogger<CatalogController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }

        #region GetProducts
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.GetProducts();
            return Ok(products);
        }

        #endregion

        #region Get product by id
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string id)
        {
            var product = await _productRepo.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"product with id: {id} is not found");
                return NotFound();
            }
            return Ok(product);
        }

        #endregion

        #region Get by category
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _productRepo.GetProductByCategory(category);
            return Ok(products);
        }

        #endregion

        #region CreateProduct
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepo.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        #endregion

        #region UpdateProduct
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           
            return Ok(await _productRepo.UpdateProduct(product));
        }

        #endregion

        #region DeleteProduct
        [HttpDelete("{id:length(24)}",Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {

            return Ok(await _productRepo.DeleteProduct(id));
        }

        #endregion

    }
}

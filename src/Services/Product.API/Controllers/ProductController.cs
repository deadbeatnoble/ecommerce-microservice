using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Product.API.Interfaces.Repository;
using System.Net;

namespace Product.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetProducts()
        {
            try {
                var products = await _productRepository.GetProducts();
                return CustomResult("Data load successful.", products);
            } catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found.", HttpStatusCode.NotFound);
                }
                var product = await _productRepository.GetProduct(id);
                return CustomResult("Data load successful.", product);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Models.Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                await _productRepository.CreateProduct(product);
                return CustomResult("Data saved successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found.", HttpStatusCode.NotFound);
                }
                var result = await _productRepository.DeleteProduct(id);
                if (result)
                {
                    return CustomResult("Data deleted successfully.");
                } else {
                    return CustomResult("Failed to delete data.", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Models.Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found.", HttpStatusCode.NotFound);
                }
                var result = await _productRepository.UpdateProduct(id, product);
                if (result)
                {
                    return CustomResult("Data modified successfully.");
                } else {
                    return CustomResult("Failed to modify data.", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }          
        }
    }
}

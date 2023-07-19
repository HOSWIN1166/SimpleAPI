using FirstSample01.API.Models.DomainAggregates;
using FirstSample01.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstSample01.API.Models.Services.Statuses;
using FirstSample01.API.Models.Contracts;
using FirstSample01.API.Models.Services;

namespace FirstSample01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository<Guid?, bool, RepositoryStatus> _productRepository;

        #region [-Ctor-]
        public ProductController(IProductRepository<Guid?, bool, RepositoryStatus> productRepository, ApplicationDbContext context)
        {
            _productRepository = productRepository;
        } 
        #endregion

        #region [-GetProduct-]

        [HttpGet("GetProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var (products, status) = await _productRepository.SelectAllAsync();

            if (status == RepositoryStatus.Success)
            {
                return Ok(products);
            }
            else if (status == RepositoryStatus.TableIsEmpty)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var (product, status) = await _productRepository.SelectByIdAsync(id);

            if (status == RepositoryStatus.Success)
            {
                return Ok(product);
            }
            else if (status == RepositoryStatus.NotExist)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(500);
            }
        } 
        #endregion

        #region [-PostProduct-]
        [HttpPost("PostProduct")]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            var (isExist, existStatus) = _productRepository.IsExist(product.Id);

            if (existStatus == RepositoryStatus.Success && isExist)
            {
                return Conflict();
            }

            var insertResult = await _productRepository.InsertAsync(product);

            if (insertResult == RepositoryStatus.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            else
            {
                return StatusCode(500);
            }
        } 
        #endregion

        #region [-PutProduct-]
        [HttpPut("PutProduct")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest();
            }

            var updateResult = await _productRepository.UpdateAsync(product);

            if (updateResult == RepositoryStatus.NullEntity)
            {
                return NotFound();
            }
            else if (updateResult == RepositoryStatus.Success)
            {
                return Ok(product);
            }
            else
            {
                return StatusCode(500);
            }
        }
        #endregion

        #region [-ProductDelete-]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult =await _productRepository.DeleteByIdAsync(id);

            if (deleteResult == RepositoryStatus.NullEntity)
            {
                return NotFound();
            }
            else if (deleteResult == RepositoryStatus.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("ProductDelete")]
        public async Task<ActionResult> ProductDelete(Guid id)
        {
            var (product, selectStatus) = await _productRepository.SelectByIdAsync(id);

            if (selectStatus == RepositoryStatus.NotExist)
            {
                return NotFound();
            }

            var deleteResult = await _productRepository.DeleteAsync(product);

            if (deleteResult == RepositoryStatus.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}

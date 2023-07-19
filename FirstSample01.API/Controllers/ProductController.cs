using FirstSample01.API.Models.DomainAggregates;
using FirstSample01.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstSample01.API.Models.Services.Statuses;

namespace FirstSample01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        #region [-PutProduct-]
        [HttpPut("PutProduct")]
        public IActionResult Put(Guid id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest();
            }

            var updateResult = _context.UpdateAsync(product);

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

        #region [-DeleteProduct-]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleteResult = _context.DeleteByIdAsync(id);

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
        #endregion
    }
}

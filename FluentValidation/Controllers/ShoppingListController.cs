using FluentValidation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart;
using ShoppingCart.Data;
using ShoppingCart.Validators;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        public async Task<IActionResult> Index([FromServices] AppDbContext db)
            => Ok(await db.Items.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShoppingItem data, [FromServices] AppDbContext db)
        {
            var validator = new AddShoppingItemValidator();
            var validationResult = validator.Validate(data);

            if (validationResult.IsValid)
            {
                db.Add(data);
                await db.SaveChangesAsync();
                return Ok(data);
            }
            return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
        }

        [HttpPost("v2")]
        public async Task<IActionResult> CreateV2([FromBody] ShoppingItem data, [FromServices] AppDbContext db)
        {
            if (ModelState.IsValid)
            {
                db.Add(data);
                await db.SaveChangesAsync();
                return Ok(data);
            }
            return BadRequest(ModelState.Values);
        }
    }
}

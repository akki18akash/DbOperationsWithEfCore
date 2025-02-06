using DbOperationWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public CurrencyController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurenciesAsync()
        {
            var result =await this.appDbContext.Currencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurencyByIdAsync([FromRoute] int id)
        {
            var result = await this.appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurencyByNameAsync([FromRoute] string name)
        {
            var result = await this.appDbContext.Currencies.Where(x=>x.Title==name).FirstOrDefaultAsync();
            return Ok(result);
        }
        //get value 1,2,3--multiple --static
        [HttpGet("all")]
        public async Task<IActionResult> GetmultipleCurrenciesAsync()
        {
            var ids=new List<int> {1,3,5,6 };
            var result = await this.appDbContext.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);
        }
        //get value 1,2,3--multiple --dynamic
        [HttpPost("all")]
        public async Task<IActionResult> GetmultiplePstCurrenciesAsync([FromBody] List<int> ids)
        {
          
            var result = await this.appDbContext.Currencies
                .Where(x => ids.Contains(x.Id))
                .Select(x=>new Currency() {Id=x.Id,Title=x.Title })
                .ToListAsync();
            return Ok(result);
        }
    }
}

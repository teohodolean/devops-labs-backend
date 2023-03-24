using DevOps_Labs_Backend.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevOps_Labs_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CounterController : ControllerBase
{
    private readonly IApplicationDbContext _context;

    public CounterController(IApplicationDbContext context) 
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<int>> GetCounter()
    {
        var counter = await _context.Counters.FirstOrDefaultAsync(); 
        return Ok(counter.Value);
    }

    [HttpPost]
    public async Task<ActionResult<int>> UpdateCounter()
    {
        var counter = await _context.Counters.FirstOrDefaultAsync();
        counter.Value += 1;

        await _context.SaveChangesAsync();

        return Ok(counter.Value);
    }
}

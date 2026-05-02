using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class CommunityEventsController : ControllerBase
{
    private readonly AppContext _context;

    public CommunityEventsController(AppContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }


    [HttpGet("GetBatchOfEvents")]
    public async Task<IActionResult> GetBatchOfEvents([FromQuery] int startIndex = 0, [FromQuery] int endIndex = 4) // TODO: add filters + user recommendations
    {

        if (startIndex < 0 || endIndex < startIndex)
        {
            return BadRequest("Invalid index range");
        }

        var events = await _context.CommunityEvents
  
            .Skip(startIndex)
            .Take(endIndex - startIndex + 1)
            .ToListAsync();

        return Ok(events);
    }

    [HttpPost("CreateEvent")]
    public async Task<IActionResult> CreateEvent([FromBody] Models.DTO.CommunityEvents newEvent)
    {
        if (newEvent == null)
        {
            return BadRequest("Event data is required");
        }

        Models.Backend.CommunityEvents backendEvent = new()
        {
            Name = newEvent.Name,
            Description = newEvent.Description,
            Date = newEvent.Date,
            Location = newEvent.Location,
            ImageUrl = newEvent.ImageUrl,
            ID = Guid.NewGuid(),
        };

        _context.CommunityEvents.Add(backendEvent);
        await _context.SaveChangesAsync();

        return Ok(backendEvent);
    }

    


}
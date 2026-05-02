
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


    [HttpGet("GetCommunityEvents")]
    public async Task<IActionResult> GetBatchOfEvents([FromBody] Models.DTO.CommunityEventBatchRequest request) // TODO: add filters + user recommendations
    {

        if (request.StartIndex < 0 || request.EndIndex < request.StartIndex)
        {
            return BadRequest("Invalid index range");
        }

        /// a simple algorithm that takes all the community events and finds the ones that match the user.
        var events = await _context.CommunityEvents
            .Where(e => request.Tags == null || request.Tags.Any(tag => e.Tags.Contains(tag)) 
            && (e.Date >= DateTime.Now))
            .Skip(request.StartIndex)
            .Take(request.EndIndex - request.StartIndex + 1)
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
            OriginalUrl = newEvent.OriginalUrl
        };

        _context.CommunityEvents.Add(backendEvent);
        await _context.SaveChangesAsync();

        return Ok(backendEvent.ID);
    }

    


}
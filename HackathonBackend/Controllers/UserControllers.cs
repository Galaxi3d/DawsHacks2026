using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


[ApiController]
[Route("api/[controller]")]
public class UserControllers : ControllerBase
{

    private readonly AppContext _context;

    public UserControllers(AppContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] Models.DTO.User user)
    {
        if (user == null)
        {
            return BadRequest("User data is required");
        }

        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            return BadRequest("Email already exists");
        }

        Models.Backend.User newUser = new()
        {
            ID = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        // Logic to register a new user
        return Ok(newUser.ID);
    }


    [HttpGet("LoginUser")]
    public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        return Ok(user.ID);
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }
}
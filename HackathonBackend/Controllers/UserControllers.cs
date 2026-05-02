using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            Password = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(user.Password)),
            Email = user.Email
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        // Logic to register a new user
        return Ok(newUser.ID);
    }


    [HttpGet("LoginUser")]
    public async Task<IActionResult> LoginUser([FromBody] Models.DTO.LogInUserRequest request)
    {
        var hashedPassword = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(request.Password));
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == hashedPassword);
        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        return Ok(user);
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

    [HttpPatch("UpdateUserBadge")]
    public async Task<IActionResult> UpdateUserBadge([FromQuery] Guid id, [FromQuery] string badge)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        user.Badges.Add(badge);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("UpdateUserPoints")]
    public async Task<IActionResult> UpdateUserPoints([FromQuery] Guid id, [FromQuery] uint points)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        user.Points += points;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("AddUserTask")]
    public async Task<IActionResult> AddUserTask([FromQuery] Guid id, [FromQuery] string task)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        if (user == null)        {
            return NotFound("User not found");
        }
        user.Tasks.Add(task);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("RemoveUserTask")]
    public async Task<IActionResult> RemoveUserTask([FromQuery] Guid id, [FromQuery] string task)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        user.Tasks.Remove(task);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("GetUserLeaderboard")]
    public async Task<IActionResult> GetUserLeaderboard()
    {
        var users = await _context.Users.OrderByDescending(u => u.Points).Take(10).ToListAsync();
        return Ok(users);
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }
}
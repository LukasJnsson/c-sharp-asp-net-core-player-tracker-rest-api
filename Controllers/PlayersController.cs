
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c_sharp_asp_net_core_player_tracker_rest_api.Data;
using c_sharp_asp_net_core_player_tracker_rest_api.Models;
namespace c_sharp_asp_net_core_player_tracker_rest_api.Controllers;


[ApiController]
public class PlayersController : BaseController
{
    private readonly ApplicationDbContext _context;

    public PlayersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Players
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
    {
        return await _context.Player.ToListAsync();
    }

    // GET: api/Players/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _context.Player.FindAsync(id);

        if (player == null)
        {
            return NotFound();
        }

        return player;
    }

    // PUT: api/Players/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlayer(int id, Player player)
    {
        if (id != player.id)
        {
            return BadRequest();
        }

        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlayerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Players
    [HttpPost]
    public async Task<ActionResult<Player>> PostPlayer(Player player)
    {
        _context.Player.Add(player);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlayer), new { id = player.id }, player);
    }

    // DELETE: api/Players/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var player = await _context.Player.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        _context.Player.Remove(player);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PlayerExists(int id)
    {
        return _context.Player.Any(e => e.id == id);
    }
}
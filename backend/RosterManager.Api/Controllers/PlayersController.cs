using Microsoft.AspNetCore.Mvc;
using RosterManager.Api.Models;
using RosterManager.Api.Services;

namespace RosterManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerStore _store;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(PlayerStore store, ILogger<PlayersController> logger)
    {
        _store = store;
        _logger = logger;
    }

    // GET /api/players?teamId=7
    [HttpGet]
    public ActionResult<IEnumerable<Player>> GetByTeam([FromQuery] int teamId)
    {
        var roster = _store.Players.Where(p => p.TeamId == teamId).ToList();
        return Ok(roster);
    }

    // Players added after the original roster table was seeded ended up further down
    // the table, past the old 1,000-row lookup window - raised to cover the full table.
    private const int MaxLookupPageSize = 200_000;

    // TODO: this still pulls a whole page into memory and scans it for one id. It works
    // today because MaxLookupPageSize covers the table, but it'll be back to silently
    // missing players the next time the roster outgrows the constant. The real fix is a
    // dedicated single-record lookup instead of page-then-filter. See NOTES.md.
    // GET /api/players/179850
    [HttpGet("{id:int}")]
    public ActionResult<Player?> GetById(int id)
    {
        var page = _store.Players.Take(MaxLookupPageSize).ToList();
        var player = page.FirstOrDefault(p => p.Id == id);
        return Ok(player);
    }
}

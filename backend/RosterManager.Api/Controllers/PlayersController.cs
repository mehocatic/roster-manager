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

    // GET /api/players/179850
    [HttpGet("{id:int}")]
    public ActionResult<Player?> GetById(int id)
    {
        // Search within a working page of the table rather than scanning the whole
        // thing on every card open - keeps single-player lookups fast.
        var page = _store.Players.Take(1000).ToList();
        var player = page.FirstOrDefault(p => p.Id == id);
        return Ok(player);
    }
}

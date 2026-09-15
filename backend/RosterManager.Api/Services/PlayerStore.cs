using RosterManager.Api.Models;

namespace RosterManager.Api.Services;

/// <summary>
/// Holds the league's player table in memory. Standing in for a real database table
/// that has grown to 180,000+ rows over several seasons.
/// </summary>
public class PlayerStore
{
    private static readonly string[] Positions = { "PG", "SG", "SF", "PF", "C" };

    private static readonly string[] FirstNames =
    {
        "Marcus", "Jordan", "Deacon", "Elias", "Trey", "Sawyer", "Miles", "Dante",
        "Kellan", "Rory", "Silas", "Judah", "Cyrus", "Grant", "Beckett", "Malik",
        "Owen", "Xavier", "Tobias", "Reid"
    };

    private static readonly string[] LastNames =
    {
        "Whitfield", "Castillo", "Bryant", "Nakamura", "Odom", "Harrington",
        "Delacroix", "Mercer", "Okafor", "Vance", "Abernathy", "Solano",
        "Greaves", "Kimura", "Lindqvist", "Pruitt", "Vaughn", "Ibarra"
    };

    // The demo team: a realistic 18-player roster seeded at low, early ids so it always
    // falls inside the first page of the table.
    public const int DemoTeamId = 7;
    public const string DemoTeamName = "Cascade Timberwolves";

    // A player who was traded to the Timberwolves after the rest of the roster already
    // existed, so his row landed far down the table instead of near the front.
    public const int RecentlyTradedPlayerId = 179_850;

    public IReadOnlyList<Player> Players { get; }

    public PlayerStore()
    {
        Players = GeneratePlayers();
    }

    private static List<Player> GeneratePlayers()
    {
        var random = new Random(42);
        var players = new List<Player>(180_000);

        for (var id = 1; id <= 180_000; id++)
        {
            players.Add(new Player
            {
                Id = id,
                Name = $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}",
                TeamId = random.Next(1, 9_000),
                Position = Positions[random.Next(Positions.Length)],
                Number = random.Next(0, 100)
            });
        }

        var demoRoster = new (string Name, string Position, int Number)[]
        {
            ("Cole Ashworth", "PG", 3),
            ("Bryce Halloway", "SG", 7),
            ("Nate Ferreira", "SF", 12),
            ("Isaiah Monroe", "PF", 21),
            ("Wes Calloway", "C", 34),
            ("Dominic Alvarado", "PG", 5),
            ("Theo Sandoval", "SG", 9),
            ("Landon Voss", "SF", 14),
            ("Cade Whitaker", "PF", 24),
            ("Ezra Pembrooke", "C", 41),
            ("Aaron Delgado", "PG", 2),
            ("Nolan Ashcroft", "SG", 8),
            ("Julian Marchetti", "SF", 15),
            ("Grady Kensington", "PF", 27),
            ("Emmett Sokolov", "C", 44),
            ("Preston Yamada", "PG", 4),
            ("Rhys Beaumont", "SG", 11),
            ("Victor Amadi", "SF", 19)
        };

        for (var i = 0; i < demoRoster.Length; i++)
        {
            var seedId = 141 + i; // low ids, always inside the first page
            var (name, position, number) = demoRoster[i];
            players[seedId - 1] = new Player
            {
                Id = seedId,
                Name = name,
                TeamId = DemoTeamId,
                Position = position,
                Number = number
            };
        }

        // Traded in mid-season. His player record was created long after the rest of the
        // roster, so it sits near the back of a 180,000-row table instead of the front.
        players[RecentlyTradedPlayerId - 1] = new Player
        {
            Id = RecentlyTradedPlayerId,
            Name = "Marcus Whitfield",
            TeamId = DemoTeamId,
            Position = "PG",
            Number = 11
        };

        return players;
    }
}

namespace RosterManager.Api.Models;

public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int TeamId { get; set; }
    public required string Position { get; set; }
    public int Number { get; set; }
}

using System;
using System.Collections.Generic;

namespace ServiceDb.Models;

public partial class Game
{
    public long GameId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Match> Matches { get; } = new List<Match>();
}

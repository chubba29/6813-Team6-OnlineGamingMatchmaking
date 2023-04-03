using System;
using System.Collections.Generic;

namespace ServiceDb.Models;

public partial class Match
{
    public long MatchId { get; set; }

    public long GameId { get; set; }

    public long FirstProfileId { get; set; }

    public long SecondProfileId { get; set; }

    public virtual Profile FirstProfile { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;

    public virtual Profile SecondProfile { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ServiceDb.Models;

public partial class GamePreference
{
    public long PrefId { get; set; }

    public long? GameId { get; set; }

    public long? ProfileId { get; set; }

    public long? Elo { get; set; }

    public string? Region { get; set; }

    public string? BehaviorIndex { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Profile? Profile { get; set; }
}

using System;
using System.Collections.Generic;

namespace ServiceDb.Models;

public partial class Profile
{
    public long ProfileId { get; set; }

    public long UserId { get; set; }

    public long? BehaviorIndex { get; set; }

    public bool? PrivacyBool { get; set; }

    public string? Region { get; set; }

    public virtual ICollection<GamePreference> GamePreferences { get; } = new List<GamePreference>();

    public virtual ICollection<Match> MatchFirstProfiles { get; } = new List<Match>();

    public virtual ICollection<Match> MatchSecondProfiles { get; } = new List<Match>();

    public virtual User User { get; set; } = null!;
}

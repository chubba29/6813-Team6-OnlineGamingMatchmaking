using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDb.Models;

public partial class Profile
{
    public long ProfileId { get; set; }

    public long UserId { get; set; }

    public long Elo { get; set; }

    public long? BehaviorIndex { get; set; }

    public bool? PrivacyBool { get; set; }

    public virtual ICollection<Match> MatchFirstProfiles { get; } = new List<Match>();

    public virtual ICollection<Match> MatchSecondProfiles { get; } = new List<Match>();

    public virtual User User { get; set; } = null!;
}

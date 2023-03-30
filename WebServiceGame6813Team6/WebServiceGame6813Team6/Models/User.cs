using System;
using System.Collections.Generic;

namespace ServiceDb.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long? ProfileId { get; set; }

    public virtual Profile? Profile { get; set; }

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}

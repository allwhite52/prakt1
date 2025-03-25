using System;
using System.Collections.Generic;

namespace prakt1.Models;

public partial class AthletesInMultipleSport
{
    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? SportsCount { get; set; }
}

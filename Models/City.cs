using System;
using System.Collections.Generic;

namespace GreenTogetheR.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<IllegalDumpReport> IllegalDumpReports { get; set; } = new List<IllegalDumpReport>();

    public virtual ICollection<RecylingCenter> RecylingCenters { get; set; } = new List<RecylingCenter>();

    public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; } = new List<RegisteredUser>();

    public virtual ICollection<WasteSchedule> WasteSchedules { get; set; } = new List<WasteSchedule>();
}

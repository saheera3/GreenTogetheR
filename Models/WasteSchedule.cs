using System;
using System.Collections.Generic;

namespace GreenTogetheR.Models;

public partial class WasteSchedule
{
    public int ScheduleId { get; set; }

    public int CityId { get; set; }

    public string CollectionDay { get; set; } = null!;

    public string WasteType { get; set; } = null!;

    public virtual City City { get; set; } = null!;
}

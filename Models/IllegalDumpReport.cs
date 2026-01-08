using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenTogetheR.Models;

public partial class IllegalDumpReport
{
    public int ReportId { get; set; }

    public int CityId { get; set; }

    public string? PhotoUrl { get; set; }

    public string Description { get; set; } = null!;

    public string? ReporterName { get; set; }

    public DateTime ReportDate { get; set; }

    public int? UserId { get; set; }
    
    public string? Location {  get; set; }

    public virtual City City { get; set; } = null!;

    public virtual RegisteredUser? User { get; set; }

    [NotMapped]
    public string? CityName { get; set; }
}

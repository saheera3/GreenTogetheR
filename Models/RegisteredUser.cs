using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenTogetheR.Models;

public partial class RegisteredUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int CityId { get; set; }

    [NotMapped]
    public string? CityName { get; set; }
    public string UserName { get; set; } = null!;

    
    public string Password { get; set; } = null!;

    public int? TotalPoints { get; set; }

    public string? Title { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<IllegalDumpReport> IllegalDumpReports { get; set; } = new List<IllegalDumpReport>();

    public virtual ICollection<PointEarned> PointEarneds { get; set; } = new List<PointEarned>();
}

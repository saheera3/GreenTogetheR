using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenTogetheR.Models;

public partial class RecylingCenter
{
    public int CenterId { get; set; }

    public int CityId { get; set; }
    [NotMapped]
    public string? CityName { get; set; }

    public string CenterName { get; set; } = null!;

    public string CenterAddress { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public virtual City City { get; set; } = null!;
}

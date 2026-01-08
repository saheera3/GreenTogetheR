using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenTogetheR.Models;

public partial class PointEarned
{
    public int PointId { get; set; }

    public int UserId { get; set; }

    public DateTime DateEarned { get; set; }

    public string ActionType { get; set; } = null!;

    public int PointsEarned { get; set; }


    public virtual RegisteredUser User { get; set; } = null!;

    [NotMapped]
    public int TotalPoints { get; set; }
}

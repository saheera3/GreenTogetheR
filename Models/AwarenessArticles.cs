using System;
using System.Collections.Generic;

namespace GreenTogetheR.Models;

public partial class AwarenessArticles
{
    public int ArticleId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public string ArticleDescription { get; set; } = null!;

    public string FileUrl { get; set; } = null!;
}

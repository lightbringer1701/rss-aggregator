using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AggregatorRSS.Context;

public partial class Channel
{
    public Guid guid { get; set; }

    public string address { get; set; } = null!;

    public string alias { get; set; } = null!;

    public bool enabled { get; set; }

    public DateTime? lastStart { get; set; }

    public DateTime? lastEnd { get; set; }
    [JsonIgnore]
    public virtual ICollection<Feed> feeds { get; set; } = new List<Feed>();

    public Channel()
    {
        guid = Guid.NewGuid();
        enabled = true;
    }
}

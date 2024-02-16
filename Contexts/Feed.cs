using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace AggregatorRSS.Context;

[XmlRoot(ElementName = "item")]
public partial class Feed
{
    [XmlIgnore]
    public Guid Guid { get; set; }

    [XmlElement(ElementName = "guid")]
    public string? GuidRss { get; set; }
    [XmlElement(ElementName = "author")]
    public string? Author { get; set; }
    [XmlElement(ElementName = "title")]
    public string? Title { get; set; }
    [XmlElement(ElementName = "link")]
    public string? Link { get; set; }
    [XmlElement(ElementName = "description")]
    public string? Description { get; set; }
    [XmlElement(ElementName = "category")]
    public string? Category { get; set; }
    [XmlElement(ElementName = "pubDate")]
    public string? pubDate { get; set; }

    [XmlIgnore]
    public Guid Channel { get; set; }
    [XmlIgnore]
    [JsonIgnore]
    public virtual Channel ChannelNavigation { get; set; } = null!;

    public Feed()
    {
        Guid = Guid.NewGuid();
    }
}

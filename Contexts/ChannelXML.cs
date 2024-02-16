using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AggregatorRSS.Context;

[XmlRoot(ElementName="channel")]
public partial class ChannelXML
{
    [XmlElement(ElementName="item")] 
	public List<Feed>? feeds { get; set; } 
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AggregatorRSS.Context;

[XmlRoot(ElementName="rss")]
public partial class RssXML
{
    [XmlElement(ElementName="channel")] 
	public ChannelXML? Channel { get; set; } 

}

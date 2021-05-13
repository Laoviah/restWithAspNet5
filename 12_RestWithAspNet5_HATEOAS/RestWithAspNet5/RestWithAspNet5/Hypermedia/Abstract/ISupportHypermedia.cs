using System.Collections.Generic;

namespace RestWithAspNet5.Hypermedia.Abstract
{
    public interface ISupportHypermedia
    {
        List<HypermediaLink> Links { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Data.Converter.Contract
{
    public interface IParser<Origin, Destination>
    {
        Destination Parse(Origin origin);
        List<Destination> Parse(List<Origin> origins);
    }
}

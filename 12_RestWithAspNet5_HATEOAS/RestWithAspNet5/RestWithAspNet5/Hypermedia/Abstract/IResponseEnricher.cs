using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithAspNet5.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        bool Enrich(ResultExecutingContext context);
    }
}

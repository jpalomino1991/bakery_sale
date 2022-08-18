using Bakery.Sale.DomainApi.Model;
using System.Collections.Generic;

namespace Bakery.Sale.DomainApi.Port
{
    public interface IObtainDeal<T>
    {
        List<Deal> GetDeals();
        Deal GetDeal(T id);
    }
}

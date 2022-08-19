using Bakery.Sale.DomainApi.Model;
using System.Collections.Generic;

namespace Bakery.Sale.DomainApi.Port
{
    public interface IObtainSale<T>
    {
        List<SaleEntity> GetDeals();
        SaleEntity GetDeal(T id);
    }
}

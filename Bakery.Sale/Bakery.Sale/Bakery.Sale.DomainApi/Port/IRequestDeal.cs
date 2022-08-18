using System.Collections.Generic;

namespace Bakery.Sale.DomainApi.Port
{
    public interface IRequestDeal<T>
    {
        List<T> GetDeals();
        T GetDeal(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Sale.DomainApi.Port
{
    public interface IRequestSale<T>
    {
        List<T> GetSale();
        T GetSaleById(int id);
        T AddSale(T dto);
        T UpdateSale(int id, T dto);
        T RemoveSaleById(int id);
        List<T> RemoveSaleByInvoice(string invoice);
    }
}

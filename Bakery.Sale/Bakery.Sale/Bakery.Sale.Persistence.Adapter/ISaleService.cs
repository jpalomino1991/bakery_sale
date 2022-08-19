using Bakery.Sale.Persistence.Adapter.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Sale.DomainApi
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleReadDto>> GetSale();
        //Task<IEnumerable<SaleReadDto>> SearchSale(SaleSearchDto Sale);
        //Task<IEnumerable<SaleReadDto>> GetSaleByType(string typeSale);
        Task<SaleReadDto> GetSaleById(int id);
        Task AddSale(SaleDto dto);
        Task UpdateSale(int id, SaleDto dto);
        Task<StatusDto> RemoveSaleById(int id);
    }
}

using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.Persistence.Adapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Sale.Persistence.Adapter.Implementations
{
    public class SaleService //: ISaleService
    {
       /* #region Properties & Members

        private readonly ISaleRepository _SaleRepository;

        #endregion

        #region Constructors

        public SaleService(ISaleRepository SaleRepository)
        {
            _SaleRepository = SaleRepository;
        }


        #endregion

        public async Task<IEnumerable<SaleReadDto>> GetSale()
        {
            var Sales = await _SaleRepository.GetAllAsync();

            return Sales.Select(x => new SaleReadDto
            {
                Id = x.Id,
                Invoice = x.Invoice,
                Quantity = x.Quantity,
                User = x.UserName,
                Date = x.Date,
                Product_Id=x.Product_Id,
                QRCode = x.QRCode
            });
        }

      /*  public async Task<IEnumerable<SaleReadDto>> SearchSale(SaleSearchDto Sale)
        {
            var inventories = await _SaleRepository.SearchSale(Sale);

            return inventories.Select(x => new SaleReadDto
            {
                Id = x.Id,
                Price = x.Price,
                Invoice = x.Invoice,
                MachineryId = x.MachineryId,
                Quantity = x.Quantity,
                Type = x.Type,
                User = x.User,
                CreationDate = x.CreationDate
            });
        }

        public async Task<SaleReadDto> GetSaleById(int id)
        {
            var Sale = await _SaleRepository.GetAsync(id);

            return new SaleReadDto
            {
                Id = Sale.Id,
                Invoice = Sale.Invoice,
                Quantity = Sale.Quantity,
                User = Sale.UserName,
                Date = Sale.Date,
                Product_Id = Sale.Product_Id
            };
        }

       /* public async Task<IEnumerable<SaleReadDto>> GetSaleByType(string typeSale)
        {
            var Sales = await _SaleRepository.GetSaleByType(typeSale);

            return Sales.Select(x => new SaleReadDto
            {
                Id = x.Id,
                Price = x.Price,
                Invoice = x.Invoice,
                MachineryId = x.MachineryId,
                Quantity = x.Quantity,
                Type = x.Type,
                User = x.User,
                CreationDate = x.CreationDate
            });
        }

        public async Task AddSale(SaleDto dto)
        {
            DateTime date = DateTime.Now;
            var saleEntity = new SaleEntity
            {
                Invoice = dto.Invoice,
                Quantity = dto.Quantity,
                UserName = dto.User,
                Date = dto.Date,
                Product_Id = dto.Product_Id,
                CreationDate = date,
                ModifiedDate = date

            };

            await _SaleRepository.AddAsync(saleEntity);
        }


        public async Task UpdateSale(int id, SaleDto dto)
        {
            DateTime date = DateTime.Now;
            var Sale = await _SaleRepository.GetAsync(id);
            Sale.Invoice = dto.Invoice;
            Sale.Quantity = dto.Quantity;
            Sale.UserName = dto.User;
            Sale.Date = dto.Date;
            Sale.Product_Id = dto.Product_Id;
            Sale.ModifiedDate = date;
            await _SaleRepository.UpdateAsync(Sale);
        }

        public async Task<StatusDto> RemoveSaleById(int id)
        {
            StatusDto status = new StatusDto();
            try
            {

                var Sale = await _SaleRepository.GetAsync(id);

                await _SaleRepository.RemoveAsync(Sale);

                status.IsSuccess = true;

                return status;
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
                return status;
            }
        }
       */
    }
}

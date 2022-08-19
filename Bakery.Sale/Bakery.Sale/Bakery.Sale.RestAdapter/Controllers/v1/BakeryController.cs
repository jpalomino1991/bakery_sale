using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.Persistence.Adapter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Sale.RestAdapter.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class BakeryController : ControllerBase
    {
        private readonly IRequestSale<SaleEntity> _SaleService;
        public BakeryController(IRequestSale<SaleEntity> SaleService)
        {
            _SaleService = SaleService;
        }

        // GET: api/<SaleController>
        [HttpGet]
        public  List<SaleReadDto> Get()
        {
            List<SaleReadDto> saleRead = new List<SaleReadDto>();

            var results = _SaleService.GetSale();
            foreach (var result in results)
            {
                var sale = new SaleReadDto {
                    Invoice = result.Invoice,
                    Date = result.Date,
                    Product_Id = result.Product_Id,
                    QRCode = result.QRCode,
                    Quantity = result.Quantity,
                    User = result.UserName
                };

                saleRead.Add(sale);
            }
            return saleRead;
        }

        // GET api/<SaleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _SaleService.GetSaleById(id);
            if (result == null) return NotFound();
            var sale = new SaleReadDto
            {
                Invoice = result.Invoice,
                Date = result.Date,
                Product_Id = result.Product_Id,
                QRCode = result.QRCode,
                Quantity = result.Quantity,
                User = result.UserName
            };

            return Ok(sale);
        }

        // POST api/<SaleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaleRegisterDto Sale)
        {

            if (Sale.Product_Id.Count <= 0)
            {
                return BadRequest(false);
            }

            foreach (var product in Sale.Product_Id)
            {
                var sale = new SaleEntity
                {
                    Date = DateTime.Now,
                    Invoice = Sale.Invoice,
                    Product_Id = product,
                    QRCode = Sale.QRCode,
                    Quantity = Sale.Quantity,
                    UserName = Sale.User
                };

                _SaleService.AddSale(sale);
            }

            return Ok(true);
        }

        // PUT api/<SaleController>/5
        [HttpPut("{id}")]
        public async Task<StatusDto> Put(int id, [FromBody] SaleDto Sale)
        {
            StatusDto status = new StatusDto();
            var result = _SaleService.GetSaleById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Sale {id} is not valid" };
                return status;
            }

            var sale = new SaleEntity
            {
                Date = DateTime.Now,
                Invoice = Sale.Invoice,
                Product_Id = Sale.Product_Id,
                QRCode = Sale.QRCode,
                Quantity = Sale.Quantity,
                UserName = Sale.User
            };

             _SaleService.UpdateSale(id, sale);

            return status = new StatusDto { IsSuccess = true, Message = $"" };  ;
        }

        // DELETE api/<SaleController>/5
        [HttpDelete("{id}")]
        public async Task<StatusDto> Delete(int id)
        {

            StatusDto status = new StatusDto();
            var result = _SaleService.GetSaleById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Sale {id} is not valid" };
                return status;
            }

            _SaleService.RemoveSaleById(id);

            return status = new StatusDto { IsSuccess = true, Message = $"" };
        }
    }
}

